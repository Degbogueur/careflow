using System.ComponentModel.DataAnnotations.Schema;

namespace CareFlow.Models;

public class Patient
{
    public int Id { get; set; }
    public int? MedicalRecordId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required DateTime DateOfBirth { get; set; }
    public required Gender Gender { get; set; }
    public string? PhoneNumber { get; set; }
    public Address? Address { get; set; }
    public required DateTime RegistrationDate { get; set; } = DateTime.Now;
    public string? UserId { get; set; }

    public MedicalRecord? MedicalRecord { get; set; }
    public ICollection<Appointment> Appointments { get; set; } = [];

    [NotMapped]
    public int Age => DateTime.Now.Year - DateOfBirth.Year - (DateTime.Now.DayOfYear < DateOfBirth.DayOfYear ? 1 : 0);

    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";

    [NotMapped]
    public bool HasUserAccount => !string.IsNullOrEmpty(UserId);
}

public enum Gender
{
    Male,
    Female,
    Other
}
