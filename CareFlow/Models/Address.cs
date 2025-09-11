using Microsoft.EntityFrameworkCore;

namespace CareFlow.Models;

[Owned]
public class Address
{
    public string? Street { get; set; }
    public required string City { get; set; }
    public required string Province { get; set; }
    public required string Country { get; set; }
    public string? PostalCode { get; set; }
}
