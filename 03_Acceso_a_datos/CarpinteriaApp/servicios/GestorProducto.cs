using CarpinteriaApp.datos;
using CarpinteriaApp.datos.Dao;
using CarpinteriaApp.dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpinteriaApp.servicios
{
    public class GestorProducto
    {
        private IProductoDao dao;

        public GestorProducto()
        {
            dao = new ProductoDao();
        }

        public List<Producto> ConsultarProductosFiltro(string nombre, bool activo)
        {
            List<Producto> lst = dao.GetByFilter(nombre, activo);
            return lst;
        }

        public int CrearProducto(Producto oProducto)
        {
            return dao.Create(oProducto);
        }
    }
}
