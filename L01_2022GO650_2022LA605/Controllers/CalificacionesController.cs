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

        [HttpGet]
        [Route("GetAllCalificaciones")]

        public IActionResult Get()
        {
            List<Calificaciones> lsitadoCalificaciones = (from e in _blogcontext.calificaciones
                                              select e).ToList();

            if (lsitadoCalificaciones.Count == 0)
            {
                return NotFound();
            }

            return Ok(lsitadoCalificaciones);
        }


        [HttpPost]
        [Route("AddCalificacion")]
        public IActionResult AgrearCalificacion([FromBody] Calificaciones calificacion)
        {
            try
            {
               _blogcontext.calificaciones.Add(calificacion);
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
            Calificaciones? calificacionActualicar = (from c in _blogcontext.calificaciones
                                       where c.calificacionId == id
                                       select c).FirstOrDefault();

            if (calificacionActualicar == null)
            { return NotFound(); }

            
            calificacionActualicar.calificacion = modificarcalificacion.calificacion;

            _blogcontext.Entry(calificacionActualicar).State = EntityState.Modified;
            _blogcontext.SaveChanges();
            return Ok(modificarcalificacion);
        }

        [HttpDelete]
        [Route("DeleteCalificacion")]
        public IActionResult EliminarCalificacion(int id)
        {
            Calificaciones? calificacion = (from c in _blogcontext.calificaciones
                            where c.calificacionId == id
                            select c).FirstOrDefault();

            if (calificacion == null)
                return NotFound();

            _blogcontext.calificaciones.Attach(calificacion);
            _blogcontext.calificaciones.Remove(calificacion);
            _blogcontext.SaveChanges();
            return Ok(calificacion);
        }
        
        
        [HttpGet]
        [Route("ObtenerPublicacion")]
        public IActionResult ObtenerCalificacionPublicacion(int publicacion_id)
        {
            Calificaciones? calificacion = (from c in _blogcontext.calificaciones
                                            where c.publicacionId == publicacion_id
                                            select c).FirstOrDefault();

            if (calificacion == null)
                return NotFound();

            _blogcontext.calificaciones.Attach(calificacion);
            _blogcontext.calificaciones.Remove(calificacion);
            _blogcontext.SaveChanges();
            return Ok(calificacion);
        }



    }
}
