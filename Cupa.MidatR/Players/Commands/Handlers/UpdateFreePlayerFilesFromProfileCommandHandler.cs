namespace Cupa.MidatR.Players.Commands.Handlers;
internal sealed class UpdateFreePlayerFilesFromProfileCommandHandler(IFilesServices filesServices, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : IRequestHandler<UpdateFreePlayerFilesFromProfileCommand, GlobalResponseDTO>
{
    private readonly IFilesServices _filesServices = filesServices;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<GlobalResponseDTO> Handle(UpdateFreePlayerFilesFromProfileCommand request, CancellationToken cancellationToken)
    {
        var currentUser = await _userManager.FindByNameAsync(request.UserId);
        if (currentUser is null || !await _userManager.IsInRoleAsync(currentUser , CupaRoles.Player))
            return new GlobalResponseDTO { Message = ErrorMessages.UserNotFound };


        var currentPlayer = await _unitOfWork.player.FindSingleAsync(x => x.UserId.Equals(currentUser.Id));
        if (currentPlayer is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UnHandledServerError };


        if (request.Type.Equals(FileType.picture))
        {
            var uploadResult = await _filesServices.UploadFileAsync(request.File, "FreePlayersPictures");
            if (!uploadResult.IsSuccess)
                return new GlobalResponseDTO { Message = uploadResult.Message };

            currentPlayer.ProfilePictureUrl = uploadResult.Url;
        }

        if (request.Type.Equals(FileType.video))
        {
            var oldPlayerVideo = await _unitOfWork.video.FindSingleAsync(x => x.PlayerId.Equals(currentPlayer.Id));
            if (oldPlayerVideo != null)
            {
                await _unitOfWork.video.DeleteAsync(oldPlayerVideo);
            }

            var uploadResult = await _filesServices.UploadFileAsync(request.File, "FreePlayersVideos");
            if (!uploadResult.IsSuccess)
                return new GlobalResponseDTO { Message = uploadResult.Message };

            var newPlayerVideo = new Video
            {
                CreatedBy = currentUser.Id,
                PlayerId = currentPlayer.Id,
                Url = uploadResult.Url,
                VideoUid = uploadResult.Uid,
                CreatedOn = DateTime.UtcNow
            };

            await _unitOfWork.video.CreateAsync(newPlayerVideo);
        }

        await _unitOfWork.CommitAsync();

        return new GlobalResponseDTO { IsSuccess = true, Message = "Uploaded Successfully " };
    }
}
