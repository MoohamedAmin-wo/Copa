namespace Cupa.MidatR.Users.Queries.Handlers;
internal sealed class GetSuccessStoriesPageViewQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetSuccessStoriesPageViewQuery, IReadOnlyCollection<SuccessStoriesForHomePageViewModelDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<IReadOnlyCollection<SuccessStoriesForHomePageViewModelDTO>> Handle(GetSuccessStoriesPageViewQuery request, CancellationToken cancellationToken)
    {
        var successStorires = await _unitOfWork.successStrories.GetAllAsync(stopTracking: true, take: 0);
        var returnedModel = successStorires.Select(x => new SuccessStoriesForHomePageViewModelDTO
        {
            Age = x.Age,
            Fullname = x.Fullname,
            DateOfBirth = x.DateOfBirth,
            VideoUrl = x.VideoUrl,
            PictureUrl = x.PictureUrl,
            Position = x.Position
        });

        return [.. returnedModel];
    }
}