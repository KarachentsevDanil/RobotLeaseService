namespace RLS.BLL.DTOs.Internal.Messages
{
    public class EmailMessage : BaseMessage
    {
        public string SendToEmail { get; set; }

        public string Subject { get; set; }
    }
}