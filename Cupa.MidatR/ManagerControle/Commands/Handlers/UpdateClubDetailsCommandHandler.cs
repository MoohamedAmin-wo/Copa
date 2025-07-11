namespace Cupa.MidatR.ManagerControle.Commands.Handlers;
internal sealed class UpdateClubDetailsCommandHandler(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork) : IRequestHandler<UpdateClubDetailsCommand, GlobalResponseDTO>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<GlobalResponseDTO> Handle(UpdateClubDetailsCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserId);
        if (user is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UserNotFound };

        if (!await _userManager.IsInRoleAsync(user, CupaRoles.Manager))
        {
            return new GlobalResponseDTO { Message = "Not authorized to take this action !" };
        }

        var manager = await _unitOfWork.managers.FindSingleAsync(x => x.UserId.Equals(user.Id));
        if (manager is null)
        {
            return new GlobalResponseDTO { Message = ErrorMessages.UnHandledServerError };
        }

        var club = await _unitOfWork.clubs.FindSingleAsync(x => x.ManagerId.Equals(manager.Id));
        if (club is null)
        {
            return new GlobalResponseDTO { Message = "Club un defined , Please create one first" };
        }

        // validate model 

        club.ClubName = request.Model.ClubName;
        club.About = request.Model.About;
        club.Story = request.Model.Story;
        club.UpdatedBy = user.Id;
        club.UpdatedOn = DateTime.UtcNow;

        try
        {
            await _unitOfWork.clubs.UpdateAsync(club);
        }
        catch (Exception ex)
        {
            return new GlobalResponseDTO { Message = $"Error updating this club , Exception Details : {ex.Message}" };
        }

        await _unitOfWork.CommitAsync();

        return new GlobalResponseDTO { IsSuccess = true, Message = "update Successfully " };
    }
}
