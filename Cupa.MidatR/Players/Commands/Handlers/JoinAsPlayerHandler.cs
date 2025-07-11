using Cupa.Domain.AppExtenssions;
namespace Cupa.MidatR.Players.Commands.Handlers;
internal sealed class JoinAsPlayerHandler(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IFilesServices filesServices) : IRequestHandler<JoinAsPlayerCommand, GlobalResponseDTO>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IFilesServices _filesServices = filesServices;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<GlobalResponseDTO> Handle(JoinAsPlayerCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserId);
        if (user is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UserNotFound };

        if (!user.EmailConfirmed)
            return new GlobalResponseDTO { Message = "Confirm your email first !" };

        if (await _userManager.IsInRoleAsync(user, CupaRoles.Player))
            return new GlobalResponseDTO { Message = "you are already a player , please check your profile !" };

        if (
            await _userManager.IsInRoleAsync(user, CupaRoles.Admin) ||
            await _userManager.IsInRoleAsync(user, CupaRoles.Manager) ||
            await _userManager.IsInRoleAsync(user, CupaRoles.Moderator))
        {
            return new GlobalResponseDTO { Message = "you can't join as player " };
        }

        if (!await _unitOfWork.position.HasAnyDataAsync())
            return new GlobalResponseDTO { Message = ErrorMessages.UnHandledServerError };

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

        if (request.Model.PhoneNumber is null)
        {
            return new GlobalResponseDTO { Message = "can't add empty value to phone number !s" };
        }

        var player = new Player
        {
            CreatedBy = user.Id,
            IsFree = true,
            UserId = user.Id,
            StoryAbout = request.Model.StoryAbout,
            PreefAbout = request.Model.PreefAbout,
            IsBinned = true,
            IsDeleted = false,
            NickName = request.Model.NickName,
            Views = 0,
            Rate = 1,
            PositionId = Value,
        };

        user.PhoneNumber ??= request.Model.PhoneNumber;

        try
        {
            await _unitOfWork.player.CreateAsync(player);
        }
        catch (Exception ex)
        {
            return new GlobalResponseDTO { Message = $"faild to create this entity , \nException Details{ex.Message}" };
        }

        await _unitOfWork.CommitAsync();

        if (request.Model.Picture != null)
        {
            if (!request.Model.Picture.IsPictureExtensionValid())
            {
                return new GlobalResponseDTO { Message = ErrorMessages.NotValidPictureExtensions };
            }

            var uploadPictureResult = await _filesServices.UploadFileAsync(request.Model.Picture, "FreePlayersPictures");
            if (!uploadPictureResult.IsSuccess || string.IsNullOrEmpty(uploadPictureResult.Url))
                return new GlobalResponseDTO { Message = uploadPictureResult.Message ?? "Upload failed with missing data." };

            player.ProfilePictureUrl = uploadPictureResult.Url;
        }

        if (request.Model.Video != null)
        {
            if (!request.Model.Video.IsVideoExtensionValid())
            {
                return new GlobalResponseDTO { Message = ErrorMessages.NotValidVideoExtensions };
            }

            var uploadVideoResult = await _filesServices.UploadFileAsync(request.Model.Video, "FreePlayersVideos");
            if (!uploadVideoResult.IsSuccess || string.IsNullOrEmpty(uploadVideoResult.Url))
                return new GlobalResponseDTO { Message = uploadVideoResult.Message ?? "Upload failed with missing data." };

            var vid = new Video
            {
                PlayerId = player.Id,
                CreatedBy = user.Id,
                IsDeleted = false,
                Url = uploadVideoResult.Url,
                VideoUid = uploadVideoResult.Uid
            };

            try
            {
                await _unitOfWork.video.CreateAsync(vid);
            }
            catch (Exception)
            {
                return new GlobalResponseDTO { Message = "unable to upload video now , please check your internet connection !" };
            }
        }

        await _unitOfWork.CommitAsync();
        return new GlobalResponseDTO { IsSuccess = true, Message = "Congratulations for Join us as a new player , \n your request is under processing . wait a message in your inbox Mail for approval " };
    }
}