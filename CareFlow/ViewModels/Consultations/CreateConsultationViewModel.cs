using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CareFlow.ViewModels.Consultations;

public class CreateConsultationViewModel
{
    public DateTime Date { get; set; } = DateTime.Now;
    public int? AppointmentId { get; set; }
    [DisplayName("Patient")]
    public int PatientId { get; set; }
    [DisplayName("Doctor")]
    public int DoctorId { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Height { get; set; }
    public decimal? Temperature { get; set; }
    public string? Symptoms { get; set; }
    [Required]
    public string Diagnosis { get; set; } = null!;
    [Required]
    public string Treatment { get; set; } = null!;
    public string? Notes { get; set; }
}