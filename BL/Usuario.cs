using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.OleDb;

namespace BL
{
    public class Usuario
    {
        public static ML.Result GetAll(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EmaquedaProgramacionNcapasContext context=new DL.EmaquedaProgramacionNcapasContext())
                {
                    //var query = context.UsuarioGetAll().ToList();
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetAll '{usuario.Nombre}','{usuario.ApellidoPaterno}','{usuario.ApellidoMaterno}'").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            usuario = new ML.Usuario();
                            usuario.IdUsuario = obj.IdUsuario;
                            usuario.UserName = obj.UserName;
                            usuario.ApellidoPaterno = obj.ApellidoPaterno;
                            usuario.ApellidoMaterno = obj.ApellidoMaterno;
                            usuario.Nombre = obj.Nombre;
                            usuario.Email = obj.Email;
                            usuario.Password = obj.Password;
                            usuario.FechaNacimiento = obj.FechaNacimiento.ToString("dd/MM/yyyy");
                            usuario.Sexo = obj.Sexo;
                            usuario.NumeroTelefono = obj.Telefono;
                            usuario.Celular = obj.Celular;
                            usuario.CURP = obj.Curp;
                            usuario.NombreCompleto = obj.Nombre + " " + obj.ApellidoPaterno + " " + obj.ApellidoMaterno;
                            usuario.Imagen = obj.Imagen;
                            usuario.Status = obj.Status.Value;

                            usuario.Rol = new ML.Rol();
                            usuario.Rol.IdRol = obj.IdRol.Value;
                            usuario.Rol.Nombre = obj.Cargo;

                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.IdDireccion = obj.IdDireccion;
                            usuario.Direccion.Calle = obj.Calle;
                            usuario.Direccion.NumeroInterior = obj.NumeroInterior;
                            usuario.Direccion.NumeroExterior = obj.NumeroExterior;

                            usuario.Direccion.Colonia = new ML.Colonia();
                            usuario.Direccion.Colonia.IdColonia = obj.IdColonia;
                            usuario.Direccion.Colonia.Nombre = obj.NombreColonia;
                            usuario.Direccion.Colonia.CodigoPostal = obj.CodigoPostal;

                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.IdMunicipio = obj.IdMunicipio;
                            usuario.Direccion.Colonia.Municipio.Nombre = obj.NombreMunicipio;

                            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            usuario.Direccion.Colonia.Municipio.Estado.IdEstado = obj.IdEstado;
                            usuario.Direccion.Colonia.Municipio.Estado.Nombre = obj.NombreEstado;

                            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = obj.IdPais;
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = obj.NombrePais;

                            result.Objects.Add(usuario);
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
        public static ML.Result GetById(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EmaquedaProgramacionNcapasContext context=new DL.EmaquedaProgramacionNcapasContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetById {idUsuario}").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();

                        usuario.IdUsuario = query.IdUsuario;
                        usuario.UserName = query.UserName;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.Nombre = query.Nombre;
                        usuario.Email = query.Email;
                        usuario.Password = query.Password;
                        usuario.FechaNacimiento = query.FechaNacimiento.ToString("dd/MM/yyyy");
                        usuario.Sexo = query.Sexo;
                        usuario.NumeroTelefono = query.Telefono;
                        usuario.Celular = query.Celular;
                        usuario.CURP = query.Curp;
                        usuario.Imagen = query.Imagen;
                        usuario.Status = query.Status.Value;

                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = query.IdRol.Value;
                        usuario.Rol.Nombre = query.Cargo;

                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.IdDireccion = query.IdDireccion;
                        usuario.Direccion.Calle = query.Calle;
                        usuario.Direccion.NumeroInterior = query.NumeroInterior;
                        usuario.Direccion.NumeroExterior = query.NumeroExterior;

                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.IdColonia = query.IdColonia;
                        usuario.Direccion.Colonia.Nombre = query.NombreColonia;

                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = query.IdMunicipio;
                        usuario.Direccion.Colonia.Municipio.Nombre = query.NombreMunicipio;

                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = query.IdEstado;
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = query.NombreEstado;

                        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = query.IdPais;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = query.NombrePais;


                        result.Object = usuario;
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
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.EmaquedaProgramacionNcapasContext context= new DL.EmaquedaProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"UsuarioAdd {usuario.Rol.IdRol},'{usuario.UserName}', '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}'," +
                        $"'{usuario.Email}','{usuario.Password}','{usuario.FechaNacimiento}','{usuario.Sexo}','{usuario.NumeroTelefono}','{usuario.Celular}'," +
                        $"'{usuario.CURP}','{usuario.Direccion.Calle}','{usuario.Direccion.NumeroInterior}','{usuario.Direccion.NumeroExterior}'," +
                        $"{usuario.Direccion.Colonia.IdColonia},'{usuario.Imagen}'");
                    if (query >= 1)
                    {
                        result.Correct = true;
                        result.Message = "Usuario Agregado Correctamente";
                    }

                    //result.Correct = true;

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = "Error al ingresar al usuario: " + ex.Message;
            }
            return result;
        }
        public static ML.Result Update(ML.Usuario usuario)
        {

            ML.Result result = new ML.Result();
            try
            {
                using (DL.EmaquedaProgramacionNcapasContext context=new DL.EmaquedaProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"UsuarioUpdate {usuario.Rol.IdRol},'{usuario.UserName}', '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}'," +
                    $"'{usuario.Email}','{usuario.Password}','{usuario.FechaNacimiento}','{usuario.Sexo}','{usuario.NumeroTelefono}','{usuario.Celular}',"+
                    $"'{usuario.CURP}',{usuario.IdUsuario},'{usuario.Direccion.Calle}','{usuario.Direccion.NumeroInterior}','{usuario.Direccion.NumeroExterior}',{usuario.Direccion.Colonia.IdColonia},'{usuario.Imagen}'");
                    if (query >= 1)
                    {
                        result.Correct = true;
                        result.Message = "Usuario Actualizado Correctamente";
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = "Error al actualizar el usuario: " + ex.Message;

            }
            return result;

        }
        public static ML.Result Delete(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EmaquedaProgramacionNcapasContext context=new DL.EmaquedaProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"UsuarioDelete {idUsuario}");
                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "Usuario Borrado de la base de datos";
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
        public static ML.Result UpdateStatus(int idUsuario, bool status) {
        
            ML.Result result=new ML.Result();
            try
            {
                using (DL.EmaquedaProgramacionNcapasContext context = new DL.EmaquedaProgramacionNcapasContext()) {


                    int query = context.Database.ExecuteSqlRaw($"UsuarioUpdateStatus {idUsuario},'{status}'");
                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "Estaus Actualizado";
                    }
                }

                   

               
            }
            catch (Exception ex)
            {

               result.Correct= false;
                result.Message = "error: " + ex.Message;
            }
            return result;
        }
        public static ML.Result GetByUsername(string userName) {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EmaquedaProgramacionNcapasContext context = new DL.EmaquedaProgramacionNcapasContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetByUsername '{userName}'").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();


                        usuario.UserName = query.UserName;
                        usuario.Password = query.Password;
                        usuario.Rol=new ML.Rol();
                        usuario.Rol.IdRol = query.IdRol.Value;
                        result.Correct = true;


                        result.Object = usuario;
                    }
                    else {
                        result.Correct = false;
                    }
                   
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = "Error usuario no enecontrado: " + ex.Message;

            }
            return result;

        }

        public static ML.Result ConvertXSLXtoDataTable(string connString)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (OleDbConnection context = new OleDbConnection(connString))
                {
                    string query = "SELECT * FROM [Usuarios$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;


                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = cmd;

                        DataTable tableMateria = new DataTable();

                        da.Fill(tableMateria);

                        if (tableMateria.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach (DataRow row in tableMateria.Rows)
                            {
                                ML.Usuario usuario = new ML.Usuario();
                                usuario.Rol=new ML.Rol();

                                usuario.Rol.IdRol = byte.Parse(row[0].ToString());
                                
                                usuario.UserName = row[1].ToString();
                                usuario.Nombre = row[2].ToString();
                                usuario.ApellidoPaterno= row[3].ToString();
                                usuario.ApellidoMaterno= row[4].ToString();
                                usuario.Email= row[5].ToString();
                                usuario.Password= row[6].ToString();
                                usuario.FechaNacimiento= row[7].ToString();
                                usuario.Sexo= row[8].ToString();
                                usuario.NumeroTelefono= row[9].ToString();
                                usuario.Celular = row[10].ToString();
                                usuario.CURP= row[11].ToString();
                                usuario.Status= bool.Parse(row[12].ToString());

                                usuario.Direccion = new ML.Direccion();
                                usuario.Direccion.Calle= row[13].ToString();
                                usuario.Direccion.NumeroInterior= row[14].ToString();
                                usuario.Direccion.NumeroExterior= row[15].ToString();
                                
                                usuario.Direccion.Colonia=new ML.Colonia();
                                usuario.Direccion.Colonia.IdColonia= int.Parse(row[16].ToString());

                                result.Objects.Add(usuario);
                            }

                            result.Correct = true;

                        }

                        result.Object = tableMateria;

                        if (tableMateria.Rows.Count > 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.Message = "No existen registros en el excel";
                        }
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
        public static ML.Result ValidarExcel(List<object> Object)
        {
            ML.Result result = new ML.Result();

            try
            {
                result.Objects = new List<object>();
                //DataTable  //Rows //Columns
                int i = 1;
                foreach (ML.Usuario usuario in Object)
                {
                    ML.ErrorExcel error = new ML.ErrorExcel();
                    error.IdRegistro = i++;

                    if (usuario.Rol.IdRol.ToString() == "")
                    {
                        error.Mensaje += "Ingresar el nombre  ";
                    }
                    if (usuario.UserName == "")
                    {
                        error.Mensaje += "Ingresar el username ";
                    }
                    if (usuario.Nombre == "")
                    {
                        error.Mensaje += "Ingresar el Nombre ";
                    }
                    if (usuario.ApellidoPaterno == "")
                    {
                        error.Mensaje += "Ingresar el apellido paterno ";
                    }
                    if (usuario.ApellidoMaterno == "")
                    {
                        error.Mensaje += "Ingresar el apellido materno ";
                    }
                    if (usuario.Email == "")
                    {
                        error.Mensaje += "Ingresar el email ";
                    }
                    if (usuario.Password == "")
                    {
                        error.Mensaje += "Ingresar el password ";
                    }
                    if (usuario.FechaNacimiento == "")
                    {
                        error.Mensaje += "Ingresar la fecha de nacimiento ";
                    }
                    if (usuario.Sexo == "")
                    {
                        error.Mensaje += "Ingresar el sexo ";
                    }  
                    if (usuario.NumeroTelefono == "")
                    {
                        error.Mensaje += "Ingresar el telefono ";
                    }
                    if (usuario.Celular == "")
                    {
                        error.Mensaje += "Ingresar el celular ";
                    }
                    if (usuario.CURP == "")
                    {
                        error.Mensaje += "Ingresar el curp ";
                    }
                    if (usuario.Status.ToString() == "")
                    {
                        error.Mensaje += "Ingresar el status ";
                    }
                    if (usuario.Direccion.Calle == "")
                    {
                        error.Mensaje += "Ingresar la calle ";
                    } 
                    if (usuario.Direccion.NumeroInterior == "")
                    {
                        error.Mensaje += "Ingresar el numero interior ";
                    }
                    if (usuario.Direccion.NumeroExterior == "")
                    {
                        error.Mensaje += "Ingresar el numero exterior ";
                    }
                    if (usuario.Direccion.Colonia.IdColonia.ToString() == "")
                    {
                        error.Mensaje += "Ingresar el numero exterior ";
                    }
                    if (error.Mensaje != null)
                    {
                        result.Objects.Add(error);
                    }


                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = "Error: "+ex.Message;

            }

            return result;
        }

    }
}