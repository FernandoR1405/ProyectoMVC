//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Supermercado_APP.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblPersona
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblPersona()
        {
            this.tblVentaEncabezadoes = new HashSet<tblVentaEncabezado>();
            this.tblCargosPersonas = new HashSet<tblCargosPersona>();
        }
    
        public int Per_Id { get; set; }
        public string Per_Identidad { get; set; }
        public string Per_Rtn { get; set; }
        public string Per_Nombres { get; set; }
        public string Per_PrimerApellido { get; set; }
        public string Per_SegundoApellido { get; set; }
        public string Per_Sexo { get; set; }
        public Nullable<int> Dir_Id { get; set; }
        public string Per_Telefono { get; set; }
        public string Per_Correo { get; set; }
        public int Per_EsActivo { get; set; }
        public int Per_UsuarioCrea { get; set; }
        public System.DateTime Per_FechaCrea { get; set; }
        public Nullable<int> Per_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> Per_FechaModifica { get; set; }
    
        public virtual tblUsuario tblUsuario { get; set; }
        public virtual tblUsuario tblUsuario1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblVentaEncabezado> tblVentaEncabezadoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCargosPersona> tblCargosPersonas { get; set; }
        public virtual tblDireccione tblDireccione { get; set; }
    }
}
