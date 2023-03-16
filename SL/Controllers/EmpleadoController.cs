using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    public class EmpleadoController : Controller
    {
        [HttpGet]
        [Route("api/Empleado/GetAll")]
        public ActionResult GetAll()
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.Empresa = new ML.Empresa();
            ML.Result result = BL.Empleado.GetAll(empleado);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }

        }

        [HttpPost]
        [Route("api/Empleado/Add")]
        public ActionResult Add([FromBody] ML.Empleado empleado)
        {

            ML.Result result = BL.Empleado.Add(empleado);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }

        }


        [HttpGet]
        [Route("api/Empleado/GetByid/{IdEmpleado}")]
        public ActionResult GetById(int IdEmpleado)
        {
            //ML.Usuario usuario = new ML.Usuario();
            ML.Result result = BL.Empleado.GetById(IdEmpleado);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }

        }

        [HttpGet]
        [Route("api/Empleado/Delete/{IdEmpleado}")]
        public ActionResult Delete(int IdEmpleado)
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = BL.Empleado.Delete(IdEmpleado);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }

        }

        [HttpPost]
        [Route("api/Empleado/Update")]
        public ActionResult Update([FromBody] ML.Empleado empleado)
        {

            ML.Result result = BL.Empleado.Update(empleado);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
    }
}
