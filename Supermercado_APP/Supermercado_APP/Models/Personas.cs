using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Supermercado_APP.Models
{
    [MetadataType(typeof(tblPersonaMeData))]
    public partial class tblPersona
    {
    }

    public class tblPersonaMeData
    {
        [Display(Name = "Id")]
        public int Per_Id { get; set; }

        [Display(Name = "Identidad")]
        public string Per_Identidad { get; set; }

        [Display(Name = "RTN")]
        public string Per_Rtn { get; set; }

        [Display(Name = "Nombres")]
        public string Per_Nombres { get; set; }

        [Display(Name = "Primer Apellido")]
        public string Per_PrimerApellido { get; set; }

        [Display(Name = "Segundo Apellido")]
        public string Per_SegundoApellido { get; set; }

        [Display(Name = "Sexo")]
        public string Per_Sexo { get; set; }

        [Display(Name = "Direccion")]
        public Nullable<int> Dir_Id { get; set; }

        [Display(Name = "Telefono")]
        public string Per_Telefono { get; set; }

        [Display(Name = "Correo")]
        public string Per_Correo { get; set; }

        [Display(Name = "Estado")]
        public int Per_EsActivo { get; set; }

        [Display(Name = "Creado Por")]
        public int Per_UsuarioCrea { get; set; }

        [Display(Name = "Fecha de Creacion")]
        public System.DateTime Per_FechaCrea { get; set; }

        [Display(Name = "Modificado Por")]
        public Nullable<int> Per_UsuarioModifica { get; set; }

        [Display(Name = "Fecha de Modificacion")]
        public Nullable<System.DateTime> Per_FechaModifica { get; set; }
    }
}