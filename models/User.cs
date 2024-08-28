using System.ComponentModel.DataAnnotations;
namespace nezter_backend
{

    public class User
    {

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }
        [Required]
        public string Direccion { get; set; }

        [Required]
        public string Telefono { get; set; }

        [Required]
        public string CodigoPostal { get; set; }

        [Required]
        public int TipoUsuario { get; set; }

        [Required]
        public int Estado { get; set; }

        [Required]
        public int Ciudad { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }


    }


}