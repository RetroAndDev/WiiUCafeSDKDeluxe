from hashlib import pbkdf2_hmac, md5
import binascii
from Crypto.Cipher import AES
import sys
import json

def get_secret():
    # TODO: generate this 'secret' instead of hardcoding it.
    # Although, I'm pretty sure there's nothing wrong with including
    # it, since Nintendo generated it on the fly and never included
    # it in binary form in any way that I'm aware of. Only their
    # code that generated this would be copyrighted, not the string/number itself.
    return 'fd040105060b111c2d49'

def generate_key(title_id, pwd):
    # remove 00 padding from title id
    title_id = title_id[2:]

    # get secret string, append title id, and convert to binary string
    secret = binascii.unhexlify(get_secret() + title_id)

    # get md5 hash of secret
    hashed_secret = md5(secret).digest()
    
    # key is a pbkdf2 hash with sha1 base using hashed_secret as salt and 20 iterations
    non_encrypted_key = pbkdf2_hmac('sha1', pwd.encode(), hashed_secret, 20, 16)

    # return as hexstring
    return binascii.hexlify(non_encrypted_key)


def encrypt_title_key(title_id, title_key, ckey):
    # pad title id with trailing zeroes
    title_id += '0000000000000000'
    title_id = binascii.unhexlify(title_id)
    ckey = binascii.unhexlify(ckey)
    title_key = binascii.unhexlify(title_key)
    encryptor = AES.new(key=ckey, mode=AES.MODE_CBC, IV=title_id)
    encrypted_title_key = encryptor.encrypt(title_key)

    # return as hexstring
    return binascii.hexlify(encrypted_title_key)

def main(tid, ckey, password='mypass', givenarg=""):
    print('Using title id: {}'.format(tid))
    print('Using common key: {}'.format(ckey))
    print('Using password: {}'.format(password))
    unencrypted = generate_key(tid, password).decode()
    encrypted = encrypt_title_key(tid, unencrypted, ckey).decode()
    print('generated unencrypted: {}'.format(unencrypted))
    print('generated encrypted: {}'.format(encrypted))
    print('gived arg: {}'.format(givenarg))

    unencryptedTitleKey = unencrypted
    encryptedTitleKey = encrypted

    return (encrypted, unencrypted)

def getUnencryptedKey(tid, ckey, password='mypass'):
    unencrypted = generate_key(tid, password).decode()
    print(format(unencrypted))

    return (unencrypted)

def getEncryptedKey(tid, ckey, password='mypass'):
    unencrypted = generate_key(tid, password).decode()
    encrypted = encrypt_title_key(tid, unencrypted, ckey).decode()
    print(format(encrypted))

    return (encrypted)

if __name__ == '__main__':
    if(sys.argv[4] == "-getUnencrypted"):
        getUnencryptedKey(sys.argv[1], sys.argv[2], password=sys.argv[3])
    else:
        if(sys.argv[4] == "-getEncrypted"):
            getEncryptedKey(sys.argv[1], sys.argv[2], password=sys.argv[3])
        else:
            main(sys.argv[1], sys.argv[2], password=sys.argv[3], givenarg=sys.argv[4])