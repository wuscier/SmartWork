using System.Windows;

namespace SmartWork.Wpf
{
    /// <summary>
    /// DialogPrompt.xaml 的交互逻辑
    /// </summary>
    public partial class DialogPrompt : Window
    {
        public DialogPrompt()
        {
            InitializeComponent();
            LocalizeUI();
        }

        public DialogPrompt(string dialogMsg) : this()
        {
            tbDialogMessage.Text = dialogMsg;
        }

        private void LocalizeUI()
        {
            btnYes.Content = "是";
            btnNo.Content = "否";
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
