namespace Cupa.MidatR.ManagerControle.Queries.Handlers;
sealed class GetActiveAdminsQueryHandler(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : IRequestHandler<GetActiveAdminsQuery, IReadOnlyCollection<AdminQueryModelDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    public async Task<IReadOnlyCollection<AdminQueryModelDTO>> Handle(GetActiveAdminsQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserId);
        if (user is null) { return null!; }

        var manager = await _unitOfWork.managers.FindSingleAsync(x => x.UserId.Equals(user.Id));
        if (manager is null) { return null!; }

        var club = await _unitOfWork.clubs.FindSingleAsync(x => x.ManagerId.Equals(manager.Id));
        if (club is null) { return null!; }

        var clubPlayers = await _unitOfWork.admins.GetAllAsync(includes: u => u.Include(x => x.User), take: 0);

        ICollection<AdminQueryModelDTO> returnModel = null;

        return [.. returnModel];
    }
}
