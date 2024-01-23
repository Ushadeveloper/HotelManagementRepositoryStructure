namespace HotelManagementRepositoryStructure.Model
{
    public class JsonResponse
    {
        public bool success { get; set; }
        public dynamic? data {get; set;}
        public Error? error { get; set;}
        public MetaData? metadata { get; set;}
        
    }

    public class Error
    {
        public int? code { get; set; }
        public int? subCode { get; set; }
        public string? message { get; set; }
        public string? detail { get; set; }
        public bool? isUserError { get; set; }
    }
    public class MetaData { }
}
