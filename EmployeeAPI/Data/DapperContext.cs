﻿using System.Data;
using System.Data.SqlClient;

namespace EmployeeAPI.Data
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("EmployeeconnectionString");
        }
        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
       
    }
}
