namespace HotelManagementRepositoryStructure.Exceptions
{
    public class EntityNotFoundException : ApplicationException
    {
        public EntityNotFoundException() : base("record does not exist") { }

        public EntityNotFoundException(string message) : base(message) { }

        public EntityNotFoundException(string message, Exception ex) : base(message, ex) { }
    }
}