namespace CareFlow.ViewModels.Doctors;

public class UpdateDoctorViewModel
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int SpecialtyId { get; set; }
}