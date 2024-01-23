namespace HotelManagementRepositoryStructure.Exceptions

{
    public class ForbiddenException : ApplicationException
    {
        public ForbiddenException() : base() { }
        public ForbiddenException(string message) : base(message) { }

        public ForbiddenException(string message, Exception ex) : base(message, ex) { }

    }
}