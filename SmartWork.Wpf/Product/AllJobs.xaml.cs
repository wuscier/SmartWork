﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

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

                        GotoPage(dataPager.PageIndex);

                        dataPager.TotalCount = SqliteDataAccess.CountJobs();
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

        private void btnEditJob_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Job job = btn.DataContext as Job;

            AppVariables.MainWindow?.NavigateTo("新建任务", job);
        }
    }
}
