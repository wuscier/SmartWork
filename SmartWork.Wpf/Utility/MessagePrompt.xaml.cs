using System;
using System.Threading;
using System.Windows;

namespace SmartWork.Wpf
{
    /// <summary>
    /// MessagePrompt.xaml 的交互逻辑
    /// </summary>
    public partial class MessagePrompt : Window
    {
        private Timer _timer;

        public MessagePrompt()
        {
            InitializeComponent();
        }

        public MessagePrompt(string message, MessageType messageType, int autoClosePeriod = 3000)
        {
            InitializeComponent();

            tbMessage.Text = message;
            tbMessageType.Text = messageType.GetMessageTypeDescription();

            _timer = new Timer(AutoClose, null, Timeout.Infinite, Timeout.Infinite);
            _timer.Change(autoClosePeriod, Timeout.Infinite);
        }

        private void AutoClose(object state)
        {
            try
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Close();
                }));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
