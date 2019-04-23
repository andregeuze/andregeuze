using System.IO;

namespace NethereumBlockchainExample.Helpers
{
    public class ContractOnDisk
    {
        public string Name { get; private set; }

        public string Abi { get; private set; }
        public string Bin { get; private set; }
        public string Json { get; private set; }

        public ContractOnDisk(string name)
        {
            Name = name;

            var location = PathHelper.LocationOfBinDirectory.Value;

            var contractsDir = Path.Combine(location, "SolidityContracts", "bin", "contracts");

            Abi = File.ReadAllText(Path.Combine(contractsDir, $"{name}.abi"));
            Bin = File.ReadAllText(Path.Combine(contractsDir, $"{name}.bin"));
            Json = File.ReadAllText(Path.Combine(contractsDir, $"{name}.json"));
        }
    }
}
