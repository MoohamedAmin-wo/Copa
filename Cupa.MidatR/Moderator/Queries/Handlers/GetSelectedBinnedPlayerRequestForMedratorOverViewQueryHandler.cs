using Humanizer;

namespace Cupa.MidatR.Moderator.Queries.Handlers;
internal sealed class GetSelectedBinnedPlayerRequestForMedratorOverViewQueryHandler(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork) : IRequestHandler<GetSelectedBinnedPlayerRequestForMedratorOverViewQuery, BinnedFreePlayerRequestForModeratorViewModelDTO>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<BinnedFreePlayerRequestForModeratorViewModelDTO> Handle(GetSelectedBinnedPlayerRequestForMedratorOverViewQuery request, CancellationToken cancellationToken)
    {
        var currentUser = await _userManager.FindByNameAsync(request.UserId);
        if (currentUser is null)
            return null!;


        if (!await _userManager.IsInRoleAsync(currentUser, CupaRoles.Moderator))
            return null!;

        var player = await _unitOfWork.player.FindSingleAsync(x => x.Id.Equals(request.PlayerId),
            includes : i => i.Include(u => u.User).Include(p => p.Position));
        if (player is null)
            return null!;


        var model = new BinnedFreePlayerRequestForModeratorViewModelDTO
        {
            Id = player.Id,
            Age = player.User.Age,
            Fullname = string.Concat(player.User.FirstName, " ", player.User.LastName),
            Nickname = player.NickName,
            Email = player.User.Email,
            Phone = player.User.PhoneNumber,
            Position = player.Position.PositionAPPR,
            CreateOn = player.CreatedOn.ToShortDateString(),
            UpdatedOn = player.UpdatedOn.Humanize(),
            ProfilePictureUrl = player.ProfilePictureUrl,
            Status = player.IsDeleted ? "Deleted" : "Active",
            StoryAbout = player.StoryAbout,
            PreefAbout = player.PreefAbout
        };

        var playerVideo = await _unitOfWork.video.FindSingleAsync(x =>x.PlayerId.Equals(player.Id));

        if(playerVideo is null)
        {
            model.VideoUrl = "no video uploaded !";
        }
        else
        {
            model.VideoUrl = playerVideo.Url;
        }

        return model;
    }
}
