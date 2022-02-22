using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Supermercado_APP.Models
{
    [MetadataType(typeof(tblUsuarioMeData))] 
    public partial class tblUsuario
    {
    }
    public class tblUsuarioMeData
    {
        [Display(Name = "Id")]
        public int Usu_Id { get; set; }

        [Display(Name = "Nombre de Usuario")]
        public string Usu_UsuarioNombre { get; set; }

        [Display(Name = "Contraseña")]
        public string Usu_Contrasena { get; set; }

        [Display(Name = "Es Admin ?")]
        public Nullable<bool> Usu_EsAdmin { get; set; }

        [Display(Name = "Cargo")]
        public Nullable<int> CarP_Id { get; set; }

        [Display(Name = "Estado")]
        public Nullable<bool> Usu_Activo { get; set; }

        [Display(Name = "Creado Por")]
        public int Usu_UsuarioCrea { get; set; }

        [Display(Name = "Fecha de Creacion")]
        public System.DateTime Usu_FechaCrea { get; set; }

        [Display(Name = "Modificado Por")]
        public Nullable<int> Usu_UsuarioModifica { get; set; }

        [Display(Name = "Fecha de Modificacion")]
        public Nullable<System.DateTime> Usu_FechaModifica { get; set; }
    }
}