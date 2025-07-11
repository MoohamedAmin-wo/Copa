using Humanizer;

namespace Cupa.MidatR.Dashboard.Queries.Handlers;
internal sealed class GetSelectedPlayerForDashBoardQueryHandler(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : IRequestHandler<GetSelectedPlayerForDashBoardQuery, PlayerViewForManagerModelDTO>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<PlayerViewForManagerModelDTO> Handle(GetSelectedPlayerForDashBoardQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserId);
        if (user is null)
            return null!;

        var player = await _unitOfWork.clubPlayer.FindSingleAsync(x => x.PlayerId.Equals(request.PlayerId),
            includes:
            x => x
            .Include(u => u.Player)
            .ThenInclude(x => x.User)
            .Include(i => i.Player)
            .ThenInclude(n => n.Position));

        if (player is null)
            return null!;

        var clubPlayer = await _unitOfWork.clubPlayer.FindSingleAsync(x => x.PlayerId.Equals(player.Id));

        var model = new PlayerViewForManagerModelDTO
        {
            Fullname = string.Concat(player.Player.User.FirstName, " ", player.Player.User.LastName),
            Email = player.Player.User.Email,
            Phone = player.Player.User.PhoneNumber,
            Age = player.Player.User.Age,
            Position = player.Player.Position.PositionName,
            ContratctDuration = player.ContractDuration.Humanize(),
            ContractPictureUrl = player.ContractPictureUrl,
            IsAvaliableForSale = player.IsAvaliableForSale ? "Avaliable" : "Not avaliable",
            Status = player.Player.IsDeleted ? "Deleted" : "Active",
            ProfilePictureUrl = player.Player.ProfilePictureUrl,
            ShirtPictureUrl = player.ShirtPictureUrl,
            Number = player.Number,
            Price = player.Price.ToString() ?? "Price not set",
            JoinedClubOn = player.JoinedClubOn
        };

        var PlayerAddress = await _unitOfWork.address.FindSingleAsync(x => x.UserId.Equals(player.Player.User.Id));
        if (PlayerAddress is null)
            model.Address = "Address not set ";
        else
            model.Address = PlayerAddress.ToString();


        var playerPictures = await _unitOfWork.pictures.FindMultipleAsync(x => x.PlayerId!.Equals(player.Id));
        if (playerPictures is null)
        {
            model.PlayerPictures = [];
        }
        else
        {
            model.PlayerPictures = [.. playerPictures.Select(x => x.Url)];
        }

        var playerVideo = await _unitOfWork.video.FindSingleAsync(x => x.PlayerId.Equals(player.PlayerId));
        if (playerVideo is null)
        {
            model.VideoUrl = "No uploaded videos found";
        }
        else
        {
            model.VideoUrl = playerVideo.Url;
        }

        return model;
    }
}
