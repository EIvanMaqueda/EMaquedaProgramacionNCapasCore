using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Colonia
    {

        public static ML.Result ColoniaGetByIdMunicipio(int idMunicipio)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EmaquedaProgramacionNcapasContext context=new DL.EmaquedaProgramacionNcapasContext())
                {
                    var query = context.Colonia.FromSqlRaw($"ColoniaGetByIdMunicipio {idMunicipio}").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Colonia colonia=new ML.Colonia();
                            colonia.IdColonia=obj.IdColonia;
                            colonia.Nombre=obj.Nombre;
                            colonia.CodigoPostal=obj.CodigoPostal;
                            result.Objects.Add(colonia);
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
