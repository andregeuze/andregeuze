using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BarokuChainuru
{
    public class BlockChain
    {
        private static string _genesisBlockData = "If you can calculate this one, you are awesome :)";

        public BlockChain()
        {
            // Add the genesis transaction


            // Add the genesis block
            var genesis = new Block { Index = 1, TimeStamp = new DateTime(1465154705), PreviousHash = "0", Data = new[] { new Transaction { Data = _genesisBlockData } } };
            genesis.Hash = CalculateHash(genesis);

            WriteBlockToStorage(genesis);
        }

        private string CalculateHash(Block block)
        {
            var hashString = $"{block.Index}{block.PreviousHash}{block.TimeStamp.Ticks}{block.Data}";

            

            var hash = SHA256.Create();
            var bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(hashString));
            return BitConverter.ToString(bytes).Replace("-", "");
        }
        
        //public Block CalculateBlock(Block lastBlock)
        //{
        //    var transactionsForNextBlock = _transactions.DequeueChunk(10).ToArray();

        //    var block = new Block
        //    {
        //        Index = lastBlock.Index + 1,
        //        TimeStamp = DateTime.Now,
        //        PreviousHash = lastBlock.Hash,
        //        Data = transactionsForNextBlock
        //    };

        //    block.Hash = CalculateHash(block);

        //    return block;
        //}

        private void WriteBlockToStorage(Block block)
        {
            using (StreamWriter blockWriter = new StreamWriter($@"{Program.BlockDataDirectory}\{block.Index}"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(blockWriter, block);
            }
            
            Console.WriteLine($"-- Block {block.Index} --");
            Console.WriteLine($"Hash:\t\t{block.Hash}");
            Console.WriteLine($"Previous Hash:\t{block.PreviousHash}");
            Console.WriteLine($"Timestamp:\t{block.TimeStamp.Ticks}");
            Console.WriteLine($"Data:");
            foreach (var transaction in block.Data)
            {
                Console.WriteLine($"\tTransaction: {transaction.Data}");
            }
            Console.WriteLine($"-- End of Block {block.Index} --");
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine();
        }

        public void AddTransaction(string data)
        {
            var transaction = new Transaction { TimeStamp = DateTime.Now, Data = data };

            using (StreamWriter transactionWriter = new StreamWriter($@"{Program.TransactionDataDirectory}\{transaction.TimeStamp.Ticks}"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(transactionWriter, transaction);
            }
        }
        
        //public Block GenerateNextBlock(string blockData)
        //{
        //    var previousBlock = _transactions.Last();
        //    var nextTimestamp = DateTime.Now;

        //    var nextBlock = new Block(previousBlock.Hash, DateTime.Now, blockData);
        //    nextBlock.Hash = CalculateHash(nextBlock);
        //    return nextBlock;
        //}

        //private bool ValidateBlock(Block newBlock)
        //{
        //    newBlock.

        //    if (previousBlock.Hash != newBlock.PreviousHash)
        //    {
        //        // Make sure that no block can be added further down the chain. This prevents changing the history.
        //        Console.WriteLine("invalid previous hash");
        //    }
        //    else if (newBlock.Hash != CalculateHash(newBlock))
        //    {
        //        Console.WriteLine("invalid hash");
        //        return false;
        //    }

        //    return true;
        //}

    }
}