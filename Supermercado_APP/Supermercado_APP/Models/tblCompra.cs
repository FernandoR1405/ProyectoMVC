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
    
    public partial class tblCompra
    {
        public int Cop_Id { get; set; }
        public string Cop_NumFactura { get; set; }
        public Nullable<System.DateTime> Cop_Fecha { get; set; }
        public Nullable<int> Prd_Id { get; set; }
        public Nullable<int> Cop_Cantidad { get; set; }
        public bool Cop_Estado { get; set; }
        public int Cop_UsuarioCrea { get; set; }
        public System.DateTime Cop_FechaCrea { get; set; }
        public Nullable<int> Cop_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> Cop_FechaModifica { get; set; }
    
        public virtual tblUsuario tblUsuario { get; set; }
        public virtual tblUsuario tblUsuario1 { get; set; }
        public virtual tblProducto tblProducto { get; set; }
    }
}
