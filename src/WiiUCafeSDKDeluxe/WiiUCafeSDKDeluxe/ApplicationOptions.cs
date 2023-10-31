namespace WiiUCafeSDKDeluxe
{
	internal class ApplicationOptions
	{
		public static readonly string[] supportedUnityVersions = new string[3] { "Not Specified", "2017.1.2p3", "Other Verion" };

		public static readonly string[] wiiUHardwares = new string[5] { "Wii U Deluxe Set", "Wii U Basic Set", "CAT-DEV", "CAT-R", "Wii U CAT-DEV + CAT-R" };

		public static readonly string defaultPythonPath = "python";

		public static readonly string defaultJavaPath = "java";

		public static readonly string defaultJavaSha256 = "2faa5c83dc5ca3483b20d4b475a2e6491f1f2a0942a773d92c664fd794c25a5f"; //Tested Java 18.0.2.1

		public static readonly string defaultPythonSha256 = "5da31132fd6a415e71e3fd40c3a23853a799b91ea2821d2bcbf1bf0e8fa01e16"; //Tested Python 3.7.3 (x64)
	}
}
