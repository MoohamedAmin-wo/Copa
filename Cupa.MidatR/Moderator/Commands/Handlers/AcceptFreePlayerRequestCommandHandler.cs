namespace Cupa.MidatR.Moderator.Commands.Handlers;
internal sealed class AcceptFreePlayerRequestCommandHandler(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IEmailSender emailSender) : IRequestHandler<AcceptFreePlayerRequestCommand, GlobalResponseDTO>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IEmailSender _emailSender = emailSender;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<GlobalResponseDTO> Handle(AcceptFreePlayerRequestCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserID);
        if (user is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UserNotFound };

        if (!await _userManager.IsInRoleAsync(user, CupaRoles.Moderator))
            return new GlobalResponseDTO { Message = ErrorMessages.UnAuthorizedUser };

        var player = await _unitOfWork.player.FindSingleAsync(x => x.Id.Equals(request.PlayerId));
        if (player is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UnHandledServerError };

        var playerUser = await _unitOfWork.users.FindSingleAsync(x => x.Id.Equals(player.UserId));
        if (playerUser is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UnHandledServerError };


        if (request.Type.Equals(ApprovalType.Accept))
        {
            player.IsBinned = false;
            player.UpdatedOn = DateTime.UtcNow;
            player.UpdatedBy = user.Id;

            try
            {
                await _unitOfWork.player.UpdateAsync(player);
            }
            catch (Exception)
            {
                return new GlobalResponseDTO { Message = ErrorMessages.UnHandledServerError };
            }

            var result = await _userManager.AddToRoleAsync(playerUser, CupaRoles.Player);
            if (!result.Succeeded)
                return new GlobalResponseDTO { Message = string.Join(",", result.Errors.Select(x => x.Description)) };


            await _emailSender.SendEmailAsync(playerUser.Email!, "Congratulations",
           $"<h2>your request has been approved <h3><p>you are now in our Free players tab and can Edit your data from your profile </p></h3></h2>");
        }

        if (request.Type.Equals(ApprovalType.Decline))
        {
            var playerVideos = await _unitOfWork.video.FindSingleAsync(x => x.PlayerId.Equals(player.Id));
            var playerPictures = await _unitOfWork.pictures.FindSingleAsync(x => x.PlayerId.Equals(player.Id));

            try
            {
                await _unitOfWork.player.DeleteAsync(player);
            }
            catch (Exception)
            {
                return new GlobalResponseDTO { Message = ErrorMessages.UnHandledServerError };
            }

            if (await _userManager.IsInRoleAsync(playerUser , CupaRoles.Player))
            {
                await _userManager.RemoveFromRoleAsync(playerUser , CupaRoles.Player);
            }

            if (playerVideos != null)
                await _unitOfWork.video.DeleteAsync(playerVideos);

            if (playerPictures != null)
                await _unitOfWork.pictures.DeleteAsync(playerPictures);

            await _emailSender.SendEmailAsync(playerUser.Email!, "Unfortunately",
           $"<h2>your request has been Declined <h3><p>your data is not match our services , please check it and send again </p></h3></h2>");
        }

        await _unitOfWork.CommitAsync();
        return new GlobalResponseDTO { IsSuccess = true, Message = "Process Complete Successfully" };
    }
}
