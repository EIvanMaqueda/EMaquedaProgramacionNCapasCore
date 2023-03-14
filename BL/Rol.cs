using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Rol
    {
        public static ML.Result GetAllRol()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EmaquedaProgramacionNcapasContext context= new DL.EmaquedaProgramacionNcapasContext())
                {
                    //var query = context.RolGetAll().ToList();
                    var query = context.Rols.FromSqlRaw("RolGetAll").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Rol rol = new ML.Rol();
                            rol.IdRol = obj.IdRol;
                            rol.Nombre = obj.Nombre;
                            result.Objects.Add(rol);
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
    }
}
