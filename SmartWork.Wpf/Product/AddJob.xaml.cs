using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace SmartWork.Wpf
{
    /// <summary>
    /// AddJob.xaml 的交互逻辑
    /// </summary>
    public partial class AddJob : UserControl
    {
        private int _jobId = 0;

        public AddJob()
        {
            InitializeComponent();

            LocalizeUI();
        }

        public AddJob(Job job):this()
        {
            _jobId = job.Id;
            txtJobDescription.Text = job.Description;
            txtJobScript.Text = job.Script;
        }

        private void LocalizeUI()
        {
            tbJobDescription.Text = "任务描述";
            btnRunJobScript.Content = "运行任务";
            btnSaveJob.Content = "保存任务";
        }

        private async void btnRunJobScript_Click(object sender, RoutedEventArgs e)
        {
            string jobScript = txtJobScript.Text.Trim();

            if (string.IsNullOrEmpty(jobScript))
            {
                Application.Current.MainWindow.Prompt("请输入任务脚本！", MessageType.Warning);
                return;
            }

            string runJobResult = await JobRunner.Run(jobScript);

            tbRunJobScriptResult.Text += runJobResult;
        }

        private void btnSaveJob_Click(object sender, RoutedEventArgs e)
        {
            string jobScript = txtJobScript.Text.Trim();

            if (string.IsNullOrEmpty(jobScript))
            {
                Application.Current.MainWindow.Prompt("请输入任务脚本！", MessageType.Warning);
                return;
            }

            string jobDescription = txtJobDescription.Text.Trim();
            if (string.IsNullOrEmpty(jobDescription))
            {
                Application.Current.MainWindow.Prompt("请输入任务描述！", MessageType.Warning);
                return;
            }


            try
            {
                if (_jobId >0)
                {
                    int updateJobResult = SqliteDataAccess.UpdateJob(new Job()
                    {
                        Id = _jobId,
                        Description = jobDescription,
                        Script = jobScript
                    });

                    if (updateJobResult > 0)
                    {
                        Application.Current.MainWindow.Prompt("更新成功！", MessageType.Info);
                    }
                    else
                    {
                        Application.Current.MainWindow.Prompt("更新失败！", MessageType.Error);
                    }
                }
                else
                {
                    int saveJobResult = SqliteDataAccess.SaveJob(new Job()
                    {
                        Description = jobDescription,
                        Script = jobScript
                    });

                    if (saveJobResult > 0)
                    {
                        Application.Current.MainWindow.Prompt("保存成功！", MessageType.Info);
                    }
                    else
                    {
                        Application.Current.MainWindow.Prompt("保存失败！", MessageType.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                Application.Current.MainWindow.Prompt(ex.Message, MessageType.Error);
            }

        }
    }
}
