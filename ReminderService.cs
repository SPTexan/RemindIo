using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace RemindIo;

public static class ReminderService
{
    static string filePath = Path.Combine(FileSystem.AppDataDirectory, "reminders.json");

    public static List<Reminder> Load()
    {
        if (!File.Exists(filePath))
            return new List<Reminder>();

        var json = File.ReadAllText(filePath);

        return JsonSerializer.Deserialize<List<Reminder>>(json) ?? new List<Reminder>();
    }

    public static void Save(List<Reminder> reminders)
    {
        var json = JsonSerializer.Serialize(reminders);

        File.WriteAllText(filePath, json);
    }
}
