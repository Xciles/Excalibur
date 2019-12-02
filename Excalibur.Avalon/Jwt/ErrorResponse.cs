namespace Excalibur.Avalon.Jwt
{
    /// <summary>
    /// Error response entity which contains a message
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// The error message
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// Error response entity which contains some class for additional data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ErrorResponse<T> : ErrorResponse
    {
        /// <summary>
        /// Additional data entity
        /// </summary>
        public T AdditionalData { get; set; }
    }
}