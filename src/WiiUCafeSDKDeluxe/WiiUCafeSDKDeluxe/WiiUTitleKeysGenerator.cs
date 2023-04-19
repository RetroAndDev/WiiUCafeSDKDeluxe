using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static WiiUCafeSDKDeluxe.WiIUProjectSettings;

namespace WiiUCafeSDKDeluxe
{
    public static class WiiUTitleKeysGenerator
    {

        public static string GetUnencryptedTitleKey(string titleKey, string passKey, string wiiUCommonKey)
        {
            var psi = new ProcessStartInfo();
            psi.FileName = ApplicationOptions.defaultPythonPath;

            var script = Path.Combine(Environment.CurrentDirectory, "keygen.py");

            psi.Arguments = $"\"{script}\" \"{titleKey}\" \"{wiiUCommonKey}\" \"{passKey}\" \"-getUnencrypted\"";

            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            var errors = "";
            var results = "";

            using (var process = Process.Start(psi))
            {
                errors = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();
            }

            if (errors == "")
            {
                return results;
            }
            else
            {
                return errors;
            }
        }

        public static string GetEncryptedTitleKey(string titleKey, string passKey, string wiiUCommonKey)
        {
            var psi = new ProcessStartInfo();
            psi.FileName = ApplicationOptions.defaultPythonPath;

            var script = Path.Combine(Environment.CurrentDirectory, "keygen.py");

            psi.Arguments = $"\"{script}\" \"{titleKey}\" \"{wiiUCommonKey}\" \"{passKey}\" \"-getEncrypted\"";

            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            var errors = "";
            var results = "";

            using (var process = Process.Start(psi))
            {
                errors = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();
            }

            if (errors == "")
            {
                return results;
            }
            else
            {
                return errors;
            }
        }
    }
}
