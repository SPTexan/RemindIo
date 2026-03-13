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

    private async void DeleteReminder(object sender, EventArgs e)
    {
        var swipeItem = sender as SwipeItem;
        var reminder = swipeItem.CommandParameter as Reminder;

        bool confirm = await DisplayAlert("Usuń", "Czy chcesz usunąć przypomnienie?", "Tak", "Nie");

        if (!confirm) return;

        reminders.Remove(reminder);

        ReminderService.Save(reminders);

        LoadReminders();
    }

    private async void EditReminder(object sender, EventArgs e)
    {
        var swipeItem = sender as SwipeItem;
        var reminder = swipeItem.CommandParameter as Reminder;

        await Navigation.PushAsync(new AddReminderPage(reminder));
    }
}