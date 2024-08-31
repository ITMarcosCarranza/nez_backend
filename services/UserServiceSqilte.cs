using System.Data;
using Microsoft.Data.Sqlite;
using SQLitePCL;

namespace nezter_backend
{

    public class UserServiceSqlite
    {

        readonly IConfiguration _configuration;
        public UserServiceSqlite(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<UserView>> GetUsers(int offset)
        {
            var users = new List<UserView>();
            try
            {
                using (var connection = new SqliteConnection("Data Source=Nezter.db"))
                {
                    await connection.OpenAsync();
                    string sql = "select  Id,Nombre,Apellido,Telefono " +
                        "from Users offset @OFFSET";
                    SqliteCommand command = new SqliteCommand(sql, connection);
                    SqliteDataReader reader = command.ExecuteReader();
                    command.Parameters.AddWithValue("@OFFSET", offset * 10);
                    if (!reader.HasRows)
                    {
                        return users;
                    }

                    while (reader.Read())
                    {
                        users.Add(new UserView
                        {
                            Id=reader.GetInt32(0),
                            Nombre = reader.GetString(0),
                            Apellido = reader.GetString(1),
                            Telefono = reader.GetString(3),
                           
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
                    string sql = $"INSERT into Users "+ 
                    "VALUES(@NOMBRE,@APELLIDO,@DIRECCION,@TELEFONO,@ZIP,@ESTADO,@CIUDAD,@TIPOUSUARIO,@LOGIN,@PASSWORD)";
                    SqliteCommand command = new SqliteCommand(sql, connection);
                    command.Parameters.AddWithValue("@NOMBRE",user.Nombre);
                    command.Parameters.AddWithValue("@APELIIDO",user.Apellido);
                    command.Parameters.AddWithValue("@DIRECCION",user.Direccion);
                    command.Parameters.AddWithValue("@TELEFONO",user.Telefono);
                    command.Parameters.AddWithValue("@ZIP",user.CodigoPostal);
                    command.Parameters.AddWithValue("@ESTADO",user.Estado);
                    command.Parameters.AddWithValue("@CIUDAD",user.Ciudad);
                    command.Parameters.AddWithValue("@TIPOUSUARIO",user.TipoUsuario);
                    command.Parameters.AddWithValue("@LOGIN",user.Login);
                    command.Parameters.AddWithValue("@PASSWORD",user.Password);
                    int result = await command.ExecuteNonQueryAsync();
                    return result > 0;
                }

            }
            catch (System.Exception e)
            {

                return false;
            }

        }


        public async Task<bool> Deactivate(int Id)
        {

            try
            {
                using (var connection = new SqliteConnection("Data Source=Nezter.db"))
                {
                    await connection.OpenAsync();
                    var sql = "Delete from Users where Id=@ID";
                    SqliteCommand command = new SqliteCommand(sql, connection);
                    command.Parameters.AddWithValue("@ID", Id);
                    int result = await command.ExecuteNonQueryAsync();

                    return result > 0;
                }
            }
            catch (System.Exception)
            {

                return false;
            }

        }

        public async Task<User> GetUser(int id)
        {

            var user = new User();
            try
            {
                using (var connection = new SqliteConnection("Data Source=Nezter.db"))
                {
                    var sql = "select * from Users where Id=@id";
                    SqliteCommand command = new SqliteCommand(sql, connection);
                    command.Parameters.AddWithValue("@ID", id);
                    connection.Open();
                    SqliteDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        user.Nombre = reader.GetString(0);
                        user.Apellido = reader.GetString(1);
                        user.Direccion = reader.GetString(2);
                        user.Telefono = reader.GetString(3);
                        user.CodigoPostal = reader.GetString(3);
                        user.TipoUsuario=reader.GetInt32(3);
                        user.Estado = reader.GetInt32(6);
                        user.Ciudad = reader.GetInt32(5);
                        user.Login = reader.GetString(4);
                        user.Login = reader.GetString(3);
                    }
                }


            }
            catch (System.Exception)
            {

                throw;
            }

            return user;
        }

    }



}