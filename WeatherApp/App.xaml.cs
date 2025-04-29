namespace WeatherApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

#if IOS || MACCATALYST || WINDOWS
    VersionTracking.Track();
#endif
        }


        protected override Window CreateWindow(IActivationState? activationState) // Fix: Make activationState nullable
        {
            return new Window(DetermineStartupPage());
        }

        private static Page DetermineStartupPage()
        {
#if IOS || MACCATALYST || WINDOWS
    if (VersionTracking.IsFirstLaunchEver)
    {
        return new WelcomePage();
    }
#endif
            return new WeatherPage();
        }



    }
}
