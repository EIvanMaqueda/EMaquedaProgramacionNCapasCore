using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {

        [TempData]
        public string Sesion { get; set; }

        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = BL.Usuario.GetAll(usuario);
            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
                return View(usuario);
            }
            else
            {
                return View(usuario);
            }
        }

        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {
            
            ML.Result result = BL.Usuario.GetAll(usuario); //EF
            
            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
                return View(usuario);
            }
            else
            {
                return View(usuario);
            }
        }

        [HttpGet]
        public ActionResult Form(int? idUsuario)
        {
            ML.Result resultrol = BL.Rol.GetAllRol();
            ML.Result resultpais = BL.Pais.GetAllPais();
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
            if (resultrol.Correct && resultpais.Correct)//validacion para ver si paises y roles bienen llenos
            {

                usuario.Rol.Roles = resultrol.Objects;
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultpais.Objects;
            }
            if (idUsuario == null)//agregar
            {
                return View(usuario);//enviar usuario
            }
            else//actualizar
            {
                ML.Result result = BL.Usuario.GetById(idUsuario.Value);
                if (result.Correct)
                {
                    usuario = (ML.Usuario)result.Object;
                    usuario.Rol.Roles = resultrol.Objects;
                    usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultpais.Objects;
                    ML.Result resultestado = BL.Estado.EstadoGetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
                    ML.Result resultMunicipio = BL.Municipio.MunicipioGetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                    ML.Result resultColonia = BL.Colonia.ColoniaGetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                    usuario.Direccion.Colonia.Municipio.Estado.Estados = resultestado.Objects;
                    usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                    usuario.Direccion.Colonia.Colonias = resultColonia.Objects;
                    return View(usuario);
                }
                else
                {
                    ViewBag.Message = result.Message;
                    return View("Modal");
                }
              
            }
        }

        [HttpPost]

        public ActionResult Form(ML.Usuario usuario)
        {
           
            //HttpPostedFileBase file = Request.Files["fuImage"];
            IFormFile file = Request.Form.Files["fuImage"];
            if (file != null)
            {
                byte[] imagen = ConvertToBytes(file);
                usuario.Imagen = Convert.ToBase64String(imagen);
            }

            ML.Result result = new ML.Result();
           // if (ModelState.IsValid == true)
            //{
                if (usuario.IdUsuario == 0)
                {
                    result = BL.Usuario.Add(usuario);
                    ViewBag.Message = result.Message;
                    return View("Modal");
                }
                else
                {
                    result = BL.Usuario.Update(usuario);
                    ViewBag.Message = result.Message;
                    return View("Modal");
                }
            //}
            //else {
            //    ML.Result resultrol = BL.Rol.GetAllRol();
            //    ML.Result resultpais = BL.Pais.GetAllPais();
            //    usuario.Rol = new ML.Rol();
            //    usuario.Direccion = new ML.Direccion();
            //    usuario.Direccion.Colonia = new ML.Colonia();
            //    usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            //    usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            //    usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
            //    usuario.Rol.Roles = resultrol.Objects;
            //    usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultpais.Objects;
            //    return View(usuario);
            //}

        }

        [HttpGet]
        public ActionResult Delete(int? idUsuario)
        {
            if (idUsuario == null)
            {
                return View();
            }
            else
            {
                
                ML.Result result = BL.Usuario.Delete(idUsuario.Value);

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
        public JsonResult EstadoGetByIdPais(int idPais)
        {
            ML.Result result = BL.Estado.EstadoGetByIdPais(idPais);
            return Json(result.Objects);
        }

        [HttpPost]
        public JsonResult MunicipioGetByIdEstado(int idEstado)
        {
            ML.Result result = BL.Municipio.MunicipioGetByIdEstado(idEstado);
            return Json(result.Objects);
        }


        [HttpPost]
        public JsonResult ColoniaGetByIdMunicipio(int idMunicipio)
        {
            ML.Result result = BL.Colonia.ColoniaGetByIdMunicipio(idMunicipio);
            return Json(result.Objects);
        }

        [HttpPost]
        public byte[] ConvertToBytes(IFormFile foto)
        {
            using var fileStream=foto.OpenReadStream();
            //byte[] data = null;
            //System.IO.BinaryReader reader = new System.IO.BinaryReader(foto.InputStream);
            //data = reader.ReadBytes((int)foto.Length);
            byte[] bytes= new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)bytes.Length);
            return bytes;

        }

        [HttpPost]
        public JsonResult CambiarStatus(int idUsuario,bool status)
        {
            ML.Result result = BL.Usuario.UpdateStatus(idUsuario,status);
            return Json(result.Objects);
        }


        [HttpGet]
        public ActionResult Login() {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {

            ML.Result result = BL.Usuario.GetByUsername(username);

            if (result.Correct)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;
                
                if (usuario.Password != password || password==null)
                {
                    ViewBag.Message = "Contraseña Incoirrecta";
                    return PartialView("ModalLogin");
                }
                else
                {
                   
                    HttpContext.Session.SetString("Usuario",usuario.Rol.IdRol.ToString());
                    
                    Sesion = HttpContext.Session.GetString("Usuario");

                    return RedirectToAction("Index", "Home");
                }
            }
            else {
                ViewBag.Message = "Usuario no encontrado";
                return PartialView("ModalLogin");
            }
        }
    }
}
