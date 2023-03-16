using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class AseguradoraController : Controller
    {
        //Inyeccion de dependencias-- patron de diseño
        private readonly IConfiguration _configuration;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public AseguradoraController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        //[TempData]
        [HttpGet]
        public ActionResult GetAll()
        {

            //ML.Result result = BL.Aseguradora.GetAllAseguradora();
            ML.Aseguradora aseguradora = new ML.Aseguradora();
            //if (result.Correct)
            //{
            //    aseguradora.Aseguradoras = result.Objects;
            //    return View(aseguradora);
            //}
            //else
            //{
            //    return View("Modal");
            //}
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (var client = new HttpClient())
                {
                    string urlApi = _configuration["UrlApi"];
                    client.BaseAddress = new Uri(urlApi);

                    var responseTask = client.GetAsync("Aseguradora/GetAll");
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Aseguradora resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Aseguradora>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }
                    }
                    aseguradora.Aseguradoras = result.Objects;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return View(aseguradora);

        }
        [HttpGet]
        public ActionResult Form(int? idAseguradora)
        {
            
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
                //ML.Result result = BL.Aseguradora.GetByIdAseguradora(idAseguradora.Value);
                ML.Result result = new ML.Result();
                try
                {
                    using (var client = new HttpClient())
                    {
                        string urlApi = _configuration["UrlApi"];
                        client.BaseAddress = new Uri(urlApi);

                        var responseTask = client.GetAsync("Aseguradora/GetByid/" + idAseguradora);
                        responseTask.Wait();

                        var resultServicio = responseTask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();
                            ML.Aseguradora resultItemList = new ML.Aseguradora();
                            resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Aseguradora>(readTask.Result.Object.ToString());
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
           
            //ML.Result result = new ML.Result();

            if (aseguradora.IdAseguradora == 0)
            {
                //result = BL.Aseguradora.AddAseguradora(aseguradora);
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["UrlApi"]);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Aseguradora>("Aseguradora/Add", aseguradora);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Aseguradora Agregado correctamente";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "Error al agregar la a aseguradora";
                        return PartialView("Modal");
                    }
                }
                
               
            }
            else
            {
                //result = BL.Aseguradora.UpdateAseguradora(aseguradora);
                //ViewBag.Message = result.Message;
                //return View("Modal");
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["UrlApi"]);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Aseguradora>("Aseguradora/Update", aseguradora);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "aseguradora actualizada correctamente";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "Error al actualizar la aseguradora";
                        return PartialView("Modal");
                    }
                }
            }

        }

        [HttpGet]
        public ActionResult Delete(int idAseguradora)
        {
            //ML.Result result = new ML.Result();
            //result = BL.Aseguradora.DeleteAseguradora(idAseguradora);
            //ViewBag.Message = result.Message;
            try
            {
                using (var client = new HttpClient())
                {
                    string urlApi = _configuration["UrlApi"];
                    client.BaseAddress = new Uri(urlApi);

                    var responseTask = client.GetAsync("Aseguradora/Delete/" + idAseguradora);
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Aseguradora borrado exitosamente";
                    }
                    else
                    {
                        ViewBag.Message = "Error al eliminar al aseguradora";
                    }

                }


            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;

            }
            return View("Modal");

        }
    }
}
