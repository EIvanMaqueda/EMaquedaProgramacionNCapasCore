using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class AseguradoraController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ViewBag.Sesion = HttpContext.Session.GetString("Usuario");
            ML.Result result = BL.Aseguradora.GetAllAseguradora();
            ML.Aseguradora aseguradora = new ML.Aseguradora();
            if (result.Correct)
            {
                aseguradora.Aseguradoras = result.Objects;
                return View(aseguradora);
            }
            else
            {
                return View("Modal");
            }

        }
        [HttpGet]
        public ActionResult Form(int? idAseguradora)
        {
            ViewBag.Sesion = HttpContext.Session.GetString("Usuario");
            ML.Usuario usuario= new ML.Usuario();
            ML.Result resultUsuarios = BL.Usuario.GetAll(usuario);
            ML.Aseguradora aseguradora = new ML.Aseguradora();
            aseguradora.Usuario = new ML.Usuario();
            if (resultUsuarios.Correct)
            {
                aseguradora.Usuario.Usuarios = resultUsuarios.Objects;
            }
            if (idAseguradora == null)
            {
                return View(aseguradora);
            }
            else
            {
                ML.Result result = BL.Aseguradora.GetByIdAseguradora(idAseguradora.Value);
                if (result.Correct)
                {
                    aseguradora = (ML.Aseguradora)result.Object;
                    aseguradora.Usuario.Usuarios = resultUsuarios.Objects;
                    return View(aseguradora);
                }
                else
                {
                    return View("Modal");
                }
            }

        }


        [HttpPost]
        public ActionResult Form(ML.Aseguradora aseguradora)//agregar metodoef
        {
            ViewBag.Sesion = HttpContext.Session.GetString("Usuario");
            ML.Result result = new ML.Result();

            if (aseguradora.IdAseguradora == 0)
            {
                result = BL.Aseguradora.AddAseguradora(aseguradora);
                ViewBag.Message = result.Message;
                return View("Modal");
            }
            else
            {
                result = BL.Aseguradora.UpdateAseguradora(aseguradora);
                ViewBag.Message = result.Message;
                return View("Modal");
            }

        }

        [HttpGet]
        public ActionResult Delete(int idAseguradora)
        {
            ML.Result result = new ML.Result();
            result = BL.Aseguradora.DeleteAseguradora(idAseguradora);
            ViewBag.Message = result.Message;
            return View("Modal");
        }
    }
}
