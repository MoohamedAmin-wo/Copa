namespace Cupa.Application.Common.Response;
public sealed record DeleteResponse
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
}