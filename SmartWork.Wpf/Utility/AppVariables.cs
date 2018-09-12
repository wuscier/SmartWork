using System.IO;
using System.Reflection;

namespace SmartWork.Wpf
{
    public static class AppVariables
    {
        private static string _appRootPath;
        public static string AppRootPath
        {
            get
            {
                if (string.IsNullOrEmpty(_appRootPath))
                {
                    Assembly assembly = Assembly.GetEntryAssembly();

                    _appRootPath = Path.GetDirectoryName(assembly.Location);
                }
                return _appRootPath;
            }
        }

        private static string _tempPath;
        public static string TempPath
        {
            get
            {
                if (string.IsNullOrEmpty(_tempPath))
                {
                    string temp = Path.Combine(AppRootPath, "temp");
                    if (!Directory.Exists(temp))
                    {
                        Directory.CreateDirectory(temp);
                    }

                    _tempPath = temp;
                }

                return _tempPath;
            }
        }

        public const string VBSSCRIPT = "Set ws = CreateObject(\"Wscript.Shell\")\r\nws.run \"cmd /c {0}\",vbhide";
    }
}
