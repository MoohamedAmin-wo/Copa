namespace Cupa.MidatR.ManagerControle.Commands.Handlers;
internal sealed class UpdateClubFilesCommandHandler : IRequestHandler<UpdateClubFilesCommand, GlobalResponseDTO>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IFilesServices _filesServices;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateClubFilesCommandHandler(UserManager<ApplicationUser> userManager, IFilesServices filesServices, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _filesServices = filesServices;
        _unitOfWork = unitOfWork;
    }

    public async Task<GlobalResponseDTO> Handle(UpdateClubFilesCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserId);
        if (user is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UserNotFound };

        if (!await _userManager.IsInRoleAsync(user, CupaRoles.Manager))
        {
            return new GlobalResponseDTO { Message = "not authorized to take this action !" };
        }

        var manager = await _unitOfWork.managers.FindSingleAsync(x => x.UserId.Equals(user.Id));
        if (manager is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UnHandledServerError };

        var club = await _unitOfWork.clubs.FindSingleAsync(x => x.ManagerId.Equals(manager.Id));
        if (club is null)
        {
            return new GlobalResponseDTO { Message = "club not found , try to create one first " };
        }

        var upload = await _filesServices.UploadFileAsync(request.Model.File, "");
        if (!upload.IsSuccess)
        {
            return new GlobalResponseDTO { Message = "faild to upload this file" };
        }

        switch (request.FileType)
        {
            case ClubFileType.Logo:
                club.LogoUrl = upload.Url;
                break;
            case ClubFileType.MainShirt:
                club.MainShirtUrl = upload.Url;
                break;
            case ClubFileType.ClubPicture:
                club.ClubPictureUrl = upload.Url;
                break;
        }

        club.UpdatedBy = user.Id;
        club.UpdatedOn = DateTime.UtcNow;

        try
        {
            await _unitOfWork.clubs.UpdateAsync(club);
        }
        catch (Exception ex)
        {
            return new GlobalResponseDTO { Message = $"Failed to save changes for this entity ! , Exception Details : {ex.Message}" };
        }

        await _unitOfWork.CommitAsync();

        return new GlobalResponseDTO { IsSuccess = true, Message = "Upload Successfully !" };
    }
}
