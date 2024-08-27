using Microsoft.Data.SqlClient;
namespace nezter_backend
{
    

    public class UserService{

        readonly IConfiguration _configuration;
        public UserService(IConfiguration configuration){
            _configuration=configuration;
        }   

        public List<UserView>GetUsers(int numPerPage=0){
           var users = new List<UserView>();
           try{
            string connectionStr = _configuration.GetConnectionString("SQLServer");
            using (SqlConnection connection = new SqlConnection(connectionStr)){
                SqlCommand command = new SqlCommand();
                command.CommandText ="GETUSERS";
                command.CommandType= System.Data.CommandType.StoredProcedure;

                
                connection.Open();

                using(SqlDataReader reader =command.ExecuteReader()){
                    users.Add(new UserView{
                            Nombre = reader[0].ToString(),
                            Apellido=reader[1].ToString(),
                            Direccion = reader[2].ToString(),
                            Telefono = reader[3].ToString(),
                            CodigoPostal = reader[4].ToString(),
                            Ciudad = ""

                    });
                }

            }} catch(Exception e) {
                Console.WriteLine(e.Message);
            }
            
            return users;

        }

        public bool CreateUser(User user)
        {
            string connectionStr = _configuration.GetConnectionString("SQLServer");
            using (SqlConnection connection = new SqlConnection(connectionStr)){
                SqlCommand command = new SqlCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText="CREATEUSER";
                command.Parameters.AddWithValue("@NAME",user.Nombre);
                command.Parameters.AddWithValue("@LAST_NAME",user.Apellido);
                command.Parameters.AddWithValue("@ADRESS",user.Direccion);
                command.Parameters.AddWithValue("@PHONE",user.Telefono);
                command.Parameters.AddWithValue("@ZIPCODE",user.CodigoPostal);
                command.Parameters.AddWithValue("@USERTYPE",user.TipoUsuario);
                command.Parameters.AddWithValue("@STATE",user.Estado);
                command.Parameters.AddWithValue("@CITY",user.Ciudad);
                command.Parameters.AddWithValue("@LOGIN",user.Login);
                command.Parameters.AddWithValue("@PASSWORD",user.Password);

                connection.Open();


                int code= command.ExecuteNonQuery();
                
                return code > -1; 

            }
      
        }
    }

}