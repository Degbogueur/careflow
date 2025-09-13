using CareFlow.Models;

namespace CareFlow.ViewModels.Appointments;

public class AppointmentViewModel
{
    public int Id { get; set; }
    public required string PatientName { get; set; }
    public required string DoctorName { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public AppointmentStatus Status { get; set; }
}
