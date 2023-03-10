using System;
using System.ComponentModel;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Attributes;

namespace TokenTransfer
{
    [DisplayName("TokenTransfer")]
    [ManifestExtra("Author", "NEO")]
    [ManifestExtra("Email", "developer@neo.org")]
    [ManifestExtra("Description", "This is a TokenTransfer")]
    public class TokenTransfer : SmartContract
    {
        public static bool Main()
        {
            return true;
        }
    }
}
