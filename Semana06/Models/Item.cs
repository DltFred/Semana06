using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Semana06.Models
{
    public class Item
    {
        public int idproducto { get; set; }
        public string descripcion { get; set; }
        public decimal precio { get; set; }
        public int cantidad { get; set; }
        public decimal monto { get { return precio * cantidad; } }
    }
}