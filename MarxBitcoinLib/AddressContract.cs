using System;

namespace MarxBitcoinLib
{
    [Serializable]
    public class AddressContract
    {
        public string PrivateKey { get; set; }
        public string PublicAddress { get; set; }
    }
}
