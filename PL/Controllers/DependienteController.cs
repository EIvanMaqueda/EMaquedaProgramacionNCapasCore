using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class DependienteController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ViewBag.Sesion = HttpContext.Session.GetString("Usuario");
            ML.Empleado empleado = new ML.Empleado();
            ML.Result resultEmpresa = BL.Empresa.EmpresaGetAll();
            ML.Result result = BL.Empleado.GetAll(empleado); //EF
            
            empleado.Empresa = new ML.Empresa();
            if (resultEmpresa.Correct)//validacion para ver si paises y roles bienen llenos
            {
                empleado.Empresa.Empresas = resultEmpresa.Objects;
            }
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

        [HttpPost]
        public ActionResult GetAll(ML.Empleado empleado)
        {
            ViewBag.Sesion = HttpContext.Session.GetString("Usuario");
            ML.Result resultEmpresa = BL.Empresa.EmpresaGetAll();
            empleado.IdEmpresa = empleado.Empresa.IdEmpresa;
            ML.Result result = BL.Empleado.GetAll(empleado); //EF
            
            empleado.Empresa = new ML.Empresa();
            if (resultEmpresa.Correct)//validacion para ver si paises y roles bienen llenos
            {
                empleado.Empresa.Empresas = resultEmpresa.Objects;
            }
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
        public ActionResult DependienteGetByIdEmpleado(int idEmpleado)
        {

            ViewBag.Sesion = HttpContext.Session.GetString("Usuario");
            ML.Result result = BL.Dependiente.GetByIdEmpleado(idEmpleado);
            ML.Dependiente dependiente = new ML.Dependiente();
            //dependiente.Empleado = new ML.Empleado();
            
            if (result.Correct)
            {
                if (result.Objects.Count != 0)
                {
                    dependiente = (ML.Dependiente)result.Objects[0];
                }
                else {
                    dependiente.Empleado = new ML.Empleado();
                    result = BL.Empleado.GetById(idEmpleado);
                    ML.Empleado empleado= new ML.Empleado();
                    empleado = (ML.Empleado)result.Object;
                    dependiente.Empleado.IdEmpleado = empleado.IdEmpleado;
                    dependiente.Empleado.Nombre= empleado.Nombre;
                    dependiente.Empleado.ApellidoPaterno=empleado.ApellidoPaterno;
                    dependiente.Empleado.ApellidoMaterno = empleado.ApellidoMaterno;
                }
                
                dependiente.Dependientes = result.Objects;
                
                return View(dependiente);
            }
            else
            {
                return View(dependiente);
            }
        }

        [HttpGet]
        public ActionResult Form(int? idDependiente, int idEmpleado)
        {
            ViewBag.Sesion = HttpContext.Session.GetString("Usuario");
            ML.Result resultdependientetipo = BL.Dependiente.DependienteTipoGetAll();
            ML.Dependiente dependiente=new ML.Dependiente();
            dependiente.DependienteTipo=new ML.DependienteTipo();
            dependiente.Empleado = new ML.Empleado();
            ML.Result result=new ML.Result();
            dependiente.Empleado.IdEmpleado = idEmpleado;
            if (resultdependientetipo.Correct)//validacion para ver si paises y roles bienen llenos
            {
                dependiente.DependienteTipo.DependientesTipos = resultdependientetipo.Objects;
            }
            if (idDependiente == null)//agregar
            {
                //dependiente.Empleado=new ML.Empleado();
                //dependiente.Empleado.IdEmpleado = idEmpleado;
                result=BL.Empleado.GetById(idEmpleado);
                ML.Empleado empleado = (ML.Empleado)result.Object;
                dependiente.Empleado.Nombre= empleado.Nombre;
                dependiente.Empleado.ApellidoPaterno=empleado.ApellidoPaterno;
                dependiente.Empleado.ApellidoMaterno=empleado.ApellidoMaterno;
                return View(dependiente);//enviar usuario
            }
            else//actualizar
            {
                //dependiente.Empleado= new ML.Empleado();
                //dependiente.Empleado.IdEmpleado = idEmpleado;
                result = BL.Dependiente.GetByIdDependiente(idDependiente.Value);
                dependiente =(ML.Dependiente)result.Object;
                dependiente.DependienteTipo.DependientesTipos = resultdependientetipo.Objects;
                return View(dependiente);

            }
        }
        
        [HttpPost]
        public ActionResult Form(ML.Dependiente dependiente)
        {
            ViewBag.Sesion = HttpContext.Session.GetString("Usuario");

            if (dependiente.IdDependiente == 0)
            {
                ML.Result result = BL.Dependiente.DependienteAdd(dependiente);
                ViewBag.Message = result.Message;
                ViewBag.Location = "DependienteGetByIdEmpleado?idEmpleado=" + dependiente.Empleado.IdEmpleado;
                return View("Modal1");

                
            }
            else {
                ML.Result result = BL.Dependiente.DependienteUpdate(dependiente);
                ViewBag.Message = result.Message;
                ViewBag.Location = "DependienteGetByIdEmpleado?idEmpleado="+dependiente.Empleado.IdEmpleado;
                return View("Modal1");
            }
        }

        [HttpGet]
        public ActionResult Delete(int idDependiente) {
            ML.Result result=BL.Dependiente.DependienteDelete(idDependiente);
            ViewBag.Message = result.Message;
            return View("Modal");
        }
    }
}
