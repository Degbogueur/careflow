using CareFlow.Models.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareFlow.Models;

public class Doctor
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public int? SpecialtyId { get; set; }
    public string? UserId { get; set; }

    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";
    [NotMapped]
    public bool HasUserAccount => !string.IsNullOrEmpty(UserId);

    public Specialty? Specialty { get; set; }
    public ApplicationUser? User { get; set; }
    public ICollection<Appointment> Appointments { get; set; } = [];
    public ICollection<Consultation> Consultations { get; set; } = [];
}
