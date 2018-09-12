using System.Windows;

namespace SmartWork.Wpf
{
    /// <summary>
    /// DemoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DemoWindow : Window
    {
        public DemoWindow()
        {
            InitializeComponent();

            LocalizeUI();
        }

        private void LocalizeUI()
        {
            btnGotoDragAndDropDemo.Content = "拖放示例";
        }

        private void btnGotoDragAndDropDemo_Click(object sender, RoutedEventArgs e)
        {
            gridContent.Children.Clear();
            gridContent.Children.Add(new DragAndDrop());
        }
    }
}
