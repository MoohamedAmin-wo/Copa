using Cupa.MidatR.Players.Queries.DTOs;
namespace Cupa.MidatR.Players.Queries.Handlers;
internal sealed class GetFreePlayerDataForProfileViewQueryHandler(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork) : IRequestHandler<GetFreePlayerDataForProfileViewQuery, FreePlayerObjectForProfileModelDTO>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<FreePlayerObjectForProfileModelDTO> Handle(GetFreePlayerDataForProfileViewQuery request, CancellationToken cancellationToken)
    {
        var currentUser = await _userManager.FindByNameAsync(request.UserId);
        if (currentUser is null || !await _userManager.IsInRoleAsync(currentUser, CupaRoles.Player))
            return null!;

        var currentPlayer = await _unitOfWork.player.FindSingleAsync(x =>x.UserId.Equals(currentUser.Id) ,
            includes : i => i.Include(u => u.User).Include(p => p.Position));

        if (currentPlayer is null)
            return null!;

        
        var returnedModel = new FreePlayerObjectForProfileModelDTO
        {
            NickName = currentPlayer.NickName,
            PictureUrl = currentPlayer.ProfilePictureUrl,
            Position = currentPlayer.Position.PositionName,
            PreefAbout = currentPlayer.PreefAbout,
            StoryAbout = currentPlayer.StoryAbout,
            Views = currentPlayer.Views,
        };

        var video = await _unitOfWork.video.FindSingleAsync(x => x.PlayerId.Equals(currentPlayer.Id));
        if(video is null)
        {
            returnedModel.VideoUrl = "No uploaded video !";
        }
        else
        {
            returnedModel.VideoUrl = video.Url;
        }
        var pictures = await _unitOfWork.pictures.GetAllAsync(x => x.PlayerId.Equals(currentPlayer.Id) , take : 10 , stopTracking : true);
        if(pictures.Count == 0)
        {
            returnedModel.Pictures = [];
        }
        else
        {
            returnedModel.Pictures = pictures.Select(x => x.Url).ToList();

        }

        return returnedModel;
    }
}