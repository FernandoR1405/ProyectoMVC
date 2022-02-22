//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Supermercado_APP.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblDireccione
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblDireccione()
        {
            this.tblProveedores = new HashSet<tblProveedore>();
            this.tblPersonas = new HashSet<tblPersona>();
        }
    
        public int Dir_Id { get; set; }
        public Nullable<int> Ciu_Codigo { get; set; }
        public string Dir_Sector { get; set; }
        public string Dir_Calle { get; set; }
        public string Dir_Avenida { get; set; }
        public string Dir_Bloque { get; set; }
        public Nullable<bool> Dir_Activo { get; set; }
        public Nullable<int> Dir_UsuarioCrea { get; set; }
        public Nullable<System.DateTime> Dir_FechaCrea { get; set; }
        public Nullable<int> Dir_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> Dir_FechaModifica { get; set; }
    
        public virtual tblUsuario tblUsuario { get; set; }
        public virtual tblUsuario tblUsuario1 { get; set; }
        public virtual tblCiudade tblCiudade { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblProveedore> tblProveedores { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblPersona> tblPersonas { get; set; }
    }
}