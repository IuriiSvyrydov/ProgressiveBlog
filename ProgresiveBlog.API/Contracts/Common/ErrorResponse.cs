namespace ProgresiveBlog.API.Contracts.Common
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string StatusParse { get; set; }
        public List<string> Errrors { get; set; } = new();
        public DateTime TimeStamp { get; set; }
    }
}
