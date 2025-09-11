namespace CareFlow.Models;

public class Specialty
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    public ICollection<Doctor> Doctors { get; set; } = [];
}