namespace Read_Planet.Models
{
    public class Message
    {
        public Message(string subject, IList<string> to, string body)
        {
            Subject = subject;
            To = to;
            Body = body;
        }

        public string Subject { get; init; }
        public IList<string> To { get; init; }
        public string Body { get; init; }
    }
}
