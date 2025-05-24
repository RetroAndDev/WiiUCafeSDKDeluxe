import os
import subprocess
import re

from constants import Constants

class ExternalTools:
    @staticmethod
    def decryptDevBuild(path, out):
        # Decrypt the build with CDecrypt
        print(f"Decrypting {path} to {out}...")

         # Check if folder exists
        if not os.path.exists(path):
            print(f"Input path {path} does not exist.")
            return False
        os.makedirs(out, exist_ok=True)

        try:
            args = ["cdecrypt.exe", path, out]
            
            if Constants.DEBUG:
                subprocess.run(args, shell=True, check=True)
            else:
                subprocess.run(args, shell=True, check=True, stdout=subprocess.PIPE, stderr=subprocess.PIPE)
                print("Decryption completed successfully.")
            return True
        except subprocess.CalledProcessError as e:
            print(f"Error decrypting build: {e}")
            return False
        
    @staticmethod
    def packDevBuild(path, out, titleId, unencryptedTitleKey):
        # Pack the build with CDecrypt
        print(f"Packing {path} to {out}...")

         # Check if folder exists
        if not os.path.exists(path):
            print(f"Input path {path} does not exist.")
            return False
        os.makedirs(out, exist_ok=True)

        #DEBUG SHOW COMMAND IN TERMINAL
        args = [
                f'cnuspacker.exe',
                f'-in "{path}"',
                f'-out "{out}"',
                f'-encryptionKey "{unencryptedTitleKey}"',
                f'-encryptKeyWith "{Constants.COMMON_KEY}"',
                f'-tID "{titleId}"',
                "-skipXMLParsing"
            ]

        try:
            
            if Constants.DEBUG:
                subprocess.run(" ".join(args), shell=True, check=True)
            else:
                subprocess.run(" ".join(args), shell=True, check=True, stdout=subprocess.PIPE, stderr=subprocess.PIPE)
                print("Packing completed successfully.")
            return True
        except subprocess.CalledProcessError as e:
            print(f"Error packing build: {e}")
            return False
