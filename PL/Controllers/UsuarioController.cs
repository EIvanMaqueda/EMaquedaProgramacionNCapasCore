﻿using Microsoft.AspNetCore.Mvc;
using ML;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        //Inyeccion de dependencias-- patron de diseño
        private readonly IConfiguration _configuration;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public UsuarioController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        //[TempData]
        // public string Sesion { get; set; }

        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (var client = new HttpClient())
                {
                    string urlApi = _configuration["UrlApi"];
                    client.BaseAddress = new Uri(urlApi);

                    var responseTask = client.GetAsync("Usuario/GetAll");
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }
                    }
                    usuario.Usuarios = result.Objects;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return View(usuario);
            //ML.Result result = BL.Usuario.GetAll(usuario);
            //if (result.Correct)
            //{
            //    usuario.Usuarios = result.Objects;
            //    return View(usuario);
            //}
            //else
            //{
            //    return View(usuario);
            //}
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
                //ML.Result result = BL.Usuario.GetById(idUsuario.Value);
                ML.Result result=new ML.Result();
                try
                {
                    using (var client = new HttpClient())
                    {
                        string urlApi = _configuration["UrlApi"];
                        client.BaseAddress = new Uri(urlApi);

                        var responseTask = client.GetAsync("Usuario/GetByid/"+ idUsuario);
                        responseTask.Wait();

                        var resultServicio = responseTask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();
                            ML.Usuario resultItemList = new ML.Usuario();
                            resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.Object.ToString());
                            result.Object = resultItemList;
                            result.Correct = true;

                        }
                        else { 
                        result.Correct=false;
                        }
                        
                    }
                }
                catch (Exception ex)
                {

                    result.Correct = false;
                    result.Message= ex.Message;
                }
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

            //ML.Result result = new ML.Result();
           // if (ModelState.IsValid == true)
            //{
                if (usuario.IdUsuario == 0)
                {
                //result = BL.Usuario.Add(usuario);
                //ViewBag.Message = result.Message;
                //return View("Modal");

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["UrlApi"]);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Usuario>("Usuario/Add", usuario);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Usuario Agregado correctamente";
                        return PartialView("Modal");
                    }
                    else{
                        ViewBag.Message = "Error al agregar al usuario";
                        return PartialView("Modal");
                    }
                }

               
            }
                else
                {
                //ML.Result result1= new ML.Result();
                //    result1 = BL.Usuario.Update(usuario);
                //    ViewBag.Message = result1.Message;
                //    return View("Modal");
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["UrlApi"]);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Usuario>("Usuario/Update", usuario);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Usuario actualizado correctamente";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "Error al actualizar al usuario";
                        return PartialView("Modal");
                    }
                }
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

                //ML.Result result = BL.Usuario.Delete(idUsuario.Value);

                //if (result.Correct)
                //{
                //    ViewBag.Message = result.Message;
                //    return View("Modal");
                //}
                //else
                //{
                //    ViewBag.Message = result.Message;
                //    return View("Modal");
                //}
                //ML.Result result=new ML.Result();
                try
                {
                    using (var client = new HttpClient())
                    {
                        string urlApi = _configuration["UrlApi"];
                        client.BaseAddress = new Uri(urlApi);

                        var responseTask = client.GetAsync("Usuario/Delete/" + idUsuario);
                        responseTask.Wait();
                        var result = responseTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            ViewBag.Message = "Usuario borrado exitosamente";
                        }
                        else {
                            ViewBag.Message = "Error al eliminar al usuario";
                        }

                    }

                    
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    
                }
                return PartialView("Modal");
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
                    
                   // Sesion = HttpContext.Session.GetString("Usuario");

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
