using Dapper;
using HotelManagementRepositoryStructure.Repository;
using HotelManagementStorePDapper.Model;
using System.Data;

namespace HotelManagementRepositoryStructure.Service
{
    public class GuestService : IGuestService
    {
        private readonly IGuestRepository _guestRepository;
        public GuestService(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }
        public async Task<List<Guest>> GetGuest()
        {
            var listGuest = await _guestRepository.GetAllAsync("GetAllGuest", null);
            return listGuest;
        }
        public async Task<Guest> GetGuest(int id)
        {
            DynamicParameters parameters= new DynamicParameters();
            parameters.Add("@id", id);
            var result = await _guestRepository.GetByIdAsync("GetGuestId", parameters);
            return result;
        }
        public async Task<int> CreateGuest(Guest guest)
        {
            string dat = Convert.ToString(guest.DOB);
            System.Globalization.CultureInfo ci = System.Globalization.CultureInfo.CreateSpecificCulture("en-GB");
            var testDate = Convert.ToDateTime(dat, ci.DateTimeFormat).ToString("MM/dd/yyyy");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Name", guest.Name);
            parameters.Add("@DOB", testDate);
            parameters.Add("@Email", guest.Email);
            parameters.Add("@Phone", guest.Phone);
            parameters.Add("@Address", guest.Address);
            parameters.Add("@lastInsertedId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await _guestRepository.Create("SaveGuestData", parameters);
            int lastInsertedId = parameters.Get<int>("@lastInsertedId");

            return lastInsertedId;
        }

        public async Task<int> UpdateGuest(Guest guest)
        {
            string dat = Convert.ToString(guest.DOB);
            System.Globalization.CultureInfo ci = System.Globalization.CultureInfo.CreateSpecificCulture("en-GB");
            var testDate = Convert.ToDateTime(dat, ci.DateTimeFormat).ToString("MM/dd/yyyy");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Name", guest.Name);
            parameters.Add("@DOB", testDate);
            parameters.Add("@Email", guest.Email);
            parameters.Add("@Phone", guest.Phone);
            parameters.Add("@Address", guest.Address);
            parameters.Add("@Id", guest.Id);
            var result = await _guestRepository.Update("UpdateGuests", parameters);
            return result;


        }
        public async Task<int> DeleteGuest(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);
            var result = await _guestRepository.Delete("DeleteGuests", parameters);
            return result;
        }


    }
}
