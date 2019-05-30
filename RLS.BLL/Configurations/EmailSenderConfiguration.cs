namespace RLS.BLL.Configurations
{
    public class EmailSenderConfiguration
    {
        public string SendFromEmail { get; set; }

        public string ApiKey { get; set; }

        public string NewMessageNotificationTemplate { get; set; }

        public string NewMessageNotificationSubject { get; set; }

        public string NewRobotMessageTemplate { get; set; }

        public string NewRobotMessageSubject { get; set; }
    }
}