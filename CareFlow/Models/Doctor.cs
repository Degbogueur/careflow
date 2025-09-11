using System.ComponentModel.DataAnnotations.Schema;

namespace CareFlow.Models;

public class Doctor
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public int? SpecialtyId { get; set; }

    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";

    public Specialty? Specialty { get; set; }
    public ICollection<Appointment> Appointments { get; set; } = [];
    public ICollection<Consultation> Consultations { get; set; } = [];
}
