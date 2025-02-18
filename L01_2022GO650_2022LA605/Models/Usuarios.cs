using System.ComponentModel.DataAnnotations;

namespace L01_2022GO650_2022LA605.Models
{
    public class Usuarios
    {
        [Key]  // Agregar esta anotación
        public int UsuarioId { get; set; }
        public int RolId { get; set; }
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }
}
