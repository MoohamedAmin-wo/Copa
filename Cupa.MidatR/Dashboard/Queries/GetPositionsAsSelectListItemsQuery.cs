using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cupa.MidatR.Dashboard.Queries;
public sealed class GetPositionsAsSelectListItemsQuery : IRequest<IEnumerable<SelectListItem>>
{
}
