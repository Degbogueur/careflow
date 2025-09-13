using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CareFlow.ViewModels.Appointments;

public class CreateAppointmentViewModel
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    [Required]
    public DateTime? Date { get; set; }
    [Required]
    public TimeSpan? StartTime { get; set; }
    [Required]
    public TimeSpan? EndTime { get; set; }

    public List<SelectListItem> Patients { get; set; } = [];
    public List<SelectListItem> Doctors { get; set; } = [];
}
