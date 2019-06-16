using GTANetworkAPI;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace fristresource
{

    class Connection
    {
        private string connectionString = string.Empty;
        private MySqlConnection databaseConnection = null;

        public Connection()
        {
            connectionString = "datasource=localhost;port=3306;username=Yorch;password=test;database=test;";
            databaseConnection = new MySqlConnection(connectionString);
        }

        public MySqlConnection getConnection()
        {
            return databaseConnection;
        }
    }
}
