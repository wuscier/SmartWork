using System.Diagnostics;
using System.Threading.Tasks;

namespace SmartWork.Wpf
{
    public static class CmdRunner
    {
        public static async Task<string> Run(string cmd)
        {
            Process process = new Process()
            {
                StartInfo =
                {
                    CreateNoWindow = true,
                    FileName = "cmd.exe",
                    UseShellExecute=false,
                    WorkingDirectory = AppVariables.TempPath,
                    RedirectStandardError=true,
                    RedirectStandardInput=true,
                    RedirectStandardOutput=true,
                },

            };

            process.Start();
            await process.StandardInput.WriteLineAsync(cmd);
            await process.StandardInput.WriteLineAsync("exit");
            string output = await process.StandardOutput.ReadToEndAsync();
            process.Close();
            return output;
        }
    }
}
