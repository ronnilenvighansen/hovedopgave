namespace MentorOnlineV3.Models.Entities
{
    public class Message
    {
        public int MessageId { get; set; }
        public string Text { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Topic { get; set; }

    }

}