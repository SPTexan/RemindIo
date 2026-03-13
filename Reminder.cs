using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Reminder
{
    public string Title { get; set; }

    public string Description { get; set; }

    public TimeSpan StartTime { get; set; }

    public TimeSpan EndTime { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public string PhotoPath { get; set; }

    public DateTime CreatedAt { get; set; }
}
