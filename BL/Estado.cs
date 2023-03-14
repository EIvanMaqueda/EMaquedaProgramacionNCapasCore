using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Estado
    {
        public static ML.Result EstadoGetByIdPais(int idPais)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EmaquedaProgramacionNcapasContext context= new DL.EmaquedaProgramacionNcapasContext())
                {
                   var query = context.Estados.FromSqlRaw($"EstadoGetByIdPais {idPais}").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Estado estado= new ML.Estado();
                            estado.IdEstado=obj.IdEstado;
                            estado.Nombre=obj.Nombre;
                            result.Objects.Add(estado);
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
