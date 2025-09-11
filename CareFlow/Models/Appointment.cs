namespace CareFlow.Models;

public class Appointment
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;

    public Patient? Patient { get; set; }
    public Doctor? Doctor { get; set; }
}

public enum AppointmentStatus
{
    Scheduled,
    Completed,
    Cancelled
}
