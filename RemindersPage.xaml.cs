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
        remindersList.ItemsSource = reminders;
    }

    private async void DeleteReminderClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var reminder = button.CommandParameter as Reminder;

        bool confirm = await DisplayAlert("Usuń", "Czy usunąć przypomnienie?", "Tak", "Nie");

        if (!confirm)
            return;

        reminders.Remove(reminder);

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