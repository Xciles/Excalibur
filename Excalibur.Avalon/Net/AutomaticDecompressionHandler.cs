// ReSharper disable once CheckNamespace
namespace System.Net.Http
{
    /// <summary>
    /// Just a HttpClientHandler that sets AutomaticDecompression to GZip and Deflate
    /// </summary>
    public class AutomaticDecompressionHandler : HttpClientHandler
    {
        /// <inheritdoc />
        /// <summary>
        /// Constructs a new instance and setting AutomaticDecompression to GZip | Deflate
        /// </summary>
        public AutomaticDecompressionHandler()
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
        }
    }
}