using System;
using System.IO;
using System.Reflection;

namespace NethereumBlockchainExample.Helpers
{
    public static class PathHelper
    {
        private static string CreateLocationOfBinDirectory()
        {
            var startupAssembly = typeof(PathHelper).GetTypeInfo().Assembly;
            var cb = startupAssembly.CodeBase;

            UriBuilder uri = new UriBuilder(cb);
            string path = Uri.UnescapeDataString(uri.Path);
            var assemblyDir = Path.GetDirectoryName(path);

            return assemblyDir;
        }

        public static Lazy<string> LocationOfBinDirectory { get; } = new Lazy<string>(() => CreateLocationOfBinDirectory());
    }
}
