using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiiUCafeSDKDeluxe
{
    public delegate void ProgressChangeDelegate(double Percentage, ref bool Cancel);
    public delegate void Completedelegate();

    class CustomFileCopier
    {
        public CustomFileCopier(string Source, string Dest)
        {
            this.SourceFilePath = Source;
            this.DestFilePath = Dest;

            OnProgressChanged += delegate { };
            OnComplete += delegate { };
        }

        public void Copy()
        {
            foreach (string file in Directory.GetFiles(SourceFilePath))
            {
                FileInfo fileInfo = new FileInfo(file);
                string destFilePath = Path.Combine(DestFilePath, fileInfo.Name);

                byte[] buffer = new byte[1024 * 1024]; // 1MB buffer
                bool cancelFlag = false;

                using (FileStream source = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    long fileLength = source.Length;
                    using (FileStream dest = new FileStream(destFilePath, FileMode.CreateNew, FileAccess.Write))
                    {
                        long totalBytes = 0;
                        int currentBlockSize = 0;

                        while ((currentBlockSize = source.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            totalBytes += currentBlockSize;
                            double percentage = (double)totalBytes * 100.0 / fileLength;

                            dest.Write(buffer, 0, currentBlockSize);

                            cancelFlag = false;
                            OnProgressChanged(percentage, ref cancelFlag);

                            if (cancelFlag == true)
                            {
                                // Delete dest file here
                                break;
                            }
                        }
                    }
                }
            }

            OnComplete();
        }

        public string SourceFilePath { get; set; }
        public string DestFilePath { get; set; }

        public event ProgressChangeDelegate OnProgressChanged;
        public event Completedelegate OnComplete;
    }
}
