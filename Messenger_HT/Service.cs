using System.Data.SqlClient;

namespace Messenger_HT
{
    public class Service
    {
        public static string connection = "Server = DESKTOP-02F3BCI; Database = Messenger; Trusted_Connection = True;";
        public static string Register(string username)
        {
            using (SqlConnection connect = new SqlConnection())
            {
                connect.ConnectionString = connection;
                connect.Open();

                try
                {
                    var query = $"INSERT INTO Users (user_name)" +
                                $"VALUES ('{username}');";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    cmd.ExecuteNonQuery();
                    return "Ro'yhatdan o'tish muvaffaqiyatli yakunlandi!";
                }
                catch (SqlException)
                {
                    return $"{username} - foydalanuvchi oldin ro'yhatdan o'tgan!";
                }
            }
        }

        public static bool Log_In(string username)
        {
            using (SqlConnection connect = new SqlConnection())
            {
                connect.ConnectionString = connection;
                connect.Open();

                var CheckQuery = $"SELECT * FROM Users " +
                                 $"WHERE user_name = '{username}';";

                SqlCommand command = new SqlCommand(CheckQuery, connect);
                //command.ExecuteNonQuery();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["user_name"] != null)
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }
        }

        public static string SendMessage(string username, string message )
        {
            using (SqlConnection connect = new SqlConnection())
            {
                connect.ConnectionString = connection;
                connect.Open();

                //string time = DateTime.Now.ToString("HH:mm:ss");
                var query = $"INSERT INTO Messages (message, user_name)" +
                            $"VALUES ('{message}', '{username}')";
                SqlCommand command = new SqlCommand(query, connect);
                command.ExecuteNonQuery();
                GetAllMessages();

                return "Xabar jo'natildi!";
            }
        }

        public static void GetAllMessages()
        {
            using (SqlConnection connect = new SqlConnection())
            {
                connect.ConnectionString = connection;
                connect.Open();

                var query = "SELECT * FROM Messages;";
                SqlCommand command = new SqlCommand(query, connect);
                command.ExecuteNonQuery();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["user_name"] + "\t" + reader["message"] + "\t" + reader["time"]);
                        //Console.CursorLeft = Console.BufferWidth = 4;
                        //Console.CursorLeft = Console.BufferWidth - 4;
                        //Console.Write("[ok]");
                        //Console.WriteLine(reader["time"]);
                    }
                }
            }
        }
    }
}
