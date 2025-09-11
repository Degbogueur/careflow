using System.ComponentModel.DataAnnotations.Schema;

namespace CareFlow.Models;

public class Consultation
{
    public int Id { get; set; }
    public int MedicalRecordId { get; set; }
    public int DoctorId { get; set; }
    public DateTime Date { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Height { get; set; }
    public decimal? Temperature { get; set; }
    public string? Symptoms { get; set; }
    public required string Diagnosis { get; set; }
    public required string Treatment { get; set; }
    public string? Notes { get; set; }

    public MedicalRecord? MedicalRecord { get; set; }
    public Doctor? Doctor { get; set; }

    [NotMapped]
    public decimal? BMI => Weight.HasValue && Height.HasValue
                           ? Math.Round(Weight.Value / (Height.Value / 100 * Height.Value / 100), 2)
                           : null;
}
