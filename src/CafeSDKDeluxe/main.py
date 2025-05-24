# Verify dependency installation
import json
import os
import shutil
import subprocess
import sys
import datetime
from sys import stdout 

from config import ConfigData
from utils import Utils
from constants import Constants
from external_tools import ExternalTools
from title import TitleManager

# Check for pycryptodome, questionary
try:
    from questionary import *
    from Crypto.Cipher import AES
    from colorama import Fore, Style, init as colorama_init
except ImportError:
    Utils.writeToConsole(Fore.RED + "Missing dependencies. Please install the required packages.")
    Utils.writeToConsole(Fore.BLUE + "Required packages: pycryptodome, questionary, colorama")
    sys.exit(1)


colorama_init()
Constants.initialize()

cls = lambda: os.system('cls')
cls()

# Tools import and initialization
titleManager = None

def configurationMenu():
    while True:
        options = ["Change Unity path", "Change SD Card path", "Enable/Disable Automatic Copy to SD after build", "Change TitleID", "Enable/Disable Debug mode (THIS SESION ONLY)", "Back"]
        answer = select("What do you want to do?", choices=options).ask()

        if answer == options[0]:
            # Change Unity path
            unityPath = path("Enter the full path to your Unity editor executable. For example : C:\\Unity\\Editor\\Unity.exe:").ask()
            if not os.path.exists(unityPath):
                Utils.writeToConsole(f"Unity path {unityPath} does not exist.")
            else:
                projectConfig.unity_path = unityPath
                Utils.writeToConsole(f"Unity path changed to {unityPath}.")
        elif answer == options[1]:
            # Change SD Card path
            sdCardPath = path("Enter the full path to your SD Card. For example : E:\\:", only_directories=True).ask()
            projectConfig.export_path = sdCardPath
            Utils.writeToConsole(f"SD Card path changed to {sdCardPath}.")
        elif answer == options[2]:
            enable = confirm("Do you want to enable automatic copy to SD after build? Currently: " + ("Enabled" if projectConfig.auto_export else "Disabled")).ask()
            projectConfig.auto_export = enable
        elif answer == options[3]:
            # Change TitleID
            Utils.writeToConsole(f"{Fore.BLUE}Current TitleID:{Fore.WHITE} {titleManager.titleID}")
            Utils.writeToConsole("You can set a TitleID or generate a new one.")
            Utils.writeToConsole("To set a TitleID, enter a 16 char hex string (example: 000500000CAA6100)")
            Utils.writeToConsole(f"To generate a new TitleID, enter one of the following types: {list(titleManager.TITLE_ID_TYPES.keys())}")
            titleId = text("Enter the new TitleID :").ask()
            titleManager.getTitleId(titleId)
            Utils.updateTitleIdToUnity(titleManager.titleID)
            Utils.writeToConsole(f"TitleID changed to {titleManager.titleID}.")
        elif answer == options[4]:
            # Enable/Disable Debug mode
            enable = confirm("Do you want to enable debug mode? Currently: " + ("Enabled" if Constants.DEBUG else "Disabled")).ask()
            Constants.DEBUG = enable
            Utils.writeToConsole(f"Debug mode changed to {Constants.DEBUG}.")
        elif answer == options[5]:
            # Save the config to a file
            config_file_path = os.path.join(os.getcwd(), 'sdk_deluxe.config')
            with open(config_file_path, 'w') as f:
                f.write(json.dumps(projectConfig.to_dict(), indent=4))
            Utils.writeToConsole("Configuration saved.")
            break

