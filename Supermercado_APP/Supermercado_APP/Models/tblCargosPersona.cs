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
    
    public partial class tblCargosPersona
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblCargosPersona()
        {
            this.tblUsuarios = new HashSet<tblUsuario>();
        }
    
        public int CarP_Id { get; set; }
        public Nullable<int> Carg_Id { get; set; }
        public Nullable<int> Per_Id { get; set; }
        public int CarP_EsActivo { get; set; }
        public int CarP_UsuarioCrea { get; set; }
        public System.DateTime CarP_FechaCrea { get; set; }
        public Nullable<int> CarP_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> CarP_FechaModifica { get; set; }
    
        public virtual tblUsuario tblUsuario { get; set; }
        public virtual tblUsuario tblUsuario1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblUsuario> tblUsuarios { get; set; }
        public virtual tblCargo tblCargo { get; set; }
        public virtual tblPersona tblPersona { get; set; }
    }
}