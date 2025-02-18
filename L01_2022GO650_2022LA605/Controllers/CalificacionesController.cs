using System.ComponentModel.DataAnnotations;
using L01_2022GO650_2022LA605.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2022GO650_2022LA605.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class calificacionesController : ControllerBase
    {
        private readonly BlogContext _blogcontext;

        public calificacionesController(BlogContext blogcontext)
        {
            _blogcontext = blogcontext;
        }

        [HttpPost]
        [Route("AddCalificacion")]
        public IActionResult AgrearCalificacion([FromBody] Calificaciones calificacion)
        {
            try
            {
               _blogcontext.Calificaciones.Add(calificacion);
                _blogcontext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateCalificacion")]
        public IActionResult ActualizarCalificacion(int id, [FromBody] Calificaciones modificarcalificacion)
        {
            Calificaciones? calificacionActualicar = (from c in _blogcontext.Calificaciones
                                       where c.calificacionId == id
                                       select c).FirstOrDefault();

            if (calificacionActualicar == null)
            { return NotFound(); }

            calificacionActualicar.publicacionId = modificarcalificacion.publicacionId;
            calificacionActualicar.usuarioId = modificarcalificacion.usuarioId;
            calificacionActualicar.calificacion = modificarcalificacion.calificacion;

            _blogcontext.Entry(calificacionActualicar).State = EntityState.Modified;
            _blogcontext.SaveChanges();
            return Ok(modificarcalificacion);
        }

        [HttpDelete]
        [Route("DeleteCalificacion")]
        public IActionResult EliminarCalificacion(int id)
        {
            Calificaciones? calificacion = (from c in _blogcontext.Calificaciones
                            where c.calificacionId == id
                            select c).FirstOrDefault();

            if (calificacion == null)
                return NotFound();

            _blogcontext.Calificaciones.Attach(calificacion);
            _blogcontext.Calificaciones.Remove(calificacion);
            _blogcontext.SaveChanges();
            return Ok(calificacion);
        }



    }
}
