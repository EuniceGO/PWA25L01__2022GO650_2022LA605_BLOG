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


        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarUsuario([FromBody] Usuarios usuario)
        {
            try
            {
                _blogContexto.usuarios.Add(usuario);
                _blogContexto.SaveChanges();
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }


        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarEquipo(int id, [FromBody] Usuarios autorModificar)
        {
            //Para actualizar un registro, se obtiene el registro original de la base de datos
            //al cual alteraremos alguna propiedad
            Usuarios? equipoActual = (from e in _blogContexto.usuarios
                                   where e.UsuarioId == id
                                   select e).FirstOrDefault();

            //Verificamos que exista el registro segun su ID
            if (equipoActual == null)
            {
                return NotFound();
            }

            //Si se encuentra el registro, se alteran los campos modificables
            equipoActual.RolId = autorModificar.RolId;
            equipoActual.NombreUsuario = autorModificar.NombreUsuario;
            equipoActual.Clave = autorModificar.Clave;
            equipoActual.Nombre = autorModificar.Nombre;
            equipoActual.Apellido = autorModificar.Apellido;





            //Se marca el registro como modificado en el contexto
            //y se envia la modificación a la base de datos
            _blogContexto.Entry(equipoActual).State = EntityState.Modified;
            _blogContexto.SaveChanges();

            return Ok(autorModificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarEquipo(int id)
        {
            //Para actualizar un registro, se obtiene el registro original de la base de datos
            //al cual eliminaremos
            Usuarios? equipo = (from e in _blogContexto.usuarios
                             where e.UsuarioId == id
                             select e).FirstOrDefault();

            //Verificamos que exista el registro segun su ID
            if (equipo == null)
                return NotFound();

            //Ejecutamos la acción de eliminar el registro
            _blogContexto.usuarios.Attach(equipo);
            _blogContexto.usuarios.Remove(equipo);
            _blogContexto.SaveChanges();

            return Ok(equipo);
        }

        [HttpGet]
        [Route("filtrarPorNombreApellido")]
        public IActionResult FiltrarPorNombreApellido(string nombre, string apellido)
        {
            var usuariosFiltrados = _blogContexto.usuarios
                .Where(u => u.Nombre.Contains(nombre) && u.Apellido.Contains(apellido))
                .ToList();

            if (!usuariosFiltrados.Any())
            {
                return NotFound("No se encontraron usuarios con ese nombre y apellido.");
            }

            return Ok(usuariosFiltrados);
        }

        [HttpGet]
        [Route("CantidadComentarios")]
        public IActionResult CantidadComentarios(int top)
        {
            var CantidadComentarios = (from u in _blogContexto.usuarios
                                       join cc in _blogContexto.comentarios
                                       on u.UsuarioId equals cc.usuarioId
                                       group cc by new {u.UsuarioId, u.NombreUsuario} into cantidad
                                       orderby cantidad.Count() descending
                                       select new
                                       {
                                           Nombre = cantidad.Key.NombreUsuario,
                                           CantidadComentarios = cantidad.Count()
                                       }).Take(top).ToList();

            return Ok(CantidadComentarios);
        }
    }



}


   
