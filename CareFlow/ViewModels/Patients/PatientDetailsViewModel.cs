using CareFlow.Models;
using System.ComponentModel;

namespace CareFlow.ViewModels.Patients;

public class PatientDetailsViewModel
{
    public int Id { get; set; }
    [DisplayName("Full name")]
    public string FullName { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    [DisplayName("Date of birth")]
    public DateTime DateOfBirth { get; set; }
    public int Age { get; set; }
    public string Email { get; set; } = string.Empty;
    [DisplayName("Phone number")]
    public string? PhoneNumber { get; set; }
    public Address? Address { get; set; }
    [DisplayName("Registration date")]
    public DateTime RegistrationDate { get; set; }
}
