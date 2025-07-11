using Cupa.Application.Services.ClubServices;
using Humanizer;

namespace Cupa.MidatR.Dashboard.Queries.Handlers;
internal sealed class GetClubDetailsForManagerDashBoardViewQueryHandler(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IClubManagmentServices clubManagmentServices) : IRequestHandler<GetClubDetailsForManagerDashBoardViewQuery, ClubDataForDashBoardViewModelDTO>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IClubManagmentServices _clubManagmentServices = clubManagmentServices;

    public async Task<ClubDataForDashBoardViewModelDTO> Handle(GetClubDetailsForManagerDashBoardViewQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserId);
        if (user is null)
            return null!;

        var club = await _clubManagmentServices.GetCurrentClubAsync(user);
        if (club is null)
            return null!;

        var returnedModel = new ClubDataForDashBoardViewModelDTO
        {
            ClubName = club.ClubName,
            About = club.About,
            Story = club.Story,
            LogoUrl = club.LogoUrl,
            ClubPictureUrl = club.ClubPictureUrl,
            MainShirtUrl = club.MainShirtUrl,
            AdminsCount = club.AdminsCount,
            PlayersCount = club.PlayersCount,
            LastUpdatedOn = club.UpdatedOn.Humanize(),
            CreatedOn = club.CreatedOn.ToString("dd MMMM.yyyy")
        };

        return returnedModel;
    }
}