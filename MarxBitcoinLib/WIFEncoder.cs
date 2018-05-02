using System;
using System.Security.Cryptography;

namespace MarxBitcoinLib
{
    public static class WIFEncoder
    {
        /* 
         * Convert private key to wallet import format (WIF)
         * 
         * 1 - Take a private key
         * 0C28FCA386C7A227600B2FE50B7CAE_SAMPLE_PRIVATE_KEY_DO_NOT_IMPORT_11EC86D3BF1FBE471BE89827E19D72AA1D
         * 
         * 2 - Add a 0x80 byte in front of it for mainnet addresses or 0xef for testnet addresses.Also add a 0x01 byte at the end if the private key will correspond to a compressed public key
         * 800C28FCA386C7A227600B2FE50B7C_SAMPLE_PRIVATE_KEY_DO_NOT_IMPORT_AE11EC86D3BF1FBE471BE89827E19D72AA1D
         * 
         * 3 - Perform SHA-256 hash on the extended key
         * 8147786C4D15106333BF278D71DADAF1079EF2D2440A4DDE37D747DED5403592
         * 
         * 4 - Perform SHA-256 hash on result of SHA-256 hash
         * 507A5B8DFED0FC6FE8801743720CEDEC06AA5C6FCA72B07C49964492FB98A714
         * 
         * 5 - Take the first 4 bytes of the second SHA-256 hash, this is the checksum
         * 507A5B8D
         * 
         * 6 - Add the 4 checksum bytes from point 5 at the end of the extended key from point 2
         * 800C28FCA386C7A227600B2FE50B7CAE11EC8_SAMPLE_PRIVATE_KEY_DO_NOT_IMPORT_6D3BF1FBE471BE89827E19D72AA1D507A5B8D
         * 
         * 7 - Convert the result from a byte string into a base58 string using Base58Check encoding.This is the Wallet Import Format
         * 5HueCGU8rMjxEXxiPuD5BDk_SAMPLE_PRIVATE_KEY_DO_NOT_IMPORT_u4MkFqeZyd4dZ1jvhTVqvbTLvyTJ
         */

        public static string ConvertToWIF(byte[] data)
        {
            byte[] newData = new byte[data.Length + 1];
            newData[0] = 0x80;

            Array.Copy(data, 0, newData, 1, data.Length);

            SHA256 sha256 = SHA256.Create();
            byte[] nDHash1 = sha256.ComputeHash(newData);
            byte[] nDHash2 = sha256.ComputeHash(nDHash1);

            byte[] newData2 = new byte[newData.Length + 4];

            Array.Copy(nDHash2, 0, newData2, newData2.Length - 4, 4);
            Array.Copy(newData, 0, newData2, 0, newData.Length);

            return Base58Encoding.Encode(newData2);
        }

        public static byte[] ConvertFromPublicAddress(string data)
        {
            byte[] decodedData = Base58Encoding.Decode(data);
            byte[] finalData = new byte[20];

            Array.Copy(decodedData, 1, finalData, 0, 20);

            return finalData;
        }
    }
}
