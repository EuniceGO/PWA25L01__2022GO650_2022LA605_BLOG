using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using L01_2022GO650_2022LA605.Models;
using Microsoft.Data.SqlClient;

namespace L01_2022GO650_2022LA605.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly BlogContext _blogContexto;

        public UsuarioController(BlogContext usuarioContexto)
        {
            _blogContexto = usuarioContexto;
        }

        /// <summary>
        /// EndPoint que retorna el listado de todos los usuarios existentes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
      
        public IActionResult Get()
        {
            List<Usuarios> listadoUsuarios = (from e in _blogContexto.usuarios
                                          select e).ToList();

            if (listadoUsuarios.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoUsuarios);
        }



    }
}
