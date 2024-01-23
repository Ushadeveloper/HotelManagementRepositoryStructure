namespace HotelManagementRepositoryStructure.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public ValidationException() : base("invalid client request") { }

        public ValidationException(string message) : base(message) { }

        public ValidationException(string message, Exception ex) : base(message, ex) { }

    }
}