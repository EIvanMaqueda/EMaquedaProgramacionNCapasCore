using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public ML.Rol Rol { get; set; }
        [Required]
        
        public string UserName { get; set; }

        [Required]
        [RegularExpression("[a-zA-Z ]{2,254}", ErrorMessage ="solo se aceptan letras")]
        public string Nombre { get; set; }

        [Required]
        [RegularExpression("[a-zA-Z ]{2,254}", ErrorMessage = "solo se aceptan letras")]
        public string ApellidoPaterno { get; set; }

        [RegularExpression("[a-zA-Z ]{2,254}")]
        public string ApellidoMaterno { get; set; }
        [Required]
        [RegularExpression("[/\\S+@\\S+\\.\\S+/]", ErrorMessage = "el email debe de llevar un @")]
        public string Email { get; set; }
       
        [Required]
       // [RegularExpression("[^(?=.[a-z])(?=.[A-Z])(?=.\\d)(?=.[@$!%?&])[A-Za-z\\d@$!%?&]{8,10}$]")]
     
        public string Password { get; set; }

        [Required]
        public string FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        
        [Required]
        [RegularExpression("[^\\d{10}$]", ErrorMessage = "solo se aceptan numeros")]
        public string NumeroTelefono { get; set; }
        
        [RegularExpression("[^\\d{10}$]", ErrorMessage = "solo se aceptan numeros")]
        public string? Celular { get; set; }
        
        [Required]
        [RegularExpression("[^[\\s\\S]{0,18}$]", ErrorMessage = "debe de contener 18 caracteres")]
        public string CURP { get; set; }
        public string? Imagen { get; set; }
        public ML.Direccion? Direccion { get; set; }
        public List<object>? Usuarios { get; set; }

        public string? NombreCompleto { get; set; }
        public bool Status { get; set; }



    }
}
