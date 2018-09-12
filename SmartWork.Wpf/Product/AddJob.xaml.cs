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
        public AddJob()
        {
            InitializeComponent();

            LocalizeUI();
        }

        public AddJob(string description, string script):this()
        {
            txtJobDescription.Text = description;
            txtJobScript.Text = script;
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


            string dateTime = DateTime.Now.ToString("HHmmssffff");

            string batchFileName = $"{dateTime}.bat";
            string vbsFileName = $"{dateTime}.vbs";

            string fullBatchFileName = Path.Combine(AppVariables.TempPath, batchFileName);
            string fullVbsFileName = Path.Combine(AppVariables.TempPath, vbsFileName);


            string vbsContent = string.Format(AppVariables.VBSSCRIPT, batchFileName);

            try
            {
                File.WriteAllText(fullBatchFileName, jobScript);

                File.WriteAllText(fullVbsFileName, vbsContent);

                string output = await CmdRunner.Run(vbsFileName);
                tbRunJobScriptResult.Text += output;

                //File.Delete(fullBatchFileName);
                //File.Delete(fullVbsFileName);

            }
            catch (Exception ex)
            {
                Application.Current.MainWindow.Prompt(ex.Message, MessageType.Error);
            }
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
            catch (Exception ex)
            {
                Application.Current.MainWindow.Prompt(ex.Message, MessageType.Error);
            }

        }
    }
}
