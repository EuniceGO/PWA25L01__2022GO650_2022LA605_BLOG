using System.ComponentModel.DataAnnotations;

namespace L01_2022GO650_2022LA605.Models
{
    public class Comentarioscs
    {

        [Key]
        public int comentarioId { get; set; }
        public int publicacionId { get; set; }
        public string comentario { get; set; }
        public int usuarioId { get; set; }
    }
}

