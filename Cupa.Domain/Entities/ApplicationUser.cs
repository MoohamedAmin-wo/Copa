using Cupa.Domain.Common;
using Cupa.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Cupa.Domain.Entities;
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? ProfilePictureUid { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public string? ProfilePictureThumbnailUrl { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsBlocked { get; set; }
    public UserGender Gender { get; set; } = 0;
    public string? UpdatedBy { get; set; }
    public DateOnly BirthDate { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public DateTime? LastUpdateForPassword { get; set; }
    public int Age => DateTime.Now.Year - BirthDate.Year;

    public Address? Address { get; set; }
    public Admin? Admin { get; set; }
    public Player? Player { get; set; }
    public Manager? Manager { get; set; }


    public List<RefreshToken>? RefreshTokens { get; set; }
}
