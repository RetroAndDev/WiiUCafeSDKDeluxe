using System.Diagnostics;

namespace WiiUCafeSDKDeluxe
{
	public static class WiiUTitleKeysGenerator
	{
		public static string GetUnencryptedTitleKey(string titleKey, string passKey, string wiiUCommonKey)
		{
			ProcessStartInfo psi = new ProcessStartInfo();
			psi.FileName = SettingsManager.applicationSettings.pythonPath;
			string script = Program.Paths["KiiU"];
			psi.Arguments = "\"" + script + "\" \"" + titleKey + "\" \"" + wiiUCommonKey + "\" \"" + passKey + "\" \"-getUnencrypted\"";
			psi.UseShellExecute = false;
			psi.CreateNoWindow = true;
			psi.RedirectStandardOutput = true;
			psi.RedirectStandardError = true;
			string errors = "";
			string results = "";
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
			psi.FileName = ApplicationOptions.defaultPythonPath;
			string script = Program.Paths["KiiU"];
			psi.Arguments = "\"" + script + "\" \"" + titleKey + "\" \"" + wiiUCommonKey + "\" \"" + passKey + "\" \"-getEncrypted\"";
			psi.UseShellExecute = false;
			psi.CreateNoWindow = true;
			psi.RedirectStandardOutput = true;
			psi.RedirectStandardError = true;
			string errors = "";
			string results = "";
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
