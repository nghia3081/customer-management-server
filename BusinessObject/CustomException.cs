using System.Text.Json;

namespace BusinessObject
{
    public class DefinedException
    {
        public int Code { get; set; }
        public string Message { get; set; } = null!;
    }
    [Serializable]
    public class CustomerManagementException : Exception
    {
        private static readonly IEnumerable<DefinedException> definedExceptions
        = JsonSerializer.Deserialize<IEnumerable<DefinedException>>(File.ReadAllText("exceptions.json") ?? string.Empty, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
        }) ?? Enumerable.Empty<DefinedException>();

        public Guid TracingId { get; private set; }
        public int ExceptionCode { get; private set; }
        private CustomerManagementException(string message, Exception? innerException = null) : base(message, innerException) { }
        protected CustomerManagementException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        public CustomerManagementException(int code, Exception? innerException = null)
        {
            Init(code, innerException);

        }
        private void Init(int code, Exception? innerException = null)
        {
            ExceptionCode = code;
            TracingId = Guid.NewGuid();
            var exceptionBody = definedExceptions.First(ex => ex.Code == code) ?? throw new CustomerManagementException(5000);
            string message = $"{exceptionBody.Message}";
            throw new CustomerManagementException(message, innerException);
        }
    }
}
