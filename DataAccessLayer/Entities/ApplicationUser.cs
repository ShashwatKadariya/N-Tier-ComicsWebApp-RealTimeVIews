using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Entities;

public class ApplicationUser: IdentityUser
{
    [Required]
    public string userName { get; set; }
}