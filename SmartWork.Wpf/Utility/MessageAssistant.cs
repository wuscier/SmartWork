using System.Windows;

namespace SmartWork.Wpf
{
    public static class WindowExtension
    {
        public static void Prompt(this Window parent, string message, MessageType messageType)
        {
            MessagePrompt messagePrompt = new MessagePrompt(message, messageType);
            messagePrompt.Owner = parent;
            messagePrompt.Show();
        }

        public static bool? ShowDialog(this Window parent, string dialogMsg)
        {
            DialogPrompt dialogPrompt = new DialogPrompt(dialogMsg);
            dialogPrompt.Owner = parent;
            return dialogPrompt.ShowDialog();
        }
    }
}
