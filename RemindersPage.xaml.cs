namespace RemindIo;

public partial class RemindersPage : ContentPage
{
    List<Reminder> reminders;

    public RemindersPage()
    {
        InitializeComponent();
        LoadReminders();
    }

    void LoadReminders()
    {
        reminders = ReminderService.Load();

        remindersList.ItemsSource = null;
        remindersList.ItemsSource = reminders;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadReminders();
    }

    private async void DeleteReminderClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var reminder = button.CommandParameter as Reminder;

        bool confirm = await DisplayAlert("Usuń",
                                          "Czy usunąć przypomnienie?",
                                          "Tak",
                                          "Nie");

        if (!confirm)
            return;

        reminders.RemoveAll(r => r.CreatedAt == reminder.CreatedAt);

        ReminderService.Save(reminders);

        LoadReminders();
    }

    private async void EditReminderClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var reminder = button.CommandParameter as Reminder;

        await Navigation.PushAsync(new AddReminderPage(reminder));
    }
}