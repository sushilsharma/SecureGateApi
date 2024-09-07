using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SecureGate.Framework.DataAccess
{
    public class ConnectionManager
    {
        private static string _connectionString = string.Empty;
        
        static ConnectionManager()
        {

        }
    
        public enum ConnectTo
        {
            SecureGateDatabaseREADConnection,
            SecureGateDatabaseWriteConnection,
            CommonDB,
            Multitenancy,
            SecureGateDocument
        }

        public static SqlConnection Create(ConnectTo connectToDatabase)
        {
            var connectionKey = connectToDatabase.ToString();

            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            var root = configurationBuilder.Build();
            _connectionString = root.GetSection("ConnectionStrings").GetSection(connectionKey).Value;


            var connectionSettings = _connectionString;

            if (connectionSettings == null)
                throw new ApplicationException(string.Format("Connection string not initialized for key {0}",
                    connectionKey ?? "NULL"));

            return new SqlConnection(connectionSettings);
        }

        public static SqlConnection Create(string connectionString)
        {
            if (connectionString == null)
                throw new ApplicationException("Invalid connection string");

            return new SqlConnection(connectionString);
        }
    }
}

