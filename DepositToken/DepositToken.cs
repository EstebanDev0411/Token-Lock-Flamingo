using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using FLMToken;
using System.ComponentModel;
using System.Numerics;

namespace StakingContract
{
    public class Staking : SmartContract
    {

        public static readonly byte[] StakingAddressPrefix = {0x01, 0x23, 0x45, 0x67, 0x89, 0xab};

        public static void Main()
        {
            // Method to be called
        }

        [DisplayName("stake")]
        
        public static void Stake(byte[] address, BigInteger amount, int duration)
        {
            // Check that the sender has enough funds
            BigInteger balance = Neo.SmartContract.Framework.Services.Neo.Runtime.GetBalance(Callers(0));
            if (balance < amount) return;

            // Transfer the funds to the staking address
            FLMToken.Transfer(Callers(0), StakingAddress, amount);

            // Store the staking information in the contract storage
            byte[] amountPrefix = new byte[] { 0x01 };
            byte[] lengthPrefix = new byte[] { 0x02 };
            Storage.Put(Storage.CurrentContext, informationPrefix.Concat(address), amount);
            Storage.Put(Storage.CurrentContext, lengthPrefix.Concat(address), duration);
        }

        [DisplayName("withdraw")]
        public static void Withdraw(byte[] address)
        {
            // Get the staking information from the contract storage
            byte[] key = new byte[] { 0x01 };
            BigInteger amount = Storage.Get(Storage.CurrentContext, key.Concat(address)).AsBigInteger();
            if (amount == 0) return;

            key = new byte[] { 0x02 };
            int duration = (int)Storage.Get(Storage.CurrentContext, key.Concat(address)).AsBigInteger();

            // Check that the staking period has ended
            uint timestamp = Neo.SmartContract.Framework.Services.Neo.Runtime.Time;
            uint stakedTime = timestamp - (uint)duration;
            if (stakedTime < Storage.Get(Storage.CurrentContext, key.Concat(address)).AsBigInteger()) return;

            // Calculate the reward amount
            BigInteger reward = amount * 10 / 100;

            // Transfer the staked amount and reward to the user
            Transfer(StakingAddress, address, amount + reward);

            // Delete the staking information from the contract storage
            key = new byte[] { 0x01 };
           
}