using Plugin.Settings;
using Plugin.Settings.Abstractions;


namespace WinpackCross
{
    public static class Settings
    {
        
        private static ISettings AppSettings
        {
            get
            {
                if (CrossSettings.IsSupported)
                    return CrossSettings.Current;

                return null; // or your custom implementation 
            }
        }
        //Setting Constants
        public static string UserName
        {
            get => AppSettings.GetValueOrDefault(nameof(UserName), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(UserName), value);
        }
        public static string Password
        {
            get => AppSettings.GetValueOrDefault(nameof(Password), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Password), value);
        }
        public static bool  Hatırla
        {
            get => AppSettings.GetValueOrDefault(nameof(Hatırla), false);
            set => AppSettings.AddOrUpdateValue(nameof(Hatırla), value);
        }
    }
}
