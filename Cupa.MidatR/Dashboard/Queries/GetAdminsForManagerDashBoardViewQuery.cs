
namespace Cupa.MidatR.Dashboard.Queries;
public sealed class GetAdminsForManagerDashBoardViewQuery(string userid, StatusType status = StatusType.Active) : IRequest<ICollection<AdminForDashBoardViewModelDTO>>
{
    public string UserId { get; } = userid;
    public StatusType Status { get; } = status;
}
