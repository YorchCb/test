using System;
using GTANetworkAPI;
using MySql.Data.MySqlClient;

namespace fristresource
{
    public class Main : Script
    {
        public Main()
        {
            
        }

        //Evento de login
        [RemoteEvent("OnPlayerLoginAttempt")]
        public void OnPlayerLoginAttempt(Client player, string username, string password)
        {
            var LoginPlayer = false;
            //Obtener datos desde el login del juego
            NAPI.Util.ConsoleOutput($"[Login Attempt] Username {username} | Password: {password}");

            //Datos para conectar a la base da datos
            string connectionString = "datasource=localhost;port=3306;username=Yorch;password=test;database=test;";

            //Sentencia SQL
            string query = "SELECT username,password FROM users WHERE username= '" + username + "'  AND password= '" + password + "'";

            // Prepara la conexión
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;

            // A consultar !
            try
            {
                // Abre la base de datos
                databaseConnection.Open();

                // Ejecuta la consultas
                reader = commandDatabase.ExecuteReader();

                // Hasta el momento todo bien, es decir datos obtenidos

                // IMPORTANTE :#
                // Si tu consulta retorna un resultado, usa el siguiente proceso para obtener datos

                if (reader.HasRows)
                {
                    LoginPlayer = true;
                }  
            }
            finally
            { // Cerrar la conexión
                databaseConnection.Close();
            }
            
            if (LoginPlayer == true)
            {
                player.TriggerEvent("LoginResult", 1);
            }
            else player.TriggerEvent("LoginResult", 0);
        }

        [Command("cv")]
        public void CMD_Cv(Client client, string vehicle_name)
        {
            
            uint hash = NAPI.Util.GetHashKey(vehicle_name);

            Vehicle veh = NAPI.Vehicle.CreateVehicle(hash, client.Position.Around(5), 0, 0, 0);

            client.SetData("PersonalVehicle", veh);

            

           
        }

        [Command("healme")]
        public void CMD_Healme(Client client)
        {
            client.Health += 10;
            client.SendChatMessage("~g~U just healed");
        }
    }
}
