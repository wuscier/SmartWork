using System.Windows;

namespace SmartWork.Wpf
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            LocalizeUI();
        }

        private void LocalizeUI()
        {
            btnNewJob.Content = "新建任务";
        }

        private void btnNewJob_Click(object sender, RoutedEventArgs e)
        {
            gridContent.Children.Clear();
            gridContent.Children.Add(new AddJob());
        }
    }
}
