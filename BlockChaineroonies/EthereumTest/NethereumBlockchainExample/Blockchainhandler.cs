using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Util;
using Nethereum.Web3;
using NethereumBlockchainExample.Helpers;
using System;
using System.Numerics;
using System.Threading.Tasks;
using static Nethereum.Util.UnitConversion;

namespace NethereumBlockchainExample
{
    public class BlockchainHandler
    {
        private static readonly UnitConversion UnitConversion = new UnitConversion();

        public static async Task<string> DeploySolidityContract(Web3 web3, DecentralisatieAccount acc, string contractName, BigInteger gasLimit, BigInteger gasPrice, HexBigInteger value, params object[] values)
        {
            var ctr = new ContractOnDisk(contractName);

            web3.TransactionManager.DefaultGasPrice = new HexBigInteger(gasPrice);

            var gasLimitHbi = new HexBigInteger(gasLimit);

            var estimatedGas = await web3.Eth.DeployContract.EstimateGasAsync(ctr.Abi, ctr.Bin, acc.PublicAddress, values);

            var tot = estimatedGas.Value * gasPrice;
            if (value != null)
            {
                tot += value;
            }
            var totEth = UnitConversion.FromWei(tot, EthUnit.Ether);

            Console.WriteLine($"Estimated Gas Usage: {estimatedGas.Value}. Estimated Ether cost: {totEth}");

            var transactionHash = await web3.Eth.DeployContract.SendRequestAsync(ctr.Abi, ctr.Bin, acc.PublicAddress, gasLimitHbi, value, values);
            Console.WriteLine($"Contract deployment ({contractName}): Waiting for transaction with hash '{transactionHash}' to be mined...");

            TransactionReceipt receipt = null;
            while (receipt == null)
            {
                receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);

                await Task.Delay(100);
            }

            Console.WriteLine($"Contract {contractName} deployed. Gas used for deploying contract: {receipt.GasUsed.Value}");
            var contractAddress = receipt.ContractAddress;

            return contractAddress;
        }

        public static async Task<TransactionReceipt> WaitForTransactionToBeMined(Web3 web3, string transactionHash)
        {
            TransactionReceipt receipt = null;
            while (receipt == null)
            {
                receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
                await Task.Delay(1);
            }
            return receipt;
        }
    }
}