def buildMenu():
    # Load builds from the Builds/ directory
    Utils.writeToConsole(f"{Fore.MAGENTA}Loading builds...")
    builds = []
    buildsDir = os.path.join(Utils.getPathToUnityProject(), "Builds")
    if os.path.exists(buildsDir):
        # Search for valid build dirs
        for root, dirs, files in os.walk(buildsDir):
            for dir in dirs:
                if os.path.exists(os.path.join(root, dir, "copyToSDCard")):
                    builds.append(dir)

    if len(builds) == 0:
        Utils.writeToConsole(f"{Fore.RED}No builds found. Please add a build to the Builds/ directory.")
        Utils.writeToConsole(f"{Fore.BLUE}Note: {Fore.WHITE}only build made as Download Image are valid. If a build is missing, please make sure to build it as a Download Image.")
        input("Press [ENTER] to go back to the main menu...")
        return
    
    Utils.writeToConsole(f"{Fore.BLUE}Found {len(builds)} builds:")
    while True:
        options = []
        translateTable = {}
        # Add all the builds to the options list with last edited date
        for build in builds:
            buildPath = os.path.join(buildsDir, build)
            displayName = f"{build} ({datetime.datetime.fromtimestamp(os.path.getmtime(buildPath)).strftime('%Y-%m-%d %H:%M:%S')})"
            translateTable[displayName] = build
            options.append(displayName)
        options.append("Back")
        answer = select("What build do you want to masterise ?", choices=options).ask()

        if answer != options[-1]:
            # Want to build the selected build
            buildFolder = translateTable[answer]
            buildDir = os.path.join(buildsDir, buildFolder)
            outDir = os.path.join(buildsDir, "RetailBuilds", buildFolder)
            Utils.writeToConsole(f"{Fore.BLUE}Building {Fore.WHITE}{buildFolder} {Fore.BLUE}to {Fore.WHITE}{outDir}{Fore.BLUE}...")
            if os.path.exists(outDir):
                Utils.deleteFolderRecursive(outDir)

            # Create temporal NUSPacker folder
            tempDir = os.path.join(buildsDir, "temp", buildFolder)
            if not os.path.exists(tempDir):
                os.makedirs(tempDir)
            else:
                Utils.writeToConsole(f"{Fore.BLUE}Deleting old temp folder at {tempDir}...")
                Utils.deleteFolderRecursive(tempDir)
            
            # Start by decrypting the build with CDecrypt
            if os.path.exists(os.path.join(buildDir, "copyToSDCard")):
                Utils.writeToConsole(f"{Fore.BLUE}Decrypting build...")
                # Encrypted game data is located in /<build name>/copyToSDCard/<randomname>/
                # If there are multiple folders, ask the user to select one
                inputPath = None
                builds = os.listdir(os.path.join(buildDir, "copyToSDCard"))
                if len(builds) > 1:
                    Utils.writeToConsole(f"{Fore.BLUE}Multiple builds found in copyToSDCard. Please select the one you want to masterise:")
                    options = []
                    for build in builds:
                        buildPath = os.path.join(buildDir, "copyToSDCard", build)
                        displayName = f"{build} ({datetime.datetime.fromtimestamp(os.path.getmtime(buildPath)).strftime('%Y-%m-%d %H:%M:%S')})"
                        translateTable[displayName] = build
                        options.append(displayName)
                    targetBuild = select("Select the build to decrypt:", choices=options).ask()
                    inputPath = os.path.join(buildDir, "copyToSDCard", translateTable[targetBuild])
                else:
                    inputPath = os.path.join(buildDir, "copyToSDCard", builds[0])

                if not ExternalTools.decryptDevBuild(inputPath, os.path.join(tempDir, "decrypted")):
                    Utils.writeToConsole(f"{Fore.RED}Error decrypting build.")
                    sys.exit(1)
            else:
                Utils.writeToConsole(f"{Fore.RED}No copyToSDCard found. This is a valid build ?")
                sys.exit(1)

            # Process the decrypted build with NUSPacker
            Utils.writeToConsole(f"{Fore.BLUE}Packing build with NUSPacker...")
            os.makedirs(outDir, exist_ok=True)

            if not ExternalTools.packDevBuild(os.path.join(tempDir, "decrypted"), outDir, titleManager.titleID, titleManager.unencryptedTitleKey):
                Utils.writeToConsole(f"{Fore.RED}Error packing build.")
                sys.exit(1)

            Utils.writeToConsole(f"{Fore.BLUE}Cleaning up...")
            # Delete the temp folder
            if os.path.exists(tempDir):
                Utils.deleteFolderRecursive(tempDir)

            Utils.writeToConsole(f"{Fore.GREEN}Build packed successfully.")
            Utils.writeToConsole(f"{Fore.BLUE}A Retail build was created in to {Fore.WHITE}{outDir.removeprefix(buildsDir)}{Fore.BLUE}. It can be installed an Retail Wii U System.")

            if projectConfig.export_path is not None and os.path.exists(projectConfig.export_path):
                if not projectConfig.auto_export:
                    copyToSd = confirm("Do you want to copy the build to the SD Card?").ask()
                else:
                    copyToSd = True
                
                if copyToSd:
                    Utils.writeToConsole(f"{Fore.BLUE}Copying build to {Fore.WHITE}{projectConfig.export_path}{Fore.BLUE}...")
                    # Copy the build to the SD Card
                    sdCardPath = os.path.join(projectConfig.export_path, "Install", f"{titleManager.titleID} - {Utils.getProductName()}")
                    if os.path.exists(sdCardPath):
                        Utils.writeToConsole(f"{Fore.YELLOW}Deleting old build at {sdCardPath}...")
                        Utils.deleteFolderRecursive(sdCardPath)
                    os.makedirs(sdCardPath, exist_ok=True)
                    # Copy the build to the SD Card
                    for root, dirs, files in os.walk(outDir):
                        for file in files:
                            src = os.path.join(root, file)
                            dst = os.path.join(sdCardPath, file)
                            Utils.writeToConsole(f"{Fore.BLUE}Copying {Fore.CYAN}{src} to {Fore.MAGENTA}{dst}{Fore.BLUE}...")
                            shutil.copy2(src, dst)
                    Utils.writeToConsole(f"{Fore.GREEN}Build copied to SD Card.")
            
            input("Press [ENTER] to go back to the main menu...")
            break
        else:
            # Back to the main menu
            break

