using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SmartWork.Wpf
{
    /// <summary>
    /// AllJobs.xaml 的交互逻辑
    /// </summary>
    public partial class AllJobs : UserControl
    {
        private string _deleteSelectedJobsTips = "删除选中的{0}项任务";

        public AllJobs()
        {
            InitializeComponent();

            LocalizeUI();

            dataPager.PageChanging += DataPager_PageChanging;
            dataPager.PageChanged += DataPager_PageChanged;
        }

        private void LocalizeUI()
        {
            btnSearch.Content = "搜索";
            btnDeleteAllJobs.Content = "删除所有任务";
            btnDeleteSelectedJobs.Content = string.Format(_deleteSelectedJobsTips, dataGridAllJobs.SelectedItems.Count);
        }

        private void DataPager_PageChanged(object sender, PageChangedEventArgs e)
        {

        }

        private bool GotoPage(int pageNum)
        {
            int offset = (pageNum - 1) * dataPager.PageSize;

            var bak = dataGridAllJobs.ItemsSource;

            try
            {
                List<Job> jobs = SqliteDataAccess.LoadJobs(dataPager.PageSize, offset);
                dataGridAllJobs.ItemsSource = jobs;

                return true;
            }
            catch (Exception ex)
            {
                Application.Current.MainWindow.Prompt(ex.Message, MessageType.Error);
                dataGridAllJobs.ItemsSource = bak;

                return false;
            }
        }

        private void RefreshDataGrid()
        {
            GotoPage(dataPager.PageIndex);

            dataPager.TotalCount = SqliteDataAccess.CountJobs();
        }

        private void DataPager_PageChanging(object sender, PageChangingEventArgs e)
        {
            if (!GotoPage(e.NewPageIndex))
            {
                e.IsCancel = true;
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
            bool? dialogResult = Application.Current.MainWindow.ShowDialog("确定删除这项任务？");

            if (dialogResult.HasValue&&dialogResult.Value)
            {
                Button btn = sender as Button;
                Job job = btn.DataContext as Job;

                try
                {
                    int result = SqliteDataAccess.DeleteJob(job);

                    if (result > 0)
                    {
                        Application.Current.MainWindow.Prompt("删除成功！", MessageType.Info);

                        RefreshDataGrid();
                    }
                    else
                    {
                        Application.Current.MainWindow.Prompt("删除失败！", MessageType.Error);
                    }

                }
                catch (Exception ex)
                {
                    Application.Current.MainWindow.Prompt(ex.Message, MessageType.Error);
                }
            }
        }

        private async void btnExecuteJob_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Job job = btn.DataContext as Job;

            string runJobResult = await JobRunner.Run(job.Script);

            Application.Current.MainWindow.Prompt(runJobResult, MessageType.Info);
        }


        private void btnEditJob_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Job job = btn.DataContext as Job;

            AppVariables.MainWindow?.NavigateTo("新建任务", job);
        }

        private void dataGridAllJobs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnDeleteSelectedJobs.Content = string.Format(_deleteSelectedJobsTips, dataGridAllJobs.SelectedItems.Count);
        }

        private void btnDeleteSelectedJobs_Click(object sender, RoutedEventArgs e)
        {
            dataGridAllJobs.SelectedItems.Cast<Job>().ToList().ForEach(job =>
            {
                SqliteDataAccess.DeleteJob(job);
            });

            RefreshDataGrid();
        }

        private void btnDeleteAllJobs_Click(object sender, RoutedEventArgs e)
        {
            int deletedCount = SqliteDataAccess.DeleteAllJobs();

            Application.Current.MainWindow.Prompt($"共删除了{deletedCount}条任务！", MessageType.Info);

            RefreshDataGrid();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchKey = txtSearchJobKeys.Text.Trim();

            if (string.IsNullOrEmpty(searchKey))
            {
                Application.Current.MainWindow.Prompt("请输入搜索关键字！多个之间用空格分隔。", MessageType.Warning);
                return;
            }

            string[] keys = searchKey.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);


        }
    }
}
