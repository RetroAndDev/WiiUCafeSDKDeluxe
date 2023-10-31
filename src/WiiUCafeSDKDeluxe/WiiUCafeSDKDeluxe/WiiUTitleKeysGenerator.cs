using System.Diagnostics;
using WinLogs;

namespace WiiUCafeSDKDeluxe
{
	public static class WiiUTitleKeysGenerator
	{
		public static string GetUnencryptedTitleKey(string titleKey, string passKey, string wiiUCommonKey)
		{
			ProcessStartInfo psi = new ProcessStartInfo();
			psi.FileName = "\"" + SettingsManager.applicationSettings.pythonPath + "\"";
            string script = "\"" + Program.Paths["KiiU"] + "\"";
            psi.Arguments = "\"" + script + "\" \"" + titleKey + "\" \"" + wiiUCommonKey + "\" \"" + passKey + "\" \"-getUnencrypted\"";
			psi.UseShellExecute = false;
			psi.CreateNoWindow = true;
			psi.RedirectStandardOutput = true;
			psi.RedirectStandardError = true;
			string errors = "";
			string results = "";
            Program.logsManager.Log("[KiiU Runner] Running Kii-U To get Unencrypted Title Key");
            Program.logsManager.Log("[KiiU Runner] Process File : " + psi.FileName);
            Program.logsManager.Log("[KiiU Runner] Process Args : " + psi.Arguments);
            using (Process process = Process.Start(psi))
			{
				errors = process.StandardError.ReadToEnd();
				results = process.StandardOutput.ReadToEnd();
			}
			if (errors == "")
			{
				return results;
			}
			return errors;
		}

		public static string GetEncryptedTitleKey(string titleKey, string passKey, string wiiUCommonKey)
		{
			ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "\"" + SettingsManager.applicationSettings.pythonPath + "\"";
            string script = Program.Paths["KiiU"];
			psi.Arguments = "\"" + script + "\" \"" + titleKey + "\" \"" + wiiUCommonKey + "\" \"" + passKey + "\" \"-getEncrypted\"";
			psi.UseShellExecute = false;
			psi.CreateNoWindow = true;
			psi.RedirectStandardOutput = true;
			psi.RedirectStandardError = true;
			string errors = "";
			string results = "";
            Program.logsManager.Log("[KiiU Runner] Running Kii-U To get Encrypted Title Key");
            Program.logsManager.Log("[KiiU Runner] Process File : " + psi.FileName);
			Program.logsManager.Log("[KiiU Runner] Process Args : " + psi.Arguments);
            using (Process process = Process.Start(psi))
			{
				errors = process.StandardError.ReadToEnd();
				results = process.StandardOutput.ReadToEnd();
			}
			if (errors == "")
			{
				return results;
			}
			return errors;
		}
	}
}
