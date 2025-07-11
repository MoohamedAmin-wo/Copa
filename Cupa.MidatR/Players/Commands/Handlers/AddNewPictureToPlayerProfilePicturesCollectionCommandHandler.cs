namespace Cupa.MidatR.Players.Commands.Handlers;
internal sealed class AddNewPictureToPlayerProfilePicturesCollectionCommandHandler(IFilesServices filesServices, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : IRequestHandler<AddNewPictureToPlayerProfilePicturesCollectionCommand, GlobalResponseDTO>
{
    private readonly IFilesServices _filesServices = filesServices;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    public async Task<GlobalResponseDTO> Handle(AddNewPictureToPlayerProfilePicturesCollectionCommand request, CancellationToken cancellationToken)
    {
        var currentUser = await _userManager.FindByNameAsync(request.UserId);
        if (currentUser is null || !await _userManager.IsInRoleAsync(currentUser , CupaRoles.Player))
            return new GlobalResponseDTO { Message = ErrorMessages.UnAuthorizedUser };

        var currentPlayer = await _unitOfWork.player.FindSingleAsync(x => x.UserId.Equals(currentUser.Id));
        if (currentPlayer is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UnHandledServerError };

        var currentplayerPicturesCount = await _unitOfWork.pictures.GetCountAsync(x => x.PlayerId.Equals(currentPlayer.Id));
        if (currentplayerPicturesCount > 10)
            return new GlobalResponseDTO { Message = "not allowed to upload more than 10 pictures " };

        var uploadResult = await _filesServices.UploadFileAsync(request.File, "FreePlayersPictures");
        if (!uploadResult.IsSuccess)
            return new GlobalResponseDTO { Message = uploadResult.Message };

        var newPlayerPicture = new Picture
        {
            Url = uploadResult.Url,
            CreatedBy = currentUser.Id,
            PictureUid = uploadResult.Uid,
            ThumbnailUrl = uploadResult.ThumbnailUrl,
            PlayerId = currentPlayer.Id,
        };

        try
        {
            await _unitOfWork.pictures.CreateAsync(newPlayerPicture);
        }
        catch (Exception)
        {
            return new GlobalResponseDTO { Message = "Faild to upload !" };
        }

        await _unitOfWork.CommitAsync();

        return new GlobalResponseDTO { IsSuccess = true, Message = "Upload complete Successfully " };
    }
}