using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace CareFlow.Models;

[Owned]
public class Address
{
    public string? Street { get; set; }
    public required string City { get; set; }
    public required string Province { get; set; }
    public required string Country { get; set; }
    [DisplayName("Postal code")]
    public string? PostalCode { get; set; }
}
