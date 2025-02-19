using System.ComponentModel.DataAnnotations;
using L01_2022GO650_2022LA605.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2022GO650_2022LA605.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentariosController : ControllerBase
    {

        private readonly BlogContext _blogContexto;

        public ComentariosController(BlogContext comentarioContexto)
        {
            _blogContexto = comentarioContexto;
        }

        /// <summary>
        /// EndPoint que retorna el listado de todos los usuarios existentes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<Comentarioscs> listadoUsuarios = (from e in _blogContexto.comentarios
                                              select e).ToList();

            if (listadoUsuarios.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoUsuarios);
        }


        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarUsuario([FromBody] Comentarioscs comentarios)
        {
            try
            {
                _blogContexto.comentarios.Add(comentarios);
                _blogContexto.SaveChanges();
                return Ok(comentarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }


        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarEquipo(int id, [FromBody] Comentarioscs Comentarios)
        {
            //Para actualizar un registro, se obtiene el registro original de la base de datos
            //al cual alteraremos alguna propiedad
            Comentarioscs? equipoActual = (from e in _blogContexto.comentarios
                                      where e.cometarioId == id
                                      select e).FirstOrDefault();

            //Verificamos que exista el registro segun su ID
            if (equipoActual == null)
            {
                return NotFound();
            }

            //Si se encuentra el registro, se alteran los campos modificables
            equipoActual.publicacionId = Comentarios.publicacionId;
            equipoActual.comentario = Comentarios.comentario;
            equipoActual.usuarioId = Comentarios.usuarioId;
          





            //Se marca el registro como modificado en el contexto
            //y se envia la modificación a la base de datos
            _blogContexto.Entry(equipoActual).State = EntityState.Modified;
            _blogContexto.SaveChanges();

            return Ok(Comentarios);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarEquipo(int id)
        {
            //Para actualizar un registro, se obtiene el registro original de la base de datos
            //al cual eliminaremos
            Comentarioscs? comentario = (from e in _blogContexto.comentarios
                                where e.cometarioId == id
                                select e).FirstOrDefault();

            //Verificamos que exista el registro segun su ID
            if (comentario == null)
                return NotFound();

            //Ejecutamos la acción de eliminar el registro
            _blogContexto.comentarios.Attach(comentario);
            _blogContexto.comentarios.Remove(comentario);
            _blogContexto.SaveChanges();

            return Ok(comentario);
        }

        [HttpGet]
        [Route("filtrarPorUsuario/{usuarioId}")]
        public IActionResult FiltrarComentariosPorUsuario(int usuarioId)
        {
            var comentariosFiltrados = _blogContexto.comentarios
                .Where(c => c.usuarioId == usuarioId)
                .ToList();

            if (!comentariosFiltrados.Any())
            {
                return NotFound("No se encontraron comentarios para este usuario.");
            }

            return Ok(comentariosFiltrados);
        }




    }
}
