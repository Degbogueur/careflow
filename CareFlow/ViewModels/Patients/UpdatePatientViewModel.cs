using CareFlow.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CareFlow.ViewModels.Patients;

public class UpdatePatientViewModel
{
    public int Id { get; set; }
    [Required]
    [DisplayName("First name")]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    [DisplayName("Last name")]
    public string LastName { get; set; } = string.Empty;
    [Required]
    public string Email { get; set; } = string.Empty;
    [DisplayName("Phone number")]
    public string? PhoneNumber { get; set; }
    [DisplayName("Date of birth")]
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public Address? Address { get; set; }
}
