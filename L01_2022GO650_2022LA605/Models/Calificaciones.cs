using System.ComponentModel.DataAnnotations;

namespace L01_2022GO650_2022LA605.Models
{
    public class Calificaciones
    {

        [Key]
        public int calificacionId { get; set; }

        public int publicacionId { get; set; }

        public int usuarioId { get; set; }

        public int calificacion { get; set; }
    }
}
