using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Dependiente
    {
        public static ML.Result GetByIdEmpleado(int idEmpleado)
        {

            ML.Result result = new ML.Result();
            try
            {
                using (DL.EmaquedaProgramacionNcapasContext context = new DL.EmaquedaProgramacionNcapasContext())
                {
                    var query = context.Dependientes.FromSqlRaw($"DependienteGetByIdEmpleado {idEmpleado}");
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Dependiente dependiente = new ML.Dependiente();

                            dependiente.IdDependiente = obj.IdDependiente;
                            dependiente.Nombre = obj.Nombre;
                            dependiente.ApellidoPaterno = obj.ApellidoPaterno;
                            dependiente.ApellidoMaterno = obj.ApellidoMaterno;
                            dependiente.FechaNacimiento = obj.FechaNacimiento.Value.ToString("dd/MM/yyyy");
                            dependiente.EstadoCivil = obj.EstadoCivil;
                            dependiente.Telefono = obj.Telefono;
                            dependiente.Rfc = obj.Rfc;
                            dependiente.Genero = obj.Genero;

                            dependiente.Empleado = new ML.Empleado();
                            dependiente.Empleado.IdEmpleado = obj.IdEmpleado.Value;
                            dependiente.EmpleadoNombre = obj.EmpleadoNombre + " " + obj.EmpleadoApellidoMaterno + " " + obj.EmpleadoApellidoPaterno;
                            dependiente.Empleado.Nombre = obj.EmpleadoNombre;
                            dependiente.Empleado.ApellidoPaterno = obj.EmpleadoApellidoPaterno;
                            dependiente.Empleado.ApellidoMaterno = obj.EmpleadoApellidoMaterno;

                            dependiente.DependienteTipo = new ML.DependienteTipo();
                            dependiente.DependienteTipo.Nombre = obj.NombreDependiente;
                            result.Objects.Add(dependiente);

                        }
                    }
                    
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {

                result.Correct = false;
                result.Message = "Error: " + ex.Message;
            }
            return result;

        }

        public static ML.Result DependienteTipoGetAll() {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EmaquedaProgramacionNcapasContext context = new DL.EmaquedaProgramacionNcapasContext())
                {
                    var query = context.DependienteTipos.FromSqlRaw($"DependienteTipoGetAll").ToList();
                    if (query!=null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.DependienteTipo dependieteTipo = new ML.DependienteTipo();
                            dependieteTipo.IdDependienteTipo = obj.IdDependienteTipo;
                            dependieteTipo.Nombre = obj.Nombre;
                            result.Objects.Add(dependieteTipo);
                        }

                        result.Correct = true;

                    }
                }
            }
            catch (Exception ex)
            {

                result.Correct = false;
                result.Message = "Error: "+ex.Message;
            }
            return result;
        
        }

        public static ML.Result DependienteAdd(ML.Dependiente dependiente) { 

            ML.Result result = new ML.Result();
            try
            {
                using (DL.EmaquedaProgramacionNcapasContext context=new DL.EmaquedaProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"DependienteAdd {dependiente.Empleado.IdEmpleado }, '{dependiente.Nombre}', '{dependiente.ApellidoPaterno}', " +
                        $"'{dependiente.ApellidoMaterno}', '{dependiente.FechaNacimiento}', '{dependiente.EstadoCivil}', '{dependiente.Genero}', " +
                        $"'{dependiente.Telefono}', '{dependiente.Rfc}', {dependiente.DependienteTipo.IdDependienteTipo}");

                    if (query>=1)
                    {
                        result.Message = "Usuario agregado correctamente";
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error al agregar al usuario: "+ex.Message;
                result.Correct = false;
            }
            return result;
        
        }

        public static ML.Result GetByIdDependiente(int idDependiente) { 
            ML.Result result=new ML.Result();
            try
            {
                using (DL.EmaquedaProgramacionNcapasContext context = new DL.EmaquedaProgramacionNcapasContext())
                {
                    var query = context.Dependientes.FromSqlRaw($"DependienteGetByIdDependiente {idDependiente}").AsEnumerable().FirstOrDefault();
                    if (query!=null)
                    {
                        ML.Dependiente dependiente= new ML.Dependiente();

                        dependiente.IdDependiente = query.IdDependiente;
                        dependiente.Nombre = query.Nombre;
                        dependiente.ApellidoPaterno = query.ApellidoPaterno;
                        dependiente.ApellidoMaterno = query.ApellidoMaterno;
                        dependiente.FechaNacimiento = query.FechaNacimiento.Value.ToString("dd/MM/yyyy");
                        dependiente.EstadoCivil = query.EstadoCivil;
                        dependiente.Telefono = query.Telefono;
                        dependiente.Rfc = query.Rfc;
                        dependiente.Genero = query.Genero;

                        dependiente.Empleado = new ML.Empleado();
                        dependiente.Empleado.IdEmpleado = query.IdEmpleado.Value;
                        dependiente.Empleado.Nombre = query.EmpleadoNombre;
                        dependiente.Empleado.ApellidoPaterno = query.EmpleadoApellidoPaterno;
                        dependiente.Empleado.ApellidoMaterno = query.EmpleadoApellidoMaterno;

                        dependiente.DependienteTipo = new ML.DependienteTipo();
                        dependiente.DependienteTipo.IdDependienteTipo=query.IdDependienteTipo.Value;
                        dependiente.DependienteTipo.Nombre = query.NombreDependiente;
                        result.Object= dependiente;

                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {

               result.Correct=false;
                result.Message= "Error: "+ex.Message;
            }
            return result;
        
        }

        public static ML.Result DependienteUpdate(ML.Dependiente dependiente) {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EmaquedaProgramacionNcapasContext context=new DL.EmaquedaProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"DependienteUpdate {dependiente.Empleado.IdEmpleado }, '{dependiente.Nombre}', '{dependiente.ApellidoPaterno}', " +
                        $"'{dependiente.ApellidoMaterno}', '{dependiente.FechaNacimiento}', '{dependiente.EstadoCivil}', '{dependiente.Genero}', " +
                        $"'{dependiente.Telefono}', '{dependiente.Rfc}', {dependiente.DependienteTipo.IdDependienteTipo},{dependiente.IdDependiente}");
                    if (query>0)
                    {
                        result.Correct = true;
                        result.Message = "Usuario Actualizado Correctamente";
                    }
                }
            }
            catch (Exception ex)
            {

                result.Correct=false;
                result.Message= "Error: "+ex.Message;
            }
            return result;
        
        }

        public static ML.Result DependienteDelete(int idDependiente) { 
            ML.Result result= new ML.Result();
            try
            {
                using (DL.EmaquedaProgramacionNcapasContext context= new DL.EmaquedaProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"DependienteDelete {idDependiente}");
                    if (query>0)
                    {
                        result.Correct = true;
                        result.Message = "Usuario Eliminado con exito";
                    }
                }
                
            }
            catch (Exception ex)
            {

               result.Correct=false;
                result.Message = "Error al eliminar al usuario: "+ex.Message;
            }
            return result;
        
        }
    }
}
