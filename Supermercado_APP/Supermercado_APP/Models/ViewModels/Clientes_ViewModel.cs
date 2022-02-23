using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Supermercado_APP.Models.ViewModels
{
    public class Clientes_ViewModel
    {
        public int Per_Id { get; set; }
        public string Per_Identidad { get; set; }
        public string Per_Rtn { get; set; }
        public string Per_Nombres { get; set; }
        public string Per_Apellido1 { get; set; }
        public string Per_Apellido2 { get; set; }
        public char Per_Sexo { get; set; }
        public int Dir_Id { get; set; }
        public string Per_Telefono { get; set; }
        public string Per_Correo { get; set; }
    }
}