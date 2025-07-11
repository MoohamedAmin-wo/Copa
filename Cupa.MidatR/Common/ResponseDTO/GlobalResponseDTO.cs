namespace Cupa.MidatR.Common.ResponseDTO
{
    public sealed record GlobalResponseDTO
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
