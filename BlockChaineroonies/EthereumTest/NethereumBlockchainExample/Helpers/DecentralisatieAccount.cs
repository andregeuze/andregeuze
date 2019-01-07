using Nethereum.Web3.Accounts;
using System;

namespace NethereumBlockchainExample.Helpers
{
    [Serializable]
    public class DecentralisatieAccount
    {
        public string AccountName { get; private set; }
        public string PublicAddress { get; private set; }
        public string PrivateKey { get; private set; }
        public string Password { get; private set; }

        private Account account;
        public Account Account
        {
            get
            {
                if (account == null)
                {
                    account = new Account(PrivateKey);
                }
                return account;
            }
        }

        public DecentralisatieAccount(string accountName, string publicAddress, string privateKey, string password)
        {
            AccountName = accountName;
            PublicAddress = publicAddress;
            PrivateKey = privateKey;
            Password = password;
        }

        public override string ToString()
        {
            return $"{AccountName} > {PublicAddress}";
        }
    }
}
