using Microsoft.AspNetCore.Identity;

namespace CareFlow.Models.Identity;

public class ApplicationUser : IdentityUser
{
    public bool IsActive { get; set; } = false;
}
