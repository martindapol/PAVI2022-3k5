using CarpinteriaApp.dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpinteriaApp.datos.Dao
{
    public class ProductoDao : IProductoDao
    {
        public int Create(Producto nuevo)
        {   //Tomar el producto nuevo y armar una lista <L> de 
            //parametros 

            //Pedir al HelperDB: EjecutarSQl(query, L)
            //Devolver la cantidad de filas afectas
            return 1;
        }

        public List<Producto> GetByFilter(string nombre, bool activo)
        {
            List<Producto> lst = new List<Producto>();
            string query = "SELECT * FROM T_PRODUCTOS WHERE 1=1";
          
            if (!string.IsNullOrEmpty(nombre))
            {
                query += " AND n_producto LIKE '%" + nombre + "%'";
            }
            if (activo)
            {
                query += " AND activo = 'S'";
            }
            else
            {
                query += " AND activo = 'N'";
            }

            List<Parametro> parametros = new List<Parametro>();
            parametros.Add(new Parametro("@nombre", nombre));
            parametros.Add(new Parametro("@activo", activo));

            DataTable resultados = new HelperDB().ConsultaSQL(query, parametros);
            foreach (DataRow fila in resultados.Rows)
            {
                Producto aux = new Producto();
                aux.ProductoNro = (int)fila["id_producto"];
                aux.Nombre = fila["n_producto"].ToString();
                aux.Precio = double.Parse(fila["precio"].ToString());
                aux.Activo = fila["activo"].ToString().Equals("S");
                lst.Add(aux);
            }
            return lst;

        }
    }
}
