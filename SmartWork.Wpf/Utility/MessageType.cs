namespace SmartWork.Wpf
{
    public enum MessageType
    {
        Info,
        Warning,
        Error,
    }

    public static class MessageTypeExtension
    {
        public static string GetMessageTypeDescription(this MessageType messageType)
        {
            string description = string.Empty;

            switch (messageType)
            {
                case MessageType.Info:
                    description = "提示";
                    break;
                case MessageType.Warning:
                    description = "警告";
                    break;
                case MessageType.Error:
                    description = "错误";
                    break;
                default:
                    break;
            }

            return description;
        }
    }
}
