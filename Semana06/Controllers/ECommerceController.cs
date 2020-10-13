using Semana06.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Semana06.Controllers
{
    public class ECommerceController : Controller
    {
        // Conexion
        SqlConnection cn = new SqlConnection(
            "Server=localhost;Database=Negocios2020;Trusted_Connection=True");
        IEnumerable<Producto> productos()/*listar los registros de tb_productos*/
        {
            List<Producto> temporal = new List<Producto>();
            using (SqlCommand cmd = new SqlCommand("Select * from tb_productos", cn))
            {
                cmd.CommandType = CommandType.Text;
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Producto c = new Producto()
                    {
                        idproducto = dr.GetInt32(0),
                        descripcion = dr.GetString(1),
                        precio = dr.GetDecimal(5),
                        stock = dr.GetInt16(6)
                    };
                    temporal.Add(c);
                }
                dr.Close(); cn.Close();
            }
            return temporal;
        }
        public ActionResult Tienda()
        {
            //cuando la tienda se apertura(primera vez) creamos el session
            if (Session["carrito"] == null)
            {
                Session["carrito"] = new List<Item>();
            }

            //envio a la vista los productos
            return View(productos());
        }
        public ActionResult Seleccionar(int? id = null, int cantidad = 1, string mensaje = "")
        {
            //si id es null, direccionamos a Tienda
            if (id == null) return RedirectToAction("Tienda");

            //si id no es null, buscar y enviar el registro
            Producto reg = productos().Where(x => x.idproducto == id).FirstOrDefault();
            ViewBag.mensaje = mensaje;
            return View(reg);
        }
        public ActionResult Agregar(int id, int cantidad)
        {
            //buscar al producto por su id
            Producto reg = productos().Where(x => x.idproducto == id).FirstOrDefault();

            //definir el Item y agregar valores al objeto
            Item it = new Item();
            it.idproducto = reg.idproducto;
            it.descripcion = reg.descripcion;
            it.precio = reg.precio;
            it.cantidad = cantidad; //form

            //agregar it al Session carrito: casteo el session y el objeto temporal almacena it
            List<Item> temporal = (List<Item>)Session["carrito"];
            temporal.Add(it);

            ViewBag.mensaje = "Agregado al carrito";
            return RedirectToAction("Seleccionar", new { id = id, cantidad = cantidad, mensaje = ViewBag.mensaje });
        }
        public ActionResult Canasta()
        {
            //si el session esta vacio
            if (Session["carrito"] == null)
                return RedirectToAction("Tienda");

            //visualizar lo almacenado en el Session
            return View((List<Item>)Session["carrito"]);
        }
    }
}