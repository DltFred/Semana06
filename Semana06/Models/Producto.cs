using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Semana06.Models
{
    public class Producto
    {
        public int idproducto { get; set; }
        public string descripcion { get; set; }
        public decimal precio { get; set; }
        public Int16 stock { get; set; }
    }
}