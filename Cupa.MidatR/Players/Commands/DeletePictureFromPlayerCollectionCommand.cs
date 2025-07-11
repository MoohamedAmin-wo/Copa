namespace Cupa.MidatR.Players.Commands;
public sealed class DeletePictureFromPlayerCollectionCommand(string userid, string pictureuid) : IRequest<GlobalResponseDTO>
{
    public string UserId { get; } = userid;
    public string PictureUid { get; } = pictureuid;
}
