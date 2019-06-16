using System;
using GTANetworkAPI;
using MySql.Data.MySqlClient;

namespace fristresource
{
    public class Main : Script
    {
        private Connection conn = null;
        private MySqlConnection connection;
        public Main()
        {
            conn = new Connection();
            connection = conn.getConnection();
        }
        //const camerasManager = require('./camerasManager.js');


        //Evento de login
        [RemoteEvent("OnPlayerLoginAttempt")]
        public void OnPlayerLoginAttempt(Client player, string username, string password)
        {
            //Obtener datos desde el login del juego
            NAPI.Util.ConsoleOutput($"[Login Attempt] Username {username} | Password: {password}");

            //Sentencia SQL
            string query = "SELECT username,password FROM users WHERE username= '" + username + "'  AND password= '" + password + "'";

            // Prepara la conexión

            MySqlCommand commandDatabase = new MySqlCommand(query, connection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;

            // A consultar !
            try
            {
                // Abre la base de datos
                connection.Open();

                // Ejecuta la consultas
                reader = commandDatabase.ExecuteReader();

                // Hasta el momento todo bien, es decir datos obtenidos

                // IMPORTANTE :#
                // Si tu consulta retorna un resultado, usa el siguiente proceso para obtener datos

                if (reader.HasRows)
                {
                    player.TriggerEvent("LoginResult", 1);
                    return;
                }
                player.TriggerEvent("LoginResult", 0);
            }
            catch (MySqlException e)
            {
                System.Console.WriteLine(e.GetBaseException());
            }
            finally
            { 
                // Cerrar la conexión
                connection.Close();
            }
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
