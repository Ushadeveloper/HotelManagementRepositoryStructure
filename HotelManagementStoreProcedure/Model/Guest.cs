namespace HotelManagementStorePDapper.Model
{
    public class Guest : BaseEntity
    {
        public string Name { get; set; }
        public DateTime? DOB { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
