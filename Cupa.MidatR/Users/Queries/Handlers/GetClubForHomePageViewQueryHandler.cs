namespace Cupa.MidatR.Users.Queries.Handlers;
internal sealed class GetClubForHomePageViewQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetClubForHomePageViewQuery, IReadOnlyCollection<ClubForHomePageQueryModelDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<IReadOnlyCollection<ClubForHomePageQueryModelDTO>> Handle(GetClubForHomePageViewQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyCollection<Club> clubs;

        if (request.ForHome)
        {
            clubs = await _unitOfWork.clubs.GetAllAsync(predicate: x => !x.IsDeleted, stopTracking: true, take: 6);
        }

        else
        {
            clubs = await _unitOfWork.clubs.GetAllAsync(predicate: x => !x.IsDeleted, stopTracking: true, take: 0);
        }

        var returnedModel = clubs.Select(x => new ClubForHomePageQueryModelDTO
        {
            ClubName = x.ClubName,
            About = x.About,
            Id = x.Id,
            ClubPictureUrl = x.ClubPictureUrl,
            LogoUrl = x.LogoUrl
        });

        return [.. returnedModel];
    }
}