using System;
using System.Windows;

namespace SmartWork.Wpf
{
    /// <summary>
    /// SaveJob.xaml 的交互逻辑
    /// </summary>
    public partial class SaveJob : Window
    {
        private string _script = string.Empty;
        public SaveJob(string jobScript)
        {
            _script = jobScript;

            InitializeComponent();
            LocalizeUI();
        }

        private void LocalizeUI()
        {
            tbNameYourNewJob.Text = "给你的新建任务取个名字吧";
            btnSaveNewJob.Content = "保存";
        }

        private void btnSaveNewJob_Click(object sender, RoutedEventArgs e)
        {
            string newJobName = txtNewJobName.Text.Trim();

            if (string.IsNullOrEmpty(newJobName))
            {
                Application.Current.MainWindow.Prompt("请输入任务名字！", MessageType.Warning);
                return;
            }


            try
            {
                int saveJobResult = SqliteDataAccess.SaveJob(new Job()
                {
                    Description = newJobName,
                    Script = _script
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

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
