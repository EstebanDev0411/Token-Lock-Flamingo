using System;
using System.ComponentModel;
using Neo;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Native;
using Neo.SmartContract.Framework.Attributes;

namespace TransferContract
{
    [DisplayName("TransferContract")]
    [ManifestExtra("Author", "Carlo")]
    [ManifestExtra("Email", "developer@neo.org")]
    [ManifestExtra("Description", "This is a TransferContract")]
    public class TransferContract : SmartContract
    {
        private static readonly UInt160 from = "NRrQYHq6wshLRSRsRDDw1nmMjpdA33xub9".ToScriptHash();
        private static readonly UInt160 stakingAddress = "NftS1gfUFrJ46oByhpVavEBB1ywmNUKZ5h".ToScriptHash();
        public static object Test()
        {
            BigInteger value = 10;
            bool result = Neo.Transfer(from, stakingAddress, value);
            return result;
        }
        public static bool Main()
        {
            return true;
        }
    }
}
