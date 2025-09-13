namespace CareFlow.ViewModels.Specialties;

public class UpdateSpecialtyViewModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}