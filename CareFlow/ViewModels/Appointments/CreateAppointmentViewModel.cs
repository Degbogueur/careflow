using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CareFlow.ViewModels.Appointments;

public class CreateAppointmentViewModel
{
    [DisplayName("Patient")]
    public int PatientId { get; set; }
    [DisplayName("Doctor")]
    public int DoctorId { get; set; }
    [Required]
    public DateTime Date { get; set; } = DateTime.Now;
    [Required]
    [DisplayName("Start time")]
    public TimeSpan? StartTime { get; set; }
    [Required]
    [DisplayName("End time")]
    public TimeSpan? EndTime { get; set; }
}
