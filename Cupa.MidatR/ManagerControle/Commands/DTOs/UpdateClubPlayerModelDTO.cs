using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Cupa.MidatR.ManagerControle.Commands.DTOs;
public sealed record UpdateClubPlayerModelDTO
{
    [Range(1, 99)]
    public int Number { get; set; }
    public string PositionId { get; set; }
    public double Price { get; set; }
    public bool IsAvaliableForSale { get; set; }
    public DateOnly ContractDurationTo { get; set; }
    public IFormFile? ContractPicture { get; set; }
    public IFormFile? Video { get; set; }
}
