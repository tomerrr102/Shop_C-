using System.Runtime.Serialization;

namespace Simulation
{
    [Serializable]
    internal class MissingInformationExeptions : Exception
    {
        public MissingInformationExeptions()
        {
        }

        public MissingInformationExeptions(string? message) : base(message)
        {
        }

        public MissingInformationExeptions(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected MissingInformationExeptions(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}