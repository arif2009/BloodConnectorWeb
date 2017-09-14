using System;
using System.IO;
using System.Text;

namespace BloodConnector.Shared.Log
{
    public class Logger
    {
        private readonly string _logDir = System.Web.HttpContext.Current.Server.MapPath("~/Log");
        private string logFile = "Exception_{0}_{1}_{2}.txt";
        private string logFilePath;

        private Logger()
        {
            if (!Directory.Exists(_logDir))
                Directory.CreateDirectory(_logDir);

            logFilePath = String.Format(Path.Combine(_logDir, logFile), DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        }

        private void LogWrite(string message)
        {
            using (var fs = new FileStream(logFilePath, FileMode.Append))
            {
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                fs.Write(buffer, 0, buffer.Length);
            }
        }

        public static void Log(string message)
        {
            var sb = new StringBuilder();
            sb.Append("===========Start===============");
            sb.AppendLine();
            sb.Append(message);
            sb.AppendLine();
            sb.Append("============End==============");
            sb.AppendLine();
            sb.AppendLine();
            new Logger().LogWrite(sb.ToString());
        }

        public static void Log(Exception ex)
        {

            var sb = new StringBuilder();
            sb.AppendFormat("===============Exception Occuurred at {0}===================", DateTime.Now.ToShortTimeString());
            sb.AppendLine();
            sb.AppendLine();

            sb.Append("Message");
            sb.AppendLine();
            sb.AppendLine("-----------");
            sb.AppendLine();
            sb.Append(ex.Message);

            sb.AppendLine();
            sb.AppendLine();


            sb.Append("StackTrace");
            sb.AppendLine();
            sb.AppendLine("-----------");
            sb.AppendLine();
            sb.Append(ex.StackTrace);
            sb.AppendLine();
            sb.AppendLine();

            sb.Append("===============End===================");

            new Logger().LogWrite(sb.ToString());
        }
    }
}
