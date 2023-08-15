using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace WinLogs
{
	public class WinLogsManager
	{
		public List<Log> logs { get; private set; }

		public void InitializeLogSystem()
		{
			logs = new List<Log>();
		}

		public void SaveLogs(string logPath)
		{
			List<string> logsString = new List<string>();
			foreach (Log log2 in logs)
			{
				logsString.Add(log2.GetLogAsString());
			}
			if (!File.Exists(logPath))
			{
				StreamWriter writer = new StreamWriter(logPath);
				foreach (string log in logsString)
				{
					writer.WriteLine(log);
				}
				writer.Close();
				LogSucess("Log file saved at:" + logPath);
			}
			else
			{
				LogError("Can't save log file at: " + logPath + ". File already exists !");
			}
		}

		public void Log(string content)
		{
			Log log = new Log
			{
				content = content,
				logType = WinLogs.Log.LogType.Log
			};
			logs.Add(log);
		}

		public void LogWarning(string content)
		{
			Log log = new Log
			{
				content = content,
				logType = WinLogs.Log.LogType.Warning
			};
			logs.Add(log);
		}

		public void LogError(string content)
		{
			Log log = new Log
			{
				content = content,
				logType = WinLogs.Log.LogType.Error
			};
			logs.Add(log);
		}

		public void LogSucess(string content)
		{
			Log log = new Log
			{
				content = content,
				logType = WinLogs.Log.LogType.Sucess
			};
			logs.Add(log);
		}

		public static Color GetPrefixColor(Log.LogType logType)
		{
			switch (logType)
			{
			case WinLogs.Log.LogType.Error:
				return Color.Red;
			case WinLogs.Log.LogType.Warning:
				return Color.Orange;
			case WinLogs.Log.LogType.Sucess:
				return Color.Green;
			default:
				return Color.Black;
			}
		}
	}
}
