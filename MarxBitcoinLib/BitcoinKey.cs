using NBitcoin;

namespace MarxBitcoinLib
{
    public class BTCKeyPair
    {
        public string WIFPrivateKey { get; set; }
        public byte[] PrivateKey { get; set; }
        public string PublicAddress { get; set; }
        public byte[] PublicAddressTruncated { get; set; }
    }
    public static class BitcoinKey
    {
        public static BTCKeyPair GenerateKey()
        {
            RandomUtils.Random = new RNGCryptoServiceProviderRandom();// UnsecureRandom();
            Key nKey = new Key();

            BTCKeyPair bKey = new BTCKeyPair();
            bKey.PrivateKey = nKey.ToBytes();
            bKey.WIFPrivateKey = nKey.GetWif(Network.Main).ToString();
            bKey.PublicAddress = nKey.PubKey.GetAddress(Network.Main).ToString();
            bKey.PublicAddressTruncated = WIFEncoder.ConvertFromPublicAddress(bKey.PublicAddress);

            return bKey;
        }

        public static string GetPublicAddress(byte[] privateKey)
        {
            Key key = new Key(privateKey, -1, true);
            return key.PubKey.GetAddress(Network.Main).ToString();
        }

        public static string GetPrivateKeyWIF(byte[] privateKey)
        {
            Key key = new Key(privateKey, -1, true);
            return key.GetWif(Network.Main).ToString();
        }

        public static BTCKeyPair GetPublicAddress(BTCKeyPair privateKey)
        {
            Key key = new Key(privateKey.PrivateKey, -1, true);
            privateKey.PublicAddress = key.PubKey.GetAddress(Network.Main).ToString();
            return privateKey;
        }

        public static bool[] ComparePublicAddresses(string[] generatedAddresses, string[] addressList)
        {
            bool[] result = new bool[addressList.Length];

            for (int i = 0; i < addressList.Length; i++)
                if (generatedAddresses[i] == addressList[i])
                    result[i] = true;

            return result;
        }
    }
}
