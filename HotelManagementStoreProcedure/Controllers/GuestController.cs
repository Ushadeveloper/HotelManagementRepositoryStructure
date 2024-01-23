using HotelManagementRepositoryStructure.Exceptions;
using HotelManagementRepositoryStructure.Service;
using HotelManagementStorePDapper.Model;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementRepositoryStructure.Controllers
{
    public class GuestController : BaseApiController
    {
        private readonly IGuestService _guestService;

        public GuestController(IGuestService guestService)
        {
            _guestService = guestService;
        }
        [HttpGet("api/Guest")]
        public async Task<IActionResult> DisplayGuest()
        {

            var result = await _guestService.GetGuest();
            if (result.Count == null)
            {
                throw new EntityNotFoundException($"result not found in database.");
            }
            return Success(result);


        }
        [HttpGet("api/Guest/{id}")]
        public async Task<IActionResult> DisplyGuestId(int id)
        {
            var result = await _guestService.GetGuest(id);
            if (result == null)
            {
                throw new EntityNotFoundException($"{id} not found in database.");
            }
            return Success(result);
        }
        [HttpPost("api/Guest")]
        public async Task<IActionResult> InsertGuest([FromBody] Guest guest)
        {
            try
            {
                if (guest == null)
                    throw new ArgumentNullException(nameof(guest));
                int lastInsertedId = await _guestService.CreateGuest(guest);
                return CreatedWithId(Convert.ToString(lastInsertedId));

            }
            catch (Exception e)
            {
                throw new InternalServerException(e.ToString());
            }
        }
        [HttpPut("api/Guest/{id}")]
        public async Task<IActionResult> UpdateGuests(int id, [FromBody] Guest guest)
        {
            try
            {
                var guestId = await _guestService.GetGuest(id);
                if (guestId == null)
                {
                    throw new EntityNotFoundException($"EmployeeId {id} not found..");
                }
                guest.Id = id;
                int result = await _guestService.UpdateGuest(guest);
                var update = await _guestService.GetGuest(id);

                return Success(update);

            }
            catch (Exception e)
            {
                throw new InternalServerException(e.Message);
            }
        }
        [HttpDelete("api/Guest/{id}")]
        public async Task<IActionResult> DeletedGuest(int id, [FromBody] Guest guest)
        {
            try
            {
                var guestId = await _guestService.GetGuest(id);
                if (guestId == null)
                {
                    throw new EntityNotFoundException($"EmployeeId {id} not found..");

                }

                int result = await _guestService.DeleteGuest(id);
                return Success(result);

            }
            catch (Exception e)
            {
                throw new InternalServerException(e.Message);
            }
        }


    }
}
