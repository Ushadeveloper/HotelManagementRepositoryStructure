using HotelManagementRepositoryStructure.Exceptions;
using HotelManagementRepositoryStructure.Model;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementRepositoryStructure.Controllers
{
    [ApiController]
    [ApplicationExceptionFilter]
    public class BaseApiController : Controller
    {

        private JsonResponse BuildSuccessResponse(dynamic data)
        {
            JsonResponse model = new JsonResponse();
            model.success = true;
            model.data = data;
            MetaData objmetadata = new MetaData();
            model.metadata = objmetadata;
            return model;
        }
        protected IActionResult Success(dynamic data = null)
        {
            var responseValue = BuildSuccessResponse(data);
            return Ok(responseValue);
        }

        protected IActionResult CreatedWithId(string id)
        {
            return Success(new { id = id });
        }
        protected IActionResult CreatedWithIds(List<string> ids, string segment, string baseApi = "api", bool showUri = true)
        {
            List<dynamic> json = new List<dynamic>();

            foreach (var value in ids)
            {
                json.Add(new { id = value });
            }
            return Success(json);
        }
    }
}