using CareFlow.Models;

namespace CareFlow.ViewModels.Patients;

public class PatientViewModel
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string? PhoneNumber { get; set; }
    public Address? Address { get; set; }
    public required DateTime RegistrationDate { get; set; }
    public int Age { get; set; }
}
