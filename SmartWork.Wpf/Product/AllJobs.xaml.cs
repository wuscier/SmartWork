using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartWork.Wpf
{
    /// <summary>
    /// AllJobs.xaml 的交互逻辑
    /// </summary>
    public partial class AllJobs : UserControl
    {
        public AllJobs()
        {
            InitializeComponent();
            dataPager.PageChanging += DataPager_PageChanging;
            dataPager.PageChanged += DataPager_PageChanged;
        }

        private void DataPager_PageChanged(object sender, PageChangedEventArgs e)
        {

        }

        private void DataPager_PageChanging(object sender, PageChangingEventArgs e)
        {
            int offset = (e.NewPageIndex - 1) * dataPager.PageSize;

            var bak = dataGridAllJobs.ItemsSource;

            try
            {
                List<Job> jobs = SqliteDataAccess.LoadJobs(dataPager.PageSize, offset);
                dataGridAllJobs.ItemsSource = jobs;
            }
            catch (Exception ex)
            {
                e.IsCancel = true;
                Application.Current.MainWindow.Prompt(ex.Message, MessageType.Error);
                dataGridAllJobs.ItemsSource = bak;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            List<Job> firstPageJobs = SqliteDataAccess.LoadJobs(30);

            dataGridAllJobs.ItemsSource = firstPageJobs;

            dataPager.TotalCount = SqliteDataAccess.CountJobs();
        }

        private void btnDeleteJob_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnEditJob_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Job job = btn.DataContext as Job;

            AppVariables.MainWindow?.NavigateTo("新建任务", job);
        }
    }
}
