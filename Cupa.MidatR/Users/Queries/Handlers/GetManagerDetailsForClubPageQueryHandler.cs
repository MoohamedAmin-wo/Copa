namespace Cupa.MidatR.Users.Queries.Handlers;
internal sealed class GetManagerDetailsForClubPageQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetManagerDetailsForClubPageQuery, ManagerDetailsForClubPageQueryModelDTO>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ManagerDetailsForClubPageQueryModelDTO> Handle(GetManagerDetailsForClubPageQuery request, CancellationToken cancellationToken)
    {
        var club = await _unitOfWork.clubs.FindSingleAsync(predicate: x => x.Id.Equals(request.ClubId));
        if (club is null)
            return null!;

        var manager = await _unitOfWork.managers.FindSingleAsync(
         predicate: x => x.Id.Equals(club.ManagerId)
        , includes: i => i.Include(u => u.User));

        if (manager is null)
            return null!;

        ManagerDetailsForClubPageQueryModelDTO model = new()
        {
            Id = manager.Id,
            Fullname = string.Concat(manager.User.FirstName, " ", manager.User.LastName),
            AppointmentDate = manager.AppointmentDate,
            ContractEndsOn = manager.ContractEndsOn,
            Age = manager.User.Age,
            PictureUrl = manager.User.ProfilePictureUrl,
            PictureThumbnailUrl = manager.User.ProfilePictureThumbnailUrl
        };

        return model;
    }
}
