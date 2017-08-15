// ReSharper disable once CheckNamespace
namespace System.Net.Http
{
    public class AutomaticDecompressionHandler : HttpClientHandler
    {
        public AutomaticDecompressionHandler()
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
        }
    }
}