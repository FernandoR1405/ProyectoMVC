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
    
    public partial class tblCargo
    {
        public tblCargo()
        {
            this.tblCargosPersonas = new HashSet<tblCargosPersona>();
        }
    
        public int Carg_Id { get; set; }
        public string Carg_Descripcion { get; set; }
        public int Carg_EsActivo { get; set; }
        public int Carg_UsuarioCrea { get; set; }
        public System.DateTime Carg_FechaCrea { get; set; }
        public Nullable<int> Carg_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> Carg_FechaModifica { get; set; }
    
        public virtual tblUsuario tblUsuario { get; set; }
        public virtual tblUsuario tblUsuario1 { get; set; }
        public virtual ICollection<tblCargosPersona> tblCargosPersonas { get; set; }
    }
}