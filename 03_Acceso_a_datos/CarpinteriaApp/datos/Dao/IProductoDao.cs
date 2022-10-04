using CarpinteriaApp.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpinteriaApp.datos.Dao
{
    public interface IProductoDao
    {
        List<Producto> GetByFilter(string nombre, bool activo);
        int Create(Producto nuevo);
    }
}
