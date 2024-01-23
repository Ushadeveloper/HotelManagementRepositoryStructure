using HotelManagementRepositoryStructure.Connection;
using HotelManagementStorePDapper.Model;

namespace HotelManagementRepositoryStructure.Repository
{
    public class GuestRepository:EntityRepository<Guest>,IGuestRepository
    {
        public GuestRepository(IConnectionString context ): base(context) { }
        
    }
}
