using Humanizer;

namespace Cupa.MidatR.Users.Queries.Handlers;
internal sealed class GetSelectedPlayerFromUserQueryHandler(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork) : IRequestHandler<GetSelectedPlayerFromUserQuery, SelectedPlayerObjectForAuthointicatedUsersModelDTO>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<SelectedPlayerObjectForAuthointicatedUsersModelDTO> Handle(GetSelectedPlayerFromUserQuery request, CancellationToken cancellationToken)
    {
        var currentUser = await _userManager.FindByNameAsync(request.UserId);
        if (currentUser is null)
            return null!;

        var selectedPlayer = await _unitOfWork.player.FindSingleAsync(
            predicate: x => x.Id.Equals(request.PlayerId),
            includes: u => u.Include(i => i.User).Include(p => p.Position));

        if (selectedPlayer is null)
            return null!;

        var selectedPlayerVideo = await _unitOfWork.video.FindSingleAsync(x => x.PlayerId.Equals(selectedPlayer.Id));

        var selectedPlayerAddress = await _unitOfWork.address.FindSingleAsync(x => x.UserId.Equals(selectedPlayer.UserId));

        var selectedPlayerPictures = await _unitOfWork.pictures.FindMultipleAsync(x => x.PlayerId.Equals(selectedPlayer.Id));

        var model = new SelectedPlayerObjectForAuthointicatedUsersModelDTO
        {
            Fullname = string.Concat(selectedPlayer.User.FirstName, " ", selectedPlayer.User.LastName),
            Age = selectedPlayer.User.Age.ToString() ?? "Age not set",
            BirthDate = selectedPlayer.User.BirthDate.ToShortDateString() ?? "Birth date not set",
            Email = selectedPlayer.User.Email!,
            Phone = selectedPlayer.User.PhoneNumber!,
            ProfilePictureUrl = selectedPlayer.ProfilePictureUrl,
            Position = selectedPlayer.Position.PositionAPPR,
            Views = selectedPlayer.Views,
            NickName = selectedPlayer.NickName,
            StoryAbout = selectedPlayer.StoryAbout,
            PreefAbout = selectedPlayer.PreefAbout
        };

        if (selectedPlayerAddress is null)
        {
            model.Address = "Address not set ";
        }
        else
        {
            model.Address = selectedPlayerAddress.ToString();
        }

        if (selectedPlayerVideo != null)
        {
            model.Video = selectedPlayerVideo.Url;
        }
        else
        {
            model.Video = "Video not uploaded ";
        }

        if (selectedPlayerPictures != null)
        {
            model.Pictures = selectedPlayerPictures.Select(x => x.Url).ToList();
        }

        var isClubplayer = await IsClubPlayerAsync(selectedPlayer);
        if (isClubplayer != null)
        {
            var selectedClub = await _unitOfWork.clubs.FindSingleAsync(x => x.Id.Equals(isClubplayer.ClubId));
            if (selectedClub is null)
                return null!;

            model.Club = selectedClub.ClubName;
            model.Price = isClubplayer.Price.ToString() ?? "price not set";
            model.Number = isClubplayer.Number.ToString();
            model.ContractEndsOn = isClubplayer.ContractDuration;
            model.ShirtPictureUrl = isClubplayer.ShirtPictureUrl;
            model.IsAvaliableForSale = isClubplayer.IsAvaliableForSale;
            model.JoinedClubOn = isClubplayer.JoinedClubOn.Humanize();
        }

        selectedPlayer.Views++;

        try
        {
            await _unitOfWork.player.UpdateAsync(selectedPlayer);
        }
        catch (Exception)
        {
            return null!;
        }

        await _unitOfWork.CommitAsync();

        return model;
    }

    private async Task<ClubPlayer> IsClubPlayerAsync(Player player)
    {
        var clubPlayer = await _unitOfWork.clubPlayer.FindSingleAsync(x => x.PlayerId.Equals(player.Id));
        if (clubPlayer is null)
            return null!;

        return clubPlayer;
    }
}
