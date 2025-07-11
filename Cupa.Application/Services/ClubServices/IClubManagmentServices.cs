namespace Cupa.Application.Services.ClubServices
{
    public interface IClubManagmentServices
    {
        Task<Club> GetCurrentClubAsync(ApplicationUser user);
    }
}
