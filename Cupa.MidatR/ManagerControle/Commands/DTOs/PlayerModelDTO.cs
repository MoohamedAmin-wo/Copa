using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Cupa.MidatR.ManagerControle.Commands.DTOs;
public record PlayerModelDTO
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Nickname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    [Range(1, 99)]
    public int Number { get; set; }
    public string PositionId { get; set; }
    public IFormFile? ProfilePicture { get; set; }
    public IFormFile? ContractPicture { get; set; }
    public IFormFile? Video { get; set; }
    public DateOnly? ContractDuration { get; set; }
    public DateOnly? JoinedClubOn { get; set; }
    public DateOnly Birthdate { get; set; }
}