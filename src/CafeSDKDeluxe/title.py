import os
import random
import sys

from constants import Constants
from keygen import getKeys

class TitleManager:
    def __init__(self, title_id):
        self.TITLE_ID_TYPES = {
            'eShopTitle': '00050000',
            'eShopDemo' : '00050002',
            'eShopDLC'  : '0005000C',
            'update'    : '0005000E'
        }
        self.getTitleId(title_id)

        # Generate the keys for the TitleID
        keys = getKeys(self.titleID, Constants.COMMON_KEY)
        print(f"Generated keys for TitleID {self.titleID}:")
        print(f"  - Encrypted Title Key   : {keys[0]}")
        print(f"  - Unencrypted Title Key : {keys[1]}")
        self.unencryptedTitleKey = keys[1]
        self.encryptedTitleKey = keys[0]

    def getTitleId(self, title_id: str):
        if len(title_id) == 16 and all(c in '0123456789ABCDEF' for c in title_id.upper()):
            self.titleID = title_id
            print(f"TitleID set to : {self.titleID}")
        # Or a generation type (eShopTitle, eShopDemo, eShopDLC, update)
        elif title_id in self.TITLE_ID_TYPES:
            self.titleID = self.generate_title_id(title_id)
            print(f"Generated TitleID : {self.titleID} of type {title_id}")
        else:
            print(f"Invalid TitleID or type: {self.titleID}. Must be 16 char hex string (set TitleID) or one of {list(self.TITLE_ID_TYPES.keys())} (Generate TitleID).")
            sys.exit(1)
    
    def generate_title_id(self, type):
        # TitleID is 64-bit hex string (16 characters) with TileID Type + Unique ID + Variation
        # Example: 000500000CAA6100
        # 00050000 -> TitleID Type (eShopTitle)
        # CAA6100 -> Unique ID (randomly generated. Can be any 6 hex characters)
        # 00 -> Variation ????

        # Load the known TitleID list from known_ids.txt. If file not found, log an error and continue.
        #  known_ids.txt is a text file containing known TitleIDs, one per line
        try:
            known_ids = [line.strip() for line in open('known_ids.txt', 'r')]
        except FileNotFoundError:
            print("known_ids.txt not found. Generating a new TitleID without checking against known IDs.")
            known_ids = []

        while True:
            # Generate a random 5-character hex string for the Unique ID
            unique_id = ''.join(random.choices('123456789ABCDEF', k=5))

            # Create the TitleID by concatenating the TitleID Type and Unique ID
            title_id = f"{self.TITLE_ID_TYPES[type]}1{unique_id}00"

            # Check if the generated TitleID is already in the known IDs list
            if title_id not in known_ids:
                return title_id