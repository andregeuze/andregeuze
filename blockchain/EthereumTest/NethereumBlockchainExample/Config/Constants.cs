using NethereumBlockchainExample.Helpers;

namespace NethereumBlockchainExample.Config
{
    public static class Constants
    {
        public const string UrlRinkeby = "https://rinkeby.infura.io/Fr9e610xMO6l2oViYQCY";
        public static DecentralisatieAccount DummyAccount { get; } = new DecentralisatieAccount(nameof(DummyAccount), "0x5388964650e77024538E262715A152571A516204", "2a29a769c0f7c95e69a417ccafadc1e1f5cbedcd77eeb920bbdfc38218fb5d3c", "");

    }
}
