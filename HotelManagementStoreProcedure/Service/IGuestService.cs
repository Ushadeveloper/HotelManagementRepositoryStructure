using HotelManagementStorePDapper.Model;

namespace HotelManagementRepositoryStructure.Service
{
    public interface IGuestService
    {
        Task<List<Guest>> GetGuest();
        Task<Guest> GetGuest(int id);
        Task<int> CreateGuest(Guest guest);
        Task<int> UpdateGuest(Guest guest);
        Task<int> DeleteGuest(int Id);

    }


}

