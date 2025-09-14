using CareFlow.Models;

namespace CareFlow.ViewModels.Patients;

public class AddPatientViewModel
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public Address? Address { get; set; }
    public required DateTime RegistrationDate { get; set; } = DateTime.Now;
}

public class UpdatePatientViewModel
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string? PhoneNumber { get; set; }
    public Address? Address { get; set; }
    public required DateTime RegistrationDate { get; set; }
}