def greet():
    Utils.writeToConsole(Style.RESET_ALL)
    Utils.writeToConsole(Fore.BLUE + r"""
 ________  ________  ________ _______           ________  ________  ___  __            ________  _______   ___       ___  ___     ___    ___ _______      
|\   ____\|\   __  \|\  _____\\  ___ \         |\   ____\|\   ___ \|\  \|\  \         |\   ___ \|\  ___ \ |\  \     |\  \|\  \   |\  \  /  /|\  ___ \     
\ \  \___|\ \  \|\  \ \  \__/\ \   __/|        \ \  \___|\ \  \_|\ \ \  \/  /|_       \ \  \_|\ \ \   __/|\ \  \    \ \  \\\  \  \ \  \/  / | \   __/|    
 \ \  \    \ \   __  \ \   __\\ \  \_|/__       \ \_____  \ \  \ \\ \ \   ___  \       \ \  \ \\ \ \  \_|/_\ \  \    \ \  \\\  \  \ \    / / \ \  \_|/__  
  \ \  \____\ \  \ \  \ \  \_| \ \  \_|\ \       \|____|\  \ \  \_\\ \ \  \\ \  \       \ \  \_\\ \ \  \_|\ \ \  \____\ \  \\\  \  /     \/   \ \  \_|\ \ 
   \ \_______\ \__\ \__\ \__\   \ \_______\        ____\_\  \ \_______\ \__\\ \__\       \ \_______\ \_______\ \_______\ \_______\/  /\   \    \ \_______\
    \|_______|\|__|\|__|\|__|    \|_______|       |\_________\|_______|\|__| \|__|        \|_______|\|_______|\|_______|\|_______/__/ /\ __\    \|_______|
                                                  \|_________|                                                                   |__|/ \|__|""")
    Utils.writeToConsole("")
    Utils.writeToConsole(f"{Fore.BLUE}Welcome to the Cafe SDK Deluxe, the ultimate tool for Wii U developers! {Fore.MAGENTA}v{Constants.APP_VERSION}")
    Utils.writeToConsole(f"{Fore.BLUE}This tool is designed to help you build and manage your Unity Wii U projects with ease.")
    Utils.writeToConsole(f"{Fore.GREEN}Made by RetroAndDev with ❤️")
    Utils.writeToConsole(f"{Fore.YELLOW}Please report any bugs or issues to the GitHub repository: https://github.com/RetroAndDev/WiiUCafeSDKDeluxe")
    Utils.writeToConsole(f"{Fore.GREEN}Enjoy your development experience!")
    Utils.writeToConsole("")

