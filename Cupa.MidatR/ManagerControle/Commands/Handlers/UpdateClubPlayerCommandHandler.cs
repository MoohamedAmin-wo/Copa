namespace Cupa.MidatR.ManagerControle.Commands.Handlers;
internal sealed class UpdateClubPlayerCommandHandler : IRequestHandler<UpdateClubPlayerCommand, GlobalResponseDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFilesServices _filesServices;
    private readonly UserManager<ApplicationUser> _userManager;

    public UpdateClubPlayerCommandHandler(IUnitOfWork unitOfWork, IFilesServices filesServices, UserManager<ApplicationUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _filesServices = filesServices;
        _userManager = userManager;
    }

    public async Task<GlobalResponseDTO> Handle(UpdateClubPlayerCommand request, CancellationToken cancellationToken)
    {
        var currentUser = await _userManager.FindByNameAsync(request.UserId);
        if (currentUser is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UserNotFound };

        if (!await _userManager.IsInRoleAsync(currentUser, CupaRoles.Manager)
            || !await _userManager.IsInRoleAsync(currentUser, CupaRoles.Admin))
        {
            return new GlobalResponseDTO { Message = ErrorMessages.UnAuthorizedUser };

        }

        var currentPlayer = await _unitOfWork.player.FindSingleAsync(x => x.Id.Equals(request.PlayerId));
        if (currentPlayer is null)
            return new GlobalResponseDTO { Message = "unable to find this player !" };

        var clubPlayer = await _unitOfWork.clubPlayer.FindSingleAsync(x => x.PlayerId.Equals(currentPlayer.Id));
        if (clubPlayer is null)
            return new GlobalResponseDTO { Message = "unable to find this player !" };


        if (request.Model.Price < 0)
            return new GlobalResponseDTO { Message = "price for player can't be negative !" };


        var convertedPositionValue = int.TryParse(request.Model.PositionId, out int Value);
        if (!convertedPositionValue)
        {
            return new GlobalResponseDTO { Message = "please select a valid position !" };
        }

        var isValidPosition = await _unitOfWork.position.FindSingleAsync(x => x.Id.Equals(Value));
        if (isValidPosition is null)
        {
            return new GlobalResponseDTO { Message = "Position not found !" };
        }

        clubPlayer.Number = request.Model.Number;
        clubPlayer.Price = request.Model.Price;
        clubPlayer.IsAvaliableForSale = request.Model.IsAvaliableForSale;
        clubPlayer.ContractDuration = request.Model.ContractDurationTo.ToShortDateString();

        currentPlayer.PositionId = Value;
        currentPlayer.UpdatedOn = DateTime.UtcNow;
        currentPlayer.UpdatedBy = currentUser.Id;

        if (request.Model.ContractPicture != null)
        {
            var upload = await _filesServices.UploadFileAsync(request.Model.ContractPicture, "PlayersContracts");
            if (!upload.IsSuccess)
            {
                return new GlobalResponseDTO { Message = "faild to upload contract picture !" };
            }

            clubPlayer.ContractPictureUrl = upload.Url;
        }

        if (request.Model.Video != null)
        {
            var upload = await _filesServices.UploadFileAsync(request.Model.Video, "ClubPlayersVideos");
            if (!upload.IsSuccess)
            {
                return new GlobalResponseDTO { Message = "faild to upload Video !" };
            }

            var oldVideo = await _unitOfWork.video.FindSingleAsync(x => x.PlayerId.Equals(currentPlayer.Id));
            if (oldVideo is null)
            {
                var video = new Video
                {
                    Url = upload.Url,
                    CreatedBy = currentUser.Id,
                    CreatedOn = DateTime.UtcNow,
                    PlayerId = currentPlayer.Id
                };

                try
                {
                    await _unitOfWork.video.CreateAsync(video);
                }
                catch (Exception ex)
                {
                    return new GlobalResponseDTO { Message = $"faild to create new Video , Exception Details : {ex.Message}" };
                }
            }
            else
            {
                oldVideo.Url = upload.Url;
                oldVideo.UpdatedOn = DateTime.UtcNow;
                oldVideo.UpdatedBy = currentUser.Id;

                try
                {
                    await _unitOfWork.video.UpdateAsync(oldVideo);
                }
                catch (Exception ex)
                {
                    return new GlobalResponseDTO { Message = $"faild to update this Video , Exception Details : {ex.Message}" };
                }
            }
        }

        try
        {
            await _unitOfWork.clubPlayer.UpdateAsync(clubPlayer);
        }
        catch (Exception ex)
        {
            return new GlobalResponseDTO { Message = $"unable to update current player ! , Exception Details : {ex.Message}" };
        }

        try
        {
            await _unitOfWork.player.UpdateAsync(currentPlayer);
        }
        catch (Exception ex)
        {
            return new GlobalResponseDTO { Message = $"unable to update current player ! , Exception Details : {ex.Message}" };
        }

        await _unitOfWork.CommitAsync();

        return new GlobalResponseDTO { IsSuccess = true, Message = "Update complete successfully " };
    }
}
