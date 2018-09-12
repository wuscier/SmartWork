using System.Windows;

namespace SmartWork.Wpf
{
    public class PageChangedEventArgs : RoutedEventArgs
    {
        public int CurrentPageIndex { get; set; }
    }
}
