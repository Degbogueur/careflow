using CareFlow.Models;

namespace CareFlow.ViewModels.Patients;

public class PatientViewModel
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public Address? Address { get; set; }
    public int Age { get; set; }
    public required DateTime RegistrationDate { get; set; }
    public bool HasUserAccount { get; set; }
}
