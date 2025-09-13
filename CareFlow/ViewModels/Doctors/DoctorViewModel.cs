namespace CareFlow.ViewModels.Doctors;

public class DoctorViewModel
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string SpecialtyName { get; set; } = string.Empty;
    public int PatientsCount { get; set; }
    public int UpcomingAppointments { get; set; }
}
