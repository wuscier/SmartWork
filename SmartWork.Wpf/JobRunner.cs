using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWork.Wpf
{
    public class JobRunner
    {
        public static async Task<string> Run(string jobScript)
        {
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

                return output;
                //tbRunJobScriptResult.Text += output;

                //File.Delete(fullBatchFileName);
                //File.Delete(fullVbsFileName);

            }
            catch (Exception ex)
            {
                return ex.Message;
                //Application.Current.MainWindow.Prompt(ex.Message, MessageType.Error);
            }
        }
    }
}
