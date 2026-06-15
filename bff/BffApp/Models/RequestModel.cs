namespace BffApp.Models
{
    public class RequestModel
    {
        public Payload? Payload { get; set; }
    }

    public class Payload
    {
        public string? Action { get; set; }
        public string? ResourceId { get; set; }
    }
}
