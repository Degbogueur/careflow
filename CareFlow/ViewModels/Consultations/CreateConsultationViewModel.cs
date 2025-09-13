using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CareFlow.ViewModels.Consultations;

public class CreateConsultationViewModel
{
    public DateTime Date { get; set; } = DateTime.Now;
    public int PatientId { get; set; }
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

    public List<SelectListItem> Patients { get; set; } = [];
    public List<SelectListItem> Doctors { get; set; } = [];
}