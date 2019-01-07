using Nethereum.Hex.HexTypes;
using Nethereum.Util;
using Nethereum.Web3;
using NethereumBlockchainExample.Config;
using System;
using System.Numerics;
using System.Threading.Tasks;

namespace NethereumBlockchainExample
{
    class Program
    {
        private UnitConversion _unitConversion = new UnitConversion();

        private BigInteger gasPrice = new BigInteger(1100000001);
        private HexBigInteger gasPriceHbi = new HexBigInteger(new BigInteger(1100000001));

        private BigInteger gasLimit = new BigInteger(6000000);
        private HexBigInteger gasLimitHbi = new HexBigInteger(new BigInteger(6000000));
        private HexBigInteger hbiNull = null;

        static void Main(string[] args)
        {
            var p = new Program();
            var result = p.GoUploadContract().GetAwaiter().GetResult();
            Console.WriteLine($"Application exit, result: {result}");
            Console.ReadLine();
        }

        public async Task<bool> GoUploadContract()
        {
            if (string.IsNullOrWhiteSpace(Constants.UrlRinkeby))
            {
                Console.WriteLine("Error: Please make sure the variable UrlRinkeby is filled in.");
                return false;
            }

            var account = Constants.DummyAccount;
            if (!account.PublicAddress.StartsWith("0x"))
            {
                Console.WriteLine("Error: Please add a public and matching private key to the DummyAccount");
                return false;
            }

            var contractName = "ExampleContract";

            var web3 = new Web3(account.Account, Constants.UrlRinkeby);
            web3.TransactionManager.DefaultGasPrice = new HexBigInteger(gasPrice);

            Console.WriteLine("Deploying contract...");
            var uploadedContractAddress = await BlockchainHandler.DeploySolidityContract(web3, account, contractName, gasLimit, gasPrice, hbiNull);
            Console.WriteLine($"Contract uploaded. See following url: https://rinkeby.etherscan.io/address/{uploadedContractAddress}");

            return true;
        }
    }
}
