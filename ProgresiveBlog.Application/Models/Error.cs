using ProgresiveBlog.Application.Enums;

namespace ProgresiveBlog.Application.Models
{
    public class Error
    {
        public ErrorCode Code { get; set; }
        public string Message { get; set; }
    }
}
