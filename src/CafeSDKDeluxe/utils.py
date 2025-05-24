import os
import sys
from keygen import verify_ckey

class Utils:
    @staticmethod
    def getCafeRoot():
        """Get the Cafe Root path from the environment variable."""
        cafe_root = os.environ.get("CAFE_ROOT")
        if not cafe_root:
            print("CAFE_ROOT environment variable is not set.")
            raise EnvironmentError("CAFER_OOT environment variable is not set.")
        return cafe_root
    
    @staticmethod
    def getCommonDevKey():
        """Get the Common Dev Key from the Cafe SDK."""
        common_dev_key_path = os.path.join(Utils.getCafeRoot(), 'system', 'bin', 'tool', 'mastering', 'resources', 'makemaster', 'tik_sys.bin')
        if not os.path.exists(common_dev_key_path):
            print(f"Common Dev Key file not found at {common_dev_key_path}.")
            raise FileNotFoundError(f"Common Dev Key file not found at {common_dev_key_path}.")
        # Read the Common Dev Key from the file
        with open(common_dev_key_path, 'rb') as f:
            common_dev_key = f.read()

        return common_dev_key
    
    @staticmethod
    def getCommonKey():
        """Get the Common Key from the environment variable."""
        common_key = os.environ.get("WIIU_COMMON_KEY")
        if not common_key:
            print("WIIU_COMMON_KEY environment variable is not set.")
            raise EnvironmentError("WIIU_COMMON_KEY environment variable is not set.")
        
        # Verify the Common Key
        if not verify_ckey(common_key):
            print("Common Key is not valid.")
            raise ValueError("Common Key is not valid.")

        return common_key
    
    @staticmethod
    def checkInstallation():
        """Check if the installation is correct."""
        # Check if app located inside an Unity project
        if not Utils.isInsideUnityProject():
            print("The app must be located inside a Unity project.")
            print("CafeSDKDeluxe.exe needs to be /<path>/UnityProject/CafeSDKDeluxe/CafeSDKDeluxe.exe")
            raise EnvironmentError("Wii U Café SDK must be installed inside a Unity project. Ex: CafeSDKDeluxe.exe needs to be /<path>/UnityProject/CafeSDKDeluxe/CafeSDKDeluxe.exe")
        
        # Check if CDECRYPT is installed
        if not os.path.exists(os.path.join(os.getcwd(), 'cdecrypt.exe')):
            print("cdecrypt.exe not found. Please install it.")
            raise FileNotFoundError("cdecrypt.exe not found. Please install it.")
        
        # Check if CDECRYPT is installed
        if not os.path.exists(os.path.join(os.getcwd(), 'cnuspacker.exe')):
            print("cnuspacker.exe not found. Please install it.")
            raise FileNotFoundError("cnuspacker.exe not found. Please install it.")
        
    @staticmethod
    def deleteFolderRecursive(path: str) -> None:
        """Delete a folder and all its contents."""
        if os.path.exists(path):
            for root, dirs, files in os.walk(path, topdown=False):
                for name in files:
                    os.remove(os.path.join(root, name))
                for name in dirs:
                    os.rmdir(os.path.join(root, name))
            os.rmdir(path)

    # Unity related stuff
    @staticmethod
    def getPathToUnityProject() -> str:
        # Unity project path is the parent directory of the app
        return os.path.abspath(os.path.join(os.getcwd(), os.pardir))
            
    @staticmethod
    def getUnityTitleId():
        # Get the TitleID from the Unity project settings
        try:
            with open(os.path.join(Utils.getPathToUnityProject(), 'ProjectSettings', 'ProjectSettings.asset'), 'r') as f:
                project_settings = f.read()
        except FileNotFoundError:
            raise FileNotFoundError("ProjectSettings.asset file not found.")
        
        # Search for wiiUTitleID line and return the TitleID
        for line in project_settings.splitlines():
            if line.strip().startswith("wiiUTitleID:"):
                return line.split(":")[1].strip()
        raise ValueError("TitleID not found in ProjectSettings.asset file.")
            
    def updateTitleIdToUnity(titleID: str) -> None:
        # Update the TitleID in the Unity project settings
        # Load the ProjectSettings.asset file
        try:
            with open(os.path.join(Utils.getPathToUnityProject(), 'ProjectSettings', 'ProjectSettings.asset'), 'r') as f:
                project_settings = f.read()
        except FileNotFoundError:
            print("ProjectSettings.asset file not found.")
            return
        
        # Search for wiiUTitleID line and replace it with the new TitleID
        for line in project_settings.splitlines():
            if line.strip().startswith("wiiUTitleID:"):
                project_settings = project_settings.replace(line, f"  wiiUTitleID: {titleID}")
            
        # Save the updated ProjectSettings.asset file
        try:
            with open(os.path.join(os.getcwd(), 'ProjectSettings', 'ProjectSettings.asset'), 'w') as f:
                f.write(project_settings)
            print(f"Updated TitleID to {titleID} in ProjectSettings.asset file.")
        except Exception as e:
            print(f"Failed to update TitleID in ProjectSettings.asset file: {e}")

    @staticmethod    
    def isInsideUnityProject():
        """Check if the parent directory is inside a Unity project."""
        if not os.path.exists(os.path.join(Utils.getPathToUnityProject(), 'ProjectSettings', 'ProjectVersion.txt')):
            return False
        if not os.path.exists(os.path.join(Utils.getPathToUnityProject(), 'ProjectSettings', 'ProjectSettings.asset')):
            return False
        return True
        
    @staticmethod
    def getUnityVersion() -> str:
        """Get the Unity version from the ProjectVersion file."""
        try:
            with open(os.path.join(Utils.getPathToUnityProject(), 'ProjectSettings', 'ProjectVersion.txt'), 'r') as f:
                for line in f:
                    if line.startswith("m_EditorVersion:"):
                        return line.split(":")[1].strip()
        except FileNotFoundError:
            raise FileNotFoundError("ProjectVersion.txt file not found.")
        
    @staticmethod
    def getProductName() -> str:
        """Get the project name from the ProjectSettings file."""
        try:
            with open(os.path.join(Utils.getPathToUnityProject(), 'ProjectSettings', 'ProjectSettings.asset'), 'r') as f:
                for line in f:
                    if line.strip().startswith("productName:"):
                        return line.split(":")[1].strip()
        except FileNotFoundError:
            raise FileNotFoundError("ProjectSettings.asset file not found.")
        
    @staticmethod
    def writeToConsole(text: str) -> None:
        """Write text to the console."""
        sys.stdout.write(text + "\n")
