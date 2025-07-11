namespace Cupa.MidatR.Auth.Commands
{
    public sealed class RequestCodeCommand(RequestCodeModelDTO model, CodeType requestType) : IRequest<GlobalResponseDTO>
    {
        public RequestCodeModelDTO Model { get; } = model;
        public CodeType RequestType { get; } = requestType;
    }
}
