using Microsoft.Data.Sqlite;

namespace nezter_backend
{

    public class UserServiceSqlite
    {

        readonly IConfiguration _configuration;
        public UserServiceSqlite(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<UserView>> GetUsers(int numPerPage = 0)
        {
            var users = new List<UserView>();
            try
            {
                using (var connection = new SqliteConnection("Data Source=Nezter.db"))
                {
                    await connection.OpenAsync();
                    string sql = "select * from Users";
                    SqliteCommand command = new SqliteCommand(sql, connection);
                    SqliteDataReader reader = command.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        return users;
                    }

                    while (reader.Read())
                    {
                        users.Add(new UserView
                        {
                            Nombre = reader.GetString(0),
                            Apellido = reader.GetString(1),
                            Direccion = reader.GetString(2),
                            Telefono = reader.GetString(3),
                            CodigoPostal = reader.GetString(4),
                            Estado = reader.GetString(5),
                            Ciudad = reader.GetString(6),
                        });
                    }
                }

            }
            catch (System.Exception)
            {

                throw;
            }

            return users;

        }

        public async Task<bool> CreateUser(User user)
        {

            try
            {
                using (var connection = new SqliteConnection("Data Source=Nezter.db"))
                {
                    await connection.OpenAsync();
                    string sql = $"INSERT into Users VALUES ('{user.Nombre}','{user.Apellido}',"+
                    $"'{user.Direccion}','{user.Telefono}','{user.CodigoPostal}',1,1 )";

                    SqliteCommand command = new SqliteCommand(sql, connection);
                    int result = await command.ExecuteNonQueryAsync();
                    return result > 0;
                }

            }
            catch (System.Exception e)
            {

                return false;
            }

        }


        public async Task<bool> Deactivate(string name)
        {

            try
            {
                using (var connection = new SqliteConnection("Data Source=Nezter.db"))
                {
                    await connection.OpenAsync();
                    var sql = "Delete from Users where Nombre='Memo'";
                    SqliteCommand command = new SqliteCommand(sql, connection);
                    int result = await command.ExecuteNonQueryAsync();

                    return result > 0;
                }
            }
            catch (System.Exception)
            {

                return false;
            }

        }

    }



}