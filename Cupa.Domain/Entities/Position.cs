namespace Cupa.Domain.Entities;
public class Position
{
    public int Id { get; set; }
    public string PositionName { get; set; } = null!;
    public string PositionAPPR { get; set; } = null!;
    public List<Player> Players { get; set; } = new List<Player>();
}