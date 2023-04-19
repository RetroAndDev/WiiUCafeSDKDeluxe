using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinLogs
{
    public class Log
    {
        public string content { get; set; }
        public LogType logType { get; set; }

        public enum LogType
        {
            Log = 0,
            Warning = 1,
            Error = 2,
            Sucess = 3,
        }

        public string GetLogAsString()
        {
            string logString = "";

            switch (logType)
            {
                case LogType.Error:
                    logString += "[Error] ";
                    break;
                case LogType.Warning:
                    logString += "[Warning] ";
                    break;
                case LogType.Sucess:
                    logString += "[Sucess] ";
                    break;
                default:
                    logString += "[Log] ";
                    break;
            }

            logString += content;
            return logString;
        }

        public string GetLogPrefix()
        {
            switch (logType)
            {
                case LogType.Error:
                    return "[Error] ";
                case LogType.Warning:
                    return "[Warning] ";
                case LogType.Sucess:
                    return "[Sucess] ";
                default:
                    return "[Log] ";
            }
        }

        public string GetLogContent()
        {
            return content;
        }
    }
}
