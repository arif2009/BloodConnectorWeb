using System;
using System.Text;
using System.Web;

namespace BloodConnector.Shared.Log
{
    public static class Logger
    {

        private static void LogWrite(string message)
        {
            var exception = new Exception(message);
            Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(exception));
        }

        public static void Log(string message)
        {
            var sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendFormat("===============Event Occuurred at {0}===================", DateTime.Now.ToShortTimeString());
            sb.AppendLine();
            sb.AppendLine();
            sb.Append(message);
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine("=======================================================");
            Logger.LogWrite(sb.ToString());
        }

        public static void Log(Exception ex)
        {
            try
            {
                Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(ex));
            }
            catch (Exception x)
            {
                //continue
            }
        }
    }
}
