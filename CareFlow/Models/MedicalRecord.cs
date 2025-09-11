using System.ComponentModel.DataAnnotations;

namespace CareFlow.Models;

public class MedicalRecord
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string? Allergies { get; set; }
    public BloodType BloodType { get; set; }
    public string? Notes { get; set; }

    public Patient? Patient { get; set; }
    public ICollection<Consultation> Consultations { get; set; } = [];
}

public enum BloodType
{
    [Display(Name = "A+")]
    A_Positive,
    [Display(Name = "A-")]
    A_Negative,
    [Display(Name = "B+")]
    B_Positive,
    [Display(Name = "B-")]
    B_Negative,
    [Display(Name = "AB+")]
    AB_Positive,
    [Display(Name = "AB-")]
    AB_Negative,
    [Display(Name = "O+")]
    O_Positive,
    [Display(Name = "O-")]
    O_Negative
}
