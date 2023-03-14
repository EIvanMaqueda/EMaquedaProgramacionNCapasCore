using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        [Route("api/Usuario/GetAll")]
        public ActionResult GetAll()
        {
            ML.Usuario usuario= new ML.Usuario();
            ML.Result result = BL.Usuario.GetAll(usuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else { 
                return NotFound(result);
            }
            
        }

        [HttpPost]
        [Route("api/Usuario/Add")]
        public ActionResult Add([FromBody]ML.Usuario usuario)
        {
           
            ML.Result result = BL.Usuario.Add(usuario);
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
        [Route("api/Usuario/GetByid/{IdUsuario}")]
        public ActionResult GetById(int IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = BL.Usuario.GetById(IdUsuario);
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
        [Route("api/Usuario/Delete/{IdUsuario}")]
        public ActionResult Delete(int IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = BL.Usuario.Delete(IdUsuario);
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
        [Route("api/Usuario/Update")]
        public ActionResult Update([FromBody] ML.Usuario usuario) {
            
            ML.Result result = BL.Usuario.Update(usuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else { 
              return NotFound(result);
            }
        }

    }
}
