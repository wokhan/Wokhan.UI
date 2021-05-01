
using Windows.Storage;

namespace Wokhan.UI.Settings
{
    public partial class SettingsManager
    {
        // Not really the best way but the UsePublisherFolder might be useful and it relies on BaseSharedFolder, so keeping it that way for now.
        private static readonly StorageFolder BaseSharedFolder = ApplicationData.Current.LocalFolder;
        private static readonly StorageFolder LocalFolder = ApplicationData.Current.LocalFolder;
    }
}
