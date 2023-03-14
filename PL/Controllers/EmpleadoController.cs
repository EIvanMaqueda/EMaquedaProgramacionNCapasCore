using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpleadoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ViewBag.Sesion = HttpContext.Session.GetString("Usuario");
            ML.Empleado empleado = new ML.Empleado();
            ML.Result result = BL.Empleado.GetAll(empleado); //EF
            
            if (result.Correct)
            {
                empleado.Empleados = result.Objects;
                return View(empleado);
            }
            else
            {
                return View(empleado);
            }
        }
        [HttpGet]
        public ActionResult Form(int? idEmpleado)
        {
            ViewBag.Sesion = HttpContext.Session.GetString("Usuario");
            ML.Result resultEmpresa=BL.Empresa.EmpresaGetAll();
            ML.Empleado empleado = new ML.Empleado();
            empleado.Empresa = new ML.Empresa();
            if (resultEmpresa.Correct)//validacion para ver si paises y roles bienen llenos
            {
                empleado.Empresa.Empresas= resultEmpresa.Objects;
            }
            if (idEmpleado == null)//agregar
            {
                return View(empleado);//enviar usuario
            }
            else//actualizar
            {
                //ML.Result result = BL.Usuario.GetById(idUsuario.Value);
                ML.Result result = BL.Empleado.GetById(idEmpleado.Value);
                if (result.Correct)
                {
                    empleado = (ML.Empleado)result.Object;
                    empleado.Empresa.Empresas= resultEmpresa.Objects;
                    return View(empleado);
                }
                else
                {
                    ViewBag.Message = result.Message;
                    return View("Modal");
                }

            }
        }

        [HttpPost]

        public ActionResult Form(ML.Empleado empleado)
        {
            ViewBag.Sesion = HttpContext.Session.GetString("Usuario");
            //HttpPostedFileBase file = Request.Files["fuImage"];
            IFormFile file = Request.Form.Files["fuImage"];
            if (file != null)
            {
                byte[] imagen = ConvertToBytes(file);
                empleado.Foto = Convert.ToBase64String(imagen);
            }
            ML.Result result = new ML.Result();
            if (empleado.IdEmpleado == 0)
            {
                result = BL.Empleado.Add(empleado);
                ViewBag.Message = result.Message;
                return View("Modal");
            }
            else
            {
                result = BL.Empleado.Update(empleado);
                ViewBag.Message = result.Message;
                return View("Modal");
            }

        }

        [HttpGet]
        public ActionResult Delete(int? idEmpleado)
        {
            if (idEmpleado == null)
            {
                return View();
            }
            else
            {

                ML.Result result = BL.Empleado.Delete(idEmpleado.Value);

                if (result.Correct)
                {
                    ViewBag.Message = result.Message;
                    return View("Modal");
                }
                else
                {
                    ViewBag.Message = result.Message;
                    return View("Modal");
                }
            }
        }

        [HttpPost]
        public byte[] ConvertToBytes(IFormFile foto)
        {
            using var fileStream = foto.OpenReadStream();
            //byte[] data = null;
            //System.IO.BinaryReader reader = new System.IO.BinaryReader(foto.InputStream);
            //data = reader.ReadBytes((int)foto.Length);
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)bytes.Length);
            return bytes;

        }
    }
}
