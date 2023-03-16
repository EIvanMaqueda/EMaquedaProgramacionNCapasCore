using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpleadoController : Controller
    {
        //Inyeccion de dependencias-- patron de diseño
        private readonly IConfiguration _configuration;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public EmpleadoController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public ActionResult GetAll()
        {

            ML.Empleado empleado = new ML.Empleado();
            empleado.Empresa = new ML.Empresa();
            //ML.Result result = BL.Empleado.GetAll(empleado); //EF

            //if (result.Correct)
            //{
            //    empleado.Empleados = result.Objects;
            //    return View(empleado);
            //}
            //else
            //{
            //    return View(empleado);
            //}
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (var client = new HttpClient())
                {
                    string urlApi = _configuration["UrlApi"];
                    client.BaseAddress = new Uri(urlApi);

                    var responseTask = client.GetAsync("Empleado/GetAll");
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Empleado resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Empleado>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }
                    }
                    empleado.Empleados = result.Objects;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return View(empleado);
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
                //ML.Result result = BL.Empleado.GetById(idEmpleado.Value);
                ML.Result result = new ML.Result();
                try
                {
                    using (var client = new HttpClient())
                    {
                        string urlApi = _configuration["UrlApi"];
                        client.BaseAddress = new Uri(urlApi);

                        var responseTask = client.GetAsync("Empleado/GetByid/" + idEmpleado);
                        responseTask.Wait();

                        var resultServicio = responseTask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();
                            ML.Empleado resultItemList = new ML.Empleado();
                            resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Empleado>(readTask.Result.Object.ToString());
                            result.Object = resultItemList;
                            result.Correct = true;

                        }
                        else
                        {
                            result.Correct = false;
                        }

                    }
                }
                catch (Exception ex)
                {

                    result.Correct = false;
                    result.Message = ex.Message;
                }
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
         
            //HttpPostedFileBase file = Request.Files["fuImage"];
            IFormFile file = Request.Form.Files["fuImage"];
            if (file != null)
            {
                byte[] imagen = ConvertToBytes(file);
                empleado.Foto = Convert.ToBase64String(imagen);
            }
            //ML.Result result = new ML.Result();
            if (empleado.IdEmpleado == 0)
            {
                //result = BL.Empleado.Add(empleado);
                //ViewBag.Message = result.Message;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["UrlApi"]);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Empleado>("Empleado/Add", empleado);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Empleado Agregado correctamente";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "Error al agregar el empleado";
                        return PartialView("Modal");
                    }
                }
                
            }
            else
            {
                //result = BL.Empleado.Update(empleado);
                //ViewBag.Message = result.Message;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["UrlApi"]);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Empleado>("Empleado/Update", empleado);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message ="Empleado actualizado correctamente";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "Error al actualizar el empleado";
                        return PartialView("Modal");
                    }
                }
              
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

                //ML.Result result = BL.Empleado.Delete(idEmpleado.Value);
                using (var client = new HttpClient())
                {
                    string urlApi = _configuration["UrlApi"];
                    client.BaseAddress = new Uri(urlApi);

                    var responseTask = client.GetAsync("Empleado/Delete/" + idEmpleado);
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Empleado borrado exitosamente";
                    }
                    else
                    {
                        ViewBag.Message = "Error al eliminar al empleado";
                    }

                }

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
                return View("Modal");
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
