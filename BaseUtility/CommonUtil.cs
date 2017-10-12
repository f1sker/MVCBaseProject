using System;
using System.Configuration;
using System.IO;
using System.Text;

namespace BaseUtility
{
    public class CommonUtil
    {
        private static bool UseLog = ConfigurationManager.AppSettings["USELOG"] == "Y" ? true : false;
        private static bool UseDbLog = ConfigurationManager.AppSettings["USEDBLOG"] == "Y" ? true : false;
        private static string LogDir = ConfigurationManager.AppSettings["LOGDIR"];

        public static void WriteLog(string log)
        {
            if (UseLog)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Format("{0} : {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ms"), log));

                try
                {
                    string dirPath = LogDir + "\\local_client\\";
                    string filePath = dirPath + string.Format("log_{0}", DateTime.Now.ToString("yyyyMMdd"));

                    DirectoryInfo di = new DirectoryInfo(dirPath);
                    if (di.Exists == false)
                        di.Create();

                    FileInfo fi = new FileInfo(filePath);
                    StreamWriter sw = fi.Exists ? File.AppendText(filePath) : new StreamWriter(filePath);
                    sw.WriteLine(sb.ToString());
                    sw.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


    }
}
