using System.Runtime.Serialization;

namespace StarWars.Domain.Exceptions
{
    [Serializable]
    public class IntegrationException : Exception
    {
        public IntegrationException()
        {
        }

        public IntegrationException(string? message) : base(message)
        {
        }

        public IntegrationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected IntegrationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}