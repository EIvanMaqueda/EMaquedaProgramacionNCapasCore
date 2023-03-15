using Microsoft.EntityFrameworkCore;
using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empleado
    {
        public static ML.Result GetAll(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EmaquedaProgramacionNcapasContext context=new DL.EmaquedaProgramacionNcapasContext())
                {
                    //empleado.Empresa = new ML.Empresa();
                    
                    var query = context.Empleados.FromSqlRaw($"EmpleadoGetAll '{empleado.Empresa.Nombre}','{empleado.Nombre}'").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            empleado = new ML.Empleado();
                            empleado.IdEmpleado = obj.IdEmpleado;
                            empleado.NumeroEmpleado = obj.NumeroEmpleado;
                            empleado.Nombre = obj.Nombre;
                            empleado.ApellidoMaterno = obj.ApellidoMaterno;
                            empleado.ApellidoPaterno = obj.ApellidoPaterno;
                            empleado.Foto = obj.Foto;
                            empleado.NSS = obj.Nss;
                            empleado.RFC = obj.Rfc;
                            empleado.Email = obj.Email;
                            empleado.Telefono = obj.Telefono;
                            empleado.FechaNacimiento = obj.FechaNacimiento.ToString("dd/MM/yyyy");
                            empleado.FechaIngreso = obj.FechaIngreso.ToString("dd/MM/yyyy");

                            empleado.Empresa = new ML.Empresa();
                            empleado.Empresa.Nombre = obj.EmpresaNombre;
                            empleado.Empresa.IdEmpresa = obj.IdEmpresa.Value;
                            empleado.Empresa.Telefono = obj.EmpresaTelefono;
                            empleado.Empresa.Email = obj.EmpresaMail;
                            empleado.Empresa.DireccionWeb = obj.DireccionWeb;
                            empleado.Empresa.Logo = obj.Logo;
                            result.Objects.Add(empleado);

                            
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

        public static ML.Result Add(ML.Empleado empleado) { 
        
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EmaquedaProgramacionNcapasContext context=new DL.EmaquedaProgramacionNcapasContext())
                {
                  
                    var query = context.Database.ExecuteSqlRaw($"EmpleadoAdd '{empleado.NumeroEmpleado}','{empleado.RFC}','{empleado.Nombre}'," +
                        $"'{empleado.ApellidoPaterno}','{empleado.ApellidoMaterno}','{empleado.Email}','{empleado.Telefono}','{empleado.FechaNacimiento}'," +
                        $"'{empleado.NSS}','{empleado.FechaIngreso}','{empleado.Foto}',{empleado.Empresa.IdEmpresa}");    
                    if (query >=1)
                    {
                        result.Message = "Usuario Agregado Correctamente";
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {

              result.Message ="Error al agregar al empleado: "+ ex.Message;
              result.Correct = false;
            }

            return result;
        }

        public static ML.Result Update(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EmaquedaProgramacionNcapasContext context=new DL.EmaquedaProgramacionNcapasContext())
                {
            
                    var query = context.Database.ExecuteSqlRaw($"EmpleadoUpdate '{empleado.NumeroEmpleado}','{empleado.RFC}','{empleado.Nombre}'," +
                       $"'{empleado.ApellidoPaterno}','{empleado.ApellidoMaterno}','{empleado.Email}','{empleado.Telefono}','{empleado.FechaNacimiento}'," +
                       $"'{empleado.NSS}','{empleado.FechaIngreso}','{empleado.Foto}',{empleado.Empresa.IdEmpresa},{empleado.IdEmpleado}");
                    if (query >=1)
                    {
                        result.Message = "Empleado actualizado correctamente";
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {

               result.Message = "Error: "+ex.Message;
               result.Correct = false;
            }
            return result;
        }

        public static ML.Result Delete(int idEmpleado) 
        { 
            var result = new ML.Result();
            try
            {
                using (DL.EmaquedaProgramacionNcapasContext context=new DL.EmaquedaProgramacionNcapasContext())
                {
                   
                    int query = context.Database.ExecuteSqlRaw($"EmpleadoDelete {idEmpleado}");
                    if (query >=1)
                    {
                        result.Message = "Empleado elimnido correctamente";
                        result.Correct=false;
                    }
                }
            }
            catch (Exception ex)
            {

                result.Message= "Error: "+ex.Message;
                result.Correct = false;
            }
            return result;
        
        }

        public static ML.Result GetById(int idEmpleado) {
            var result = new ML.Result();
            try
            {
                using (DL.EmaquedaProgramacionNcapasContext context=new DL.EmaquedaProgramacionNcapasContext())
                {
   
                    var query = context.Empleados.FromSqlRaw($"EmpleadoGetById {idEmpleado}").AsEnumerable().FirstOrDefault();
                    if (query != null)
                    {
                            ML.Empleado empleado = new ML.Empleado();
                            empleado.IdEmpleado = query.IdEmpleado;
                            empleado.NumeroEmpleado = query.NumeroEmpleado;
                            empleado.Nombre = query.Nombre;
                            empleado.ApellidoMaterno = query.ApellidoMaterno;
                            empleado.ApellidoPaterno = query.ApellidoPaterno;
                            empleado.Foto = query.Foto;
                            empleado.NSS = query.Nss;
                            empleado.RFC = query.Rfc;
                            empleado.Email = query.Email;
                            empleado.Telefono = query.Telefono;
                            empleado.FechaNacimiento = query.FechaNacimiento.ToString("dd/MM/yyyy");
                            empleado.FechaIngreso = query.FechaIngreso.ToString("dd/MM/yyyy");

                            empleado.Empresa = new ML.Empresa();
                            empleado.Empresa.Nombre = query.EmpresaNombre;
                            empleado.Empresa.IdEmpresa = query.IdEmpresa.Value;
                            empleado.Empresa.Telefono = query.EmpresaTelefono;
                            empleado.Empresa.Email = query.EmpresaMail;
                            empleado.Empresa.DireccionWeb = query.DireccionWeb;
                            empleado.Empresa.Logo = query.Logo;

                            result.Object=empleado;
                        
                    }
                    result.Correct = true;
                }
            }
            catch (Exception)
            {

                result.Correct=false;
            }
            return result;
        
        }

    }
}

