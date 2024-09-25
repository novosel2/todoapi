namespace Core.Exceptions
{
    public class CreationFailedException : Exception
    {
        public CreationFailedException()
        {
        }

        public CreationFailedException(string? message) : base(message)
        {
        }

        public CreationFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
