namespace Cupa.MidatR.Players.Commands.Handlers;
internal sealed class UpdateFreePlayerDataFromProfileCommandHandler(IUnitOfWork unitOfWork, IAuthService authService, UserManager<ApplicationUser> userManager) : IRequestHandler<UpdateFreePlayerDataFromProfileCommand, GlobalResponseDTO>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    public async Task<GlobalResponseDTO> Handle(UpdateFreePlayerDataFromProfileCommand request, CancellationToken cancellationToken)
    {
        var currentUser = await _userManager.FindByNameAsync(request.UserId);
        if (currentUser is null || !await _userManager.IsInRoleAsync(currentUser, CupaRoles.Player))
            return new GlobalResponseDTO { Message = ErrorMessages.UnAuthorizedUser };

        var currentPlayer = await _unitOfWork.player.FindSingleAsync(x => x.UserId.Equals(currentUser.Id));
        if (currentPlayer is null)
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

        currentPlayer.StoryAbout = request.Model.StoryAbout;
        currentPlayer.PreefAbout = request.Model.PreefAbout;
        currentPlayer.NickName = request.Model.NickName;
        currentPlayer.PositionId = Value;

        try
        {
            await _unitOfWork.player.UpdateAsync(currentPlayer);
        }
        catch (Exception)
        {
            return new GlobalResponseDTO { Message = ErrorMessages.UnableToUpdateCurrentRecord };
        }

        await _unitOfWork.CommitAsync();
        return new GlobalResponseDTO { IsSuccess = true, Message = ErrorMessages.SuccessfullUpdates };
    }
}