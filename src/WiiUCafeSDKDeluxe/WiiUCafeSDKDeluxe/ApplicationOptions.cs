using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiiUCafeSDKDeluxe
{
    internal class ApplicationOptions
    {
        public static readonly string[] supportedUnityVersions = new string[3] { "Not Specified", "2017.1.2p3", "Other Verion" }; 
        public static readonly string[] wiiUHardwares = new string[5] { "Wii U Deluxe Set", "Wii U Basic Set", "CAT-DEV", "CAT-R", "Wii U CAT-DEV + CAT-R" };
        public static readonly string defaultPythonPath = @"C:\Program Files (x86)\Python38-32\python.exe";
        public static readonly string defaultJavaPath = @"C:\Program Files\Java\jdk-18.0.2.1\bin\java.exe";
    }
}
