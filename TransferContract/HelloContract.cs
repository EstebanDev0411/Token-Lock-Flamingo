using System;
using System.ComponentModel;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Attributes;

namespace HelloContract
{
    [DisplayName("HelloContract")]
    [ManifestExtra("Author", "NEO")]
    [ManifestExtra("Email", "developer@neo.org")]
    [ManifestExtra("Description", "This is a HelloContract")]
    public class HelloContract : SmartContract
    {
        public static bool Main()
        {
            return true;
        }
    }
}