if __name__ == '__main__':
    cls()
    # Verify and load options
    Utils.checkInstallation()

    # Load the config file
    projectConfig = None
    try:
        with open('sdk_deluxe.config', 'r') as f:
            projectConfig = ConfigData.from_dict(json.load(f))
    except FileNotFoundError:
        pass

    # Read the titleId 
    unityTitleId = Utils.getUnityTitleId()
    if unityTitleId is not None:
        titleManager = TitleManager(unityTitleId)

    # Display the welcome message
    greet()

    if projectConfig is None:
        Utils.writeToConsole("This is a new project. Please enter the required information to set up your project.")
        unityVersion = Utils.getUnityVersion()
        Utils.writeToConsole(f"Wii U Cafe SDK Deluxe detected a Unity {unityVersion} project")

        if unityVersion not in Constants.WORKING_UNITY_VERSIONS:
            Utils.writeToConsole(f"Warning: Unity {unityVersion} is not a supported version. Supported versions are: {', '.join(Constants.WORKING_UNITY_VERSIONS)}")
            Utils.writeToConsole("This version was not tested with the SDK Deluxe. Proceed at your own risk.")

        Utils.writeToConsole("Generating a new TitleID and Title Key...")
        titleManager = TitleManager("eShopTitle")

        Utils.writeToConsole("Some details are require to finish the project setup.")
        unityExists = False
        while not unityExists:
            unityPath = text("Enter the full path to your Unity editor executable. For example : C:\\Unity\\Editor\\Unity.exe:").ask()
            if not os.path.exists(unityPath):
                Utils.writeToConsole(f"Unity path {unityPath} does not exist.")
                unityExists = False
            else:
                unityExists = True

        Utils.writeToConsole("Insert the SD Card you will use to copy the build to your Wii U.")
        sdCardPath = text("Enter the full path to your SD Card. For example : E:\\:").ask()

        config = ConfigData()
        config.unity_path = unityPath
        config.export_path = sdCardPath

        # Save the config to a file
        config_file_path = os.path.join(os.getcwd(), 'sdk_deluxe.config')
        
        with open(config_file_path, 'w') as f:
            f.write(json.dumps(config.to_dict(), indent=4))

        Utils.writeToConsole("Project setup complete!")
        Utils.writeToConsole("Please restart the application to continue.")
    else:
        while True:
            cls()
            greet()
            options = ["Open...", "Build...", "Configuration...", "Project Info", "Exit"]
    
            answer = select("What do you want to do?", choices=options).ask()

            if answer == options[0]:
                # Ask what to open
                openOptions = ["Open Unity project", "Open Retail builds", "Open SD Card (if mounted)", "Back"]
                openAnswer = select("What do you want to open?", choices=openOptions).ask()
                if openAnswer == openOptions[0]:
                    # Open the Unity project
                    # Open Unity project with a command line argument and don't block this script
                    Utils.writeToConsole("Opening Unity project...")
                    cmd = [ projectConfig.unity_path, "-projectPath", Utils.getPathToUnityProject() ]

                    # Options de démarrage
                    si = subprocess.STARTUPINFO()
                    si.dwFlags |= subprocess.STARTF_USESHOWWINDOW

                    # Lancement de Unity sans console ni logs Python
                    process = subprocess.Popen(
                        cmd,
                        stdout=subprocess.DEVNULL,
                        stderr=subprocess.DEVNULL,
                        creationflags=subprocess.CREATE_NO_WINDOW,
                        startupinfo=si,
                        shell=False
                    )
                    Utils.writeToConsole("Unity project opened.")
                elif openAnswer == openOptions[1]:
                    # Open the Retail builds folder
                    Utils.writeToConsole("Opening Retail builds folder...")
                    os.makedirs(os.path.join(Utils.getPathToUnityProject(), "Builds", "RetailBuilds"), exist_ok=True)
                    subprocess.run(["explorer", os.path.join(Utils.getPathToUnityProject(), "Builds", "RetailBuilds")])
                elif openAnswer == openOptions[2]:
                    # Open the SD Card folder
                    if projectConfig.export_path is not None and os.path.exists(projectConfig.export_path):
                        Utils.writeToConsole("Opening SD Card folder...")
                        subprocess.run(["explorer", projectConfig.export_path])
                    else:
                        Utils.writeToConsole("SD Card path is not set or does not exist.")
            elif answer == options[1]:
                buildMenu()
            elif answer == options[2]:
                # Display the configuration menu and wait for the while loop to finish
                configurationMenu()
            elif answer == options[3]:
                # Display the project info
                Utils.writeToConsole(f"{Fore.MAGENTA}Project Info:")
                Utils.writeToConsole(f"{Fore.BLUE}Name:{Fore.WHITE} {Utils.getProductName()}")
                Utils.writeToConsole(f"{Fore.BLUE}Unity path:{Fore.WHITE} {projectConfig.unity_path}")
                Utils.writeToConsole(f"{Fore.BLUE}SD Card path:{Fore.WHITE} {projectConfig.export_path}")
                Utils.writeToConsole(f"{Fore.BLUE}TitleID:{Fore.WHITE} {titleManager.titleID}")
                Utils.writeToConsole(f"{Fore.BLUE}Title Keys:")
                Utils.writeToConsole(f"{Fore.CYAN}  - Unencrypted Title Key   :{Fore.WHITE} {titleManager.unencryptedTitleKey}")
                Utils.writeToConsole(f"{Fore.CYAN}  - Encrypted Title Key     :{Fore.WHITE} {titleManager.encryptedTitleKey}")
                input("Press [ENTER] to go back to the main menu...")
            elif answer == options[4]:
                Utils.writeToConsole(f"{Fore.GREEN}Goodbye!")
                sys.exit(0)