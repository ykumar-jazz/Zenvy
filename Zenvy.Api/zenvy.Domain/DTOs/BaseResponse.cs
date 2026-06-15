namespace zenvy.Domain.DTOs
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = null!;
    }
}