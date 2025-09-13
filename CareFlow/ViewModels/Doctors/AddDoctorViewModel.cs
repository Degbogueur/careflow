using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CareFlow.ViewModels.Doctors;

public class AddDoctorViewModel
{
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    public int SpecialtyId { get; set; }

    public List<SelectListItem>? Specialties { get; set; }
}