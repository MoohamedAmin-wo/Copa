namespace Cupa.MidatR.Users.Queries.DTOs;
public sealed record ManagerDetailsForClubPageQueryModelDTO
{
    public int Id { get; set; }
    public DateOnly AppointmentDate { get; set; }
    public DateOnly ContractEndsOn { get; set; }
    public string Fullname { get; set; }
    public int Age { get; set; }
    public string PictureUrl { get; set; }
    public string PictureThumbnailUrl { get; set; }
}
