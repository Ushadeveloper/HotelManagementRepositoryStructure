namespace HotelManagementRepositoryStructure.Exceptions
{
    public class NoContentException : ApplicationException
    {
        public NoContentException() : base("record not found") { }

        public NoContentException(string message) : base(message) { }

        public NoContentException(string message, Exception ex) : base(message, ex) { }
    }
}
