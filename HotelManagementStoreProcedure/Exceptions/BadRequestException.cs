namespace HotelManagementRepositoryStructure.Exceptions
{
    public class BadRequestException : ApplicationException
    {
        public BadRequestException() : base("invalid client request") { }

        public BadRequestException(string message) : base(message) { }

        public BadRequestException(string message, Exception ex) : base(message, ex) { }

    }
}
