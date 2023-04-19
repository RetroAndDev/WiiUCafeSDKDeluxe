using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiiUCafeSDKDeluxe
{
    public class WiiUMasterSettings
    {
        public bool autoCopyToSd { get; set; } = false;
        public bool nus_skipappxmlparsing { get; set; } = true;
        public string sdCardPath { get; set; } = "";
        public MasterProcessMode masterProcessMode { get; set; } = MasterProcessMode.ProcessFromDownloadImage;

        public enum MasterProcessMode
        {
            ProcessFromDownloadImage = 0,
            ProcessFromWumad = 1,
            ProcessFromRaw = 2,
        }
    }
}
