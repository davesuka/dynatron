using Microsoft.IdentityModel.Tokens;

namespace Dynatron.API.Commons
{
    public class BaseResponse<T> where T : class
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }
        public IEnumerable<ValidationFailure>? Errors { get; set; }
    }
}
