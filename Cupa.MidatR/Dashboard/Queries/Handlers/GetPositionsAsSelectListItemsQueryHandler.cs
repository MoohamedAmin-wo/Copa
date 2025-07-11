using Microsoft.AspNetCore.Mvc.Rendering;
namespace Cupa.MidatR.Dashboard.Queries.Handlers;
internal sealed class GetPositionsAsSelectListItemsQueryHandler : IRequestHandler<GetPositionsAsSelectListItemsQuery, IEnumerable<SelectListItem>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPositionsAsSelectListItemsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<SelectListItem>> Handle(GetPositionsAsSelectListItemsQuery request, CancellationToken cancellationToken)
    {
        var positions = await _unitOfWork.position.GetAllAsync(take: 0);
        if (positions is null)
            return null!;

        var model = positions.Select(x => new SelectListItem
        {
            Value = x.Id.ToString(),
            Text = x.PositionName
        });

        return model;
    }
}