using Newtonsoft.Json;

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Windows.Foundation;
using Windows.Storage;

namespace Wokhan.UI.Settings
{
    public partial class SettingsManager
    {
        public static void CleanUp(bool usePublisherFolder, params string[] fileNames)
        {
            _cleanUp(usePublisherFolder, fileNames);
        }

        public static IAsyncOperation<T> LoadSharedSettings<T>(string fileName = "Settings.dat", Func<T> defaultValue = null)
        {
            return LoadSettingsAsync<T>(fileName, true, defaultValue).AsAsyncOperation();
        }

        public static IAsyncOperation<T> LoadSettings<T>(string fileName = "Settings.dat", Func<T> defaultValue = null)
        {
            return LoadSettingsAsync<T>(fileName, false, defaultValue).AsAsyncOperation();
        }

        public static IAsyncOperation<bool> SaveSharedSettings<T>(T calOpts, string fileName = "Settings.dat")
        {
            return SaveSettingsAsync(calOpts, fileName, true).AsAsyncOperation();
        }

        public static IAsyncOperation<bool> SaveSettings<T>(T calOpts, string fileName = "Settings.dat")
        {
            return SaveSettingsAsync(calOpts, fileName, false).AsAsyncOperation();
        }

        private static void _cleanUp(bool usePublisherFolder, params string[] fileNames)
        {
            StorageFolder baseFolder = usePublisherFolder ? BaseSharedFolder : LocalFolder;
            fileNames.ToList().ForEach(async f =>
            {
                try
                {
                    await (await baseFolder.GetFileAsync(f)).DeleteAsync();
                }
                catch (FileNotFoundException) { }
            });
        }

        private static async Task<T> LoadSettingsAsync<T>(string fileName, bool usePublisherFolder, Func<T> defaultValue = null)
        {
            StorageFolder baseFolder = usePublisherFolder ? BaseSharedFolder : LocalFolder;

            var sets = (StorageFile)await baseFolder.TryGetItemAsync(fileName);

            if (sets == null && usePublisherFolder)
            {
                sets = (StorageFile)await LocalFolder.TryGetItemAsync(fileName);
                // Moving the file back to the publisher folder (as we wanted to use it)
                if (sets != null)
                {
                    sets = await sets.CopyAsync(baseFolder);
                }
            }

            if (sets != null)
            {
                try
                {
                    var data = await FileIO.ReadTextAsync(sets);
                    return JsonConvert.DeserializeObject<T>(data);
                }
                catch (Exception e)
                {
                    await sets.CopyAsync(baseFolder, fileName + ".bk", NameCollisionOption.ReplaceExisting);
                    throw e;
                }
            }

            return defaultValue != null ? defaultValue() : Activator.CreateInstance<T>();
        }

        private static async Task<bool> SaveSettingsAsync<T>(T calOpts, string fileName, bool usePublisher)
        {
            StorageFile sets = await (usePublisher ? BaseSharedFolder : LocalFolder).CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

            await FileIO.WriteTextAsync(sets, JsonConvert.SerializeObject(calOpts));

            return true;
        }
    }
}