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
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public int SpecialtyId { get; set; }

    public List<SelectListItem>? Specialties { get; set; }
}