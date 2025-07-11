namespace Cupa.MidatR.Users.Commands.Handlers;
internal sealed class UpdateUserProfilePictureCommandHandler : IRequestHandler<UpdateUserProfilePictureCommand, GlobalResponseDTO>
{
    private readonly IFilesServices _filesServices;
    private readonly UserManager<ApplicationUser> _userManager;
    public UpdateUserProfilePictureCommandHandler(UserManager<ApplicationUser> userManager, IFilesServices filesServices)
    {
        _userManager = userManager;
        _filesServices = filesServices;
    }

    public async Task<GlobalResponseDTO> Handle(UpdateUserProfilePictureCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserId);
        if (user is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UserNotFound };

        if (request.Picture is null)
            return new GlobalResponseDTO { Message = ErrorMessages.EmptyImage };

        var updateProfilePictureResult = await _filesServices.UploadFileAsync(request.Picture, "UsersProfilePictures");
        if (!updateProfilePictureResult.IsSuccess)
            return new GlobalResponseDTO { Message = updateProfilePictureResult.Message };

        //if(user.ProfilePictureUid != null)
        //{
        //    var deleteResult = await _filesServices.DeleteFileAsync(user.ProfilePictureUid);
        //    if (!deleteResult.IsSuccess)
        //        return new GlobalResponseDTO() { Message = deleteResult.Message};
        //}

        user.ProfilePictureThumbnailUrl = updateProfilePictureResult.ThumbnailUrl;
        user.ProfilePictureUrl = updateProfilePictureResult.Url;
        user.ProfilePictureUid = updateProfilePictureResult.Uid;
        user.UpdatedOn = DateTime.UtcNow;
        user.UpdatedBy = request.UserId;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            return new GlobalResponseDTO { Message = ErrorMessages.UnHandledServerError };

        return new GlobalResponseDTO { IsSuccess = true, Message = "image uploaded Successfully " };
    }
}
