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
    
    public partial class tblDepartamento
    {
        public tblDepartamento()
        {
            this.tblCiudades = new HashSet<tblCiudade>();
        }
    
        public int Dep_Codigo { get; set; }
        public string Dep_Descripcion { get; set; }
        public Nullable<bool> Dep_Activo { get; set; }
        public Nullable<int> Dep_UsuarioCrea { get; set; }
        public Nullable<System.DateTime> Dep_FechaCrea { get; set; }
        public Nullable<int> Dep_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> Dep_FechaModifica { get; set; }
    
        public virtual tblUsuario tblUsuario { get; set; }
        public virtual tblUsuario tblUsuario1 { get; set; }
        public virtual ICollection<tblCiudade> tblCiudades { get; set; }
    }
}