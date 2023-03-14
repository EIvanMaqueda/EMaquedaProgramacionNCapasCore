using Microsoft.EntityFrameworkCore;
using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Aseguradora
    {
        public static ML.Result AddAseguradora(ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.EmaquedaProgramacionNcapasContext context= new DL.EmaquedaProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"AseguradoraAdd '{aseguradora.Nombre}',{aseguradora.Usuario.IdUsuario}");

                    if (query >= 1)
                    {
                        result.Correct = true;
                        result.Message = "Aseguradora Agregada Correctamente";
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = "Error al ingresar la aseguradora: " + ex.Message;
            }
            return result;
        }

        public static ML.Result UpdateAseguradora(ML.Aseguradora aseguradora)
        {

            ML.Result result = new ML.Result();
            try
            {
                using (DL.EmaquedaProgramacionNcapasContext context=new DL.EmaquedaProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"AseguradoraUpdate {aseguradora.IdAseguradora},'{aseguradora.Nombre}',{aseguradora.Usuario.IdUsuario}");
                    if (query >= 1)
                    {
                        result.Correct = true;
                        result.Message = "Aseguradora Actualizada Correctamente";
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = "Error al actualizar la Aseguradora: " + ex.Message;

            }
            return result;

        }

        public static ML.Result DeleteAseguradora(int idAseguradora)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EmaquedaProgramacionNcapasContext context=new DL.EmaquedaProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"AseguradoraDelete {idAseguradora}");
                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "Aseguradora Borrada de la base de datos";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = "Error: " + ex.Message;

            }
            return result;
        }



        public static ML.Result GetAllAseguradora()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EmaquedaProgramacionNcapasContext context= new DL.EmaquedaProgramacionNcapasContext())
                {

                    var query = context.Aseguradoras.FromSqlRaw("AseguradoraGetAll").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Aseguradora aseguradora = new ML.Aseguradora();
                            aseguradora.IdAseguradora = obj.IdAseguradora;
                            aseguradora.Nombre= obj.Nombre;
                            aseguradora.FechaCreacion = obj.FechaCreacion.Value;
                            aseguradora.FechaModificacion = obj.FechaModificacion.Value;

                            aseguradora.Usuario = new ML.Usuario();
                            aseguradora.Usuario.IdUsuario = obj.IdUsuario.Value;
                            aseguradora.Usuario.Nombre = obj.NombreUsuario;
                            aseguradora.Usuario.ApellidoPaterno = obj.ApellidoPaterno;
                            aseguradora.Usuario.ApellidoMaterno= obj.ApellidoMaterno;
                            result.Objects.Add(aseguradora);
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

        public static ML.Result GetByIdAseguradora(int idAseguradora)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EmaquedaProgramacionNcapasContext context=new DL.EmaquedaProgramacionNcapasContext())
                {
                    
                    var query = context.Aseguradoras.FromSqlRaw($"AseguradoraGetById {idAseguradora}").AsEnumerable().FirstOrDefault();
                    if (query != null)
                    {


                        ML.Aseguradora aseguradora = new ML.Aseguradora();
                        aseguradora.IdAseguradora = query.IdAseguradora;
                        aseguradora.Nombre = query.Nombre;
                        aseguradora.FechaCreacion = query.FechaCreacion.Value;
                        aseguradora.FechaModificacion = query.FechaModificacion.Value;

                        aseguradora.Usuario = new ML.Usuario();
                        aseguradora.Usuario.IdUsuario = query.IdUsuario.Value;
                        result.Object = aseguradora;
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = "Error usuario no enecontrado: " + ex.Message;

            }
            return result;
        }

      
    }
}
