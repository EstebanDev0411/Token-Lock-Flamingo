using Neo;
using Neo.SmartContract;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Native;
using Neo.SmartContract.Framework.Services;
using System;
using System.Numerics;

namespace Template.NEP17.CSharp
{
    [DisplayName("djnicholson.XyzTokenContract")]
    [ManifestExtra("Author", "David Nicholson")]
    [ManifestExtra("Email", "[[email protected]](/cdn-cgi/l/email-protection)")]
    [ManifestExtra("Description", "Controls issuance of the XYZ token")]
    public partial class NEP17 : SmartContract
    {
        const string MAP\_NAME = "XyzTokenContract";

        public static BigInteger TotalSupply() => TotalSupplyStorage.Get();

        public static string Symbol() => "Flund";

        public static ulong Decimals() => 8;

        [DisplayName("Transfer")]
        public static event Action<UInt160, UInt160, BigInteger> OnTransfer;

        private static StorageMap Balances => new StorageMap(Storage.CurrentContext, MAP\_NAME);

        private static BigInteger Get(UInt160 key) => (BigInteger) Balances.Get(key);

        private static void Put(UInt160 key, BigInteger value) => Balances.Put(key, value);

        public static BigInteger BalanceOf(UInt160 account)
        {
            
            if (!ValidateAddress(account)) throw new Exception("The parameters account SHOULD be a 20-byte non-zero address.");
            return AssetStorage.Get(account);
        }

        private static void Increase(UInt160 key, BigInteger value)
        {
            Put(key, Get(key) + value);
        }

        private static void Reduce(UInt160 key, BigInteger value)
        {
            var oldValue = Get(key);
            if (oldValue == value)
            {
                Balances.Delete(key);
            }
            else
            {
                Put(key, oldValue - value);
            }
        }

        public static bool Transfer(UInt160 from, UInt160 to, BigInteger amount, object data)
        {
            if (!from.IsValid || !to.IsValid)
            {
                throw new Exception("The parameters from and to should be 20-byte addresses");
            }

            if (amount < 0) 
            {
                throw new Exception("The amount parameter must be greater than or equal to zero");
            }

            if (!from.Equals(Runtime.CallingScriptHash) && !Runtime.CheckWitness(from))
            {
                throw new Exception("No authorization.");
            }
            
            if (Get(from) < amount)
            {
                throw new Exception("Insufficient balance");
            }

            Reduce(from, amount);
            Increase(to, amount);
            OnTransfer(from, to, amount);

            if (ContractManagement.GetContract(to) != null)
            {
                Contract.Call(to, "onPayment", CallFlags.None, new object[] { from, amount, data });
            }
            
            return true;
        }

        public static BigInteger BalanceOf(UInt160 account)
        {
            return Get(account);
        }

        [DisplayName("\_deploy")]
        public static void Deploy(object data, bool update)
        {
            if (!update)
            {
                var tx = (Transaction) Runtime.ScriptContainer;
                var owner = (Neo.UInt160) tx.Sender;
                Increase(owner, InitialSupply);
                OnTransfer(null, owner, InitialSupply);
            }
        }
    }
}