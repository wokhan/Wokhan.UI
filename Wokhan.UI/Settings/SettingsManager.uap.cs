using Windows.Storage;

namespace Wokhan.UI.Settings
{
    public partial class SettingsManager
    {
        private static readonly StorageFolder BaseSharedFolder = ApplicationData.Current.GetPublisherCacheFolder("SmartTiles");
        private static readonly StorageFolder LocalFolder = ApplicationData.Current.LocalFolder;
    }
}