namespace CareFlow.ViewModels.Specialties;

public class SpecialtyViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int DoctorsCount { get; set; }
}
