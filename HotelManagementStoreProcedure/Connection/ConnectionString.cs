namespace HotelManagementRepositoryStructure.Connection
{
    public class ConnectionString:IConnectionString
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        public ConnectionString(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("dbConnection");
        }
        public string GetConnectionString()
        {
            return _configuration.GetConnectionString("dbConnection");
        }
        
    }
}
