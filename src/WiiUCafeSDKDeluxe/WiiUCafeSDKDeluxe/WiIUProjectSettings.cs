using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiiUCafeSDKDeluxe
{
    public class WiIUProjectSettings
    {
        public string TitleId { get; set; }
        public string UniqueId { get; set; }
        public string Variation { get; set; }
        public string unencrypted_TitleKey { get; set; }
        public string encrypted_TitleKey { get; set; }
        public WiiUCommonKey encryptedKey { get; set; }
        public WiiUPassKey passKey { get; set; }
        public string passKeyString { get; set; }

        public static WiIUProjectSettings GenerateDefault()
        {
            WiIUProjectSettings projectSettings = new WiIUProjectSettings()
            {
                TitleId = "0x0005000011000000",
                UniqueId = "0x10000",
                Variation = "0x00",
                unencrypted_TitleKey = "377a4d82cdc086e3305bbe26354e6c1b",
                encrypted_TitleKey = "04738ab39ea5b453e9aa4e8b40c727d7",
                encryptedKey = WiiUCommonKey.WiiUCommonKey,
                passKey = WiiUPassKey.mypass,
                passKeyString = GetPassKey(WiiUPassKey.mypass),
            };
            return projectSettings;
        }

        public static string GetPassKey(WiiUPassKey wiiUPassKey)
        {
            switch (wiiUPassKey)
            {
                default:
                    return "mypass";
                case WiiUPassKey.nintendo:
                    return "nintendo";
            }
        }

        public enum WiiUCommonKey
        {
            WiiUCommonKey = 0,
            WiiUDevCommonKey = 1,
        }
        public enum WiiUPassKey
        {
            mypass = 0,
            nintendo = 1,
        }
    }
}
