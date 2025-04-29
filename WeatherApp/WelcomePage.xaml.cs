namespace WeatherApp;

public partial class WelcomePage : ContentPage
{
    public WelcomePage()
    {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, EventArgs e)
    {

    }

    private async void BtnStarted_Clicked(object sender, EventArgs e)
    {
#if IOS || MACCATALYST || WINDOWS
        await Navigation.PushModalAsync(new WeatherPage());
#else
        await Task.Run(() => Console.WriteLine("Navigation not supported on this platform."));
#endif
    }
}
