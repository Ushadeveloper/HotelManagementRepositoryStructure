namespace HotelManagementRepositoryStructure.Exceptions
{
    public class InternalServerException : ApplicationException
    {
        public InternalServerException() : base("invalid server process request") { }

        public InternalServerException(string message) : base(message) { }

        public InternalServerException(string message, Exception ex) : base(message, ex) { }

    }
}