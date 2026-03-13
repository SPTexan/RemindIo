using Plugin.LocalNotification;

namespace RemindIo;

public partial class AddReminderPage : ContentPage
{
    double latitude;
    double longitude;
    string photoPath;

    Reminder editingReminder;

    public AddReminderPage(Reminder reminder = null)
    {
        InitializeComponent();

        if (reminder != null)
        {
            editingReminder = reminder;

            titleEntry.Text = reminder.Title;
            descriptionEntry.Text = reminder.Description;

            startTimePicker.Time = reminder.StartTime;
            endTimePicker.Time = reminder.EndTime;

            latitude = reminder.Latitude;
            longitude = reminder.Longitude;

            photoPath = reminder.PhotoPath;

            if (!string.IsNullOrEmpty(photoPath))
                photoPreview.Source = ImageSource.FromFile(photoPath);
        }
    }

    private async void GetLocationClicked(object sender, EventArgs e)
    {
        var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

        if (status != PermissionStatus.Granted)
        {
            await DisplayAlert("GPS", "Brak dostępu do lokalizacji", "OK");
            return;
        }

        var location = await Geolocation.GetLocationAsync();

        if (location != null)
        {
            latitude = location.Latitude;
            longitude = location.Longitude;

            await DisplayAlert("GPS", $"{latitude}, {longitude}", "OK");
        }
    }

    private async void TakePhotoClicked(object sender, EventArgs e)
    {
        var photo = await MediaPicker.Default.CapturePhotoAsync();

        if (photo == null)
            return;

        var newFile = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);

        using var stream = await photo.OpenReadAsync();
        using var newStream = File.OpenWrite(newFile);

        await stream.CopyToAsync(newStream);

        photoPath = newFile;

        photoPreview.Source = ImageSource.FromFile(photoPath);
    }

    private async void SaveReminderClicked(object sender, EventArgs e)
    {
        var reminders = ReminderService.Load();

        if (editingReminder != null)
        {
            reminders.Remove(editingReminder);
        }

        var reminder = new Reminder
        {
            Title = titleEntry.Text,
            Description = descriptionEntry.Text,
            StartTime = startTimePicker.Time,
            EndTime = endTimePicker.Time,
            Latitude = latitude,
            Longitude = longitude,
            PhotoPath = photoPath,
            CreatedAt = DateTime.Now
        };

        reminders.Add(reminder);

        ReminderService.Save(reminders);

        await DisplayAlert("OK", "Przypomnienie zapisane", "OK");

        await Navigation.PopAsync();
    }
}