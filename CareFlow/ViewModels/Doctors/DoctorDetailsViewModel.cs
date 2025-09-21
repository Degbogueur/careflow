using CareFlow.ViewModels.Patients;
using System.ComponentModel;

namespace CareFlow.ViewModels.Doctors;

public class DoctorDetailsViewModel
{
    public int Id { get; set; }
    [DisplayName("Full name")]
    public string FullName { get; set; } = string.Empty;
    [DisplayName("Specialty")]
    public string SpecialtyName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    [DisplayName("Phone number")]
    public string? PhoneNumber { get; set; }

    public ICollection<PatientViewModel> Patients { get; set; } = [];
}