using System;
using System.ComponentModel;
using Neo;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Attributes;

namespace TransferContract
{
    [DisplayName("TransferContract")]
    [ManifestExtra("Author", "Carlo")]
    [ManifestExtra("Email", "developer@neo.org")]
    [ManifestExtra("Description", "This is a TransferContract")]
    public class TransferContract : SmartContract
    {
        public static bool Main()
        {
            
            return true;
        }
    }
}
