namespace RemindIo;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void AddReminderClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddReminderPage());
    }

    private async void ViewRemindersClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RemindersPage());
    }
}
