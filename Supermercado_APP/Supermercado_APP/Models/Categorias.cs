using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Supermercado_APP.Models
{
    [MetadataType(typeof(tblCategoriaMeData))]
    public partial class tblCategoria
    {
    }

    public class tblCategoriaMeData
    {
        [Display(Name = "Id")]
        public int Cat_Id { get; set; }
        [Display(Name = "Descripcion")]
        public string Cat_Descripcion { get; set; }

        [Display(Name = "Estado")]
        public bool Cat_Estado { get; set; }

        [Display(Name = "Usuario Crea")]
        public int Cat_UsuarioCrea { get; set; }
        [Display(Name = "Fecha de Creacion")]
        public System.DateTime Cat_FechaCrea { get; set; }
        [Display(Name = "Modificacido por")]
        public Nullable<int> Cat_UsuarioModifica { get; set; }
        [Display(Name = "Fecha de Modificacion")]
        public Nullable<System.DateTime> Cat_FechaModifica { get; set; }
    }
}