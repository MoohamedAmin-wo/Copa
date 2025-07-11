namespace Cupa.MidatR.Users.Queries.Handlers;
internal sealed class GetClubDetailsForPageViewQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetClubDetailsForPageViewQuery, ClubForDetailsPageQueryModelDTO>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<ClubForDetailsPageQueryModelDTO> Handle(GetClubDetailsForPageViewQuery request, CancellationToken cancellationToken)
    {
        var club = await _unitOfWork.clubs.FindSingleAsync(
            predicate: x => x.Id.Equals(request.CludId) && !x.IsDeleted);

        if (club is null)
            return null!;

        return new ClubForDetailsPageQueryModelDTO
        {
            ClubName = club.ClubName,
            ClubPictureUrl = club.ClubPictureUrl,
            LogoUrl = club.LogoUrl,
            MainShirtUrl = club.MainShirtUrl,
            Story = club.Story,
            PlayersCount = club.PlayersCount
        };
    }
}
