using System;
using System.IO;

namespace BarokuChainuru
{
    class Program
    {
        public const string BlockDataDirectory = @".\Data\Blocks";
        public const string TransactionDataDirectory = @".\Data\Transactions";

        static void Main(string[] args)
        {
            // Ensure the data directories exist
            EnsureDirectoryPresent(BlockDataDirectory);
            EnsureDirectoryPresent(TransactionDataDirectory);

            // Setup blockchain
            var chain = new BlockChain();

            // Submit transactions to the chain
            chain.AddTransaction("{'MijnDataObject': 'Dit is mijn nieuwe data!'}");
            chain.AddTransaction("{'MijnDataObject': 'Hier is nog meer data :)'}");
            chain.AddTransaction("{'SuperAwesomeGameContractData': 'ABCDGEDEGYERTEFEDFWGDGWGFF'}");

            // TODO: Generate block + validate + add block to chain
            // chain.StartGeneratingBlocks

            Console.ReadLine();
        }

        static void EnsureDirectoryPresent(string path)
        {
            if (Directory.Exists(path)) { return; }

            var dataDir = Directory.CreateDirectory(path);
            Console.WriteLine($"Created directory '{dataDir.FullName}'");
        }
    }
}
