//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Supermercado_APP.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblCargosPersona
    {
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
        public virtual ICollection<tblUsuario> tblUsuarios { get; set; }
        public virtual tblCargo tblCargo { get; set; }
        public virtual tblPersona tblPersona { get; set; }
    }
}