namespace RemindIo;

public partial class WelcomePage : ContentPage
{
    public WelcomePage()
    {
        InitializeComponent();
    }

    private async void OnStartClicked(object sender, EventArgs e)
    {
        // Przej?cie do g?ównego ekranu
        await Navigation.PushAsync(new MainPage());
    }
}