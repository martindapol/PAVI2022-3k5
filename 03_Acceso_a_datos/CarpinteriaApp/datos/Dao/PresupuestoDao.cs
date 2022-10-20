using CarpinteriaApp.dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpinteriaApp.datos.Dao
{
    public class PresupuestoDao : IPresupuestoDao
    {
        public bool Crear(Presupuesto presupuesto)
        {
            bool aux = false;
            SqlConnection cnn = HelperDB.GetInstancia().ObtenerConexion();
            SqlTransaction t = null;
            try
            {
                string queryMaestro = "INSERT INTO T_PRESUPUESTOS (fecha, cliente, descuento, total) VALUES(@fecha, @cliente, @desc, @total);";

                cnn.Open();
                t = cnn.BeginTransaction();

                SqlCommand cmd = new SqlCommand(queryMaestro, cnn, t);
                //cmd.CommandType = System.Data.CommandType.Text; Opcional: valor por defecto
                cmd.Parameters.AddWithValue("@fecha", presupuesto.Fecha);
                cmd.Parameters.AddWithValue("@cliente", presupuesto.Cliente);
                cmd.Parameters.AddWithValue("@desc", presupuesto.Descuento);
                cmd.Parameters.AddWithValue("@total", presupuesto.CalcularTotal());
                cmd.ExecuteNonQuery();

                string queryIdentity = "SELECT @@IDENTITY as ult";
                
                SqlCommand cmdUltimo = new SqlCommand(queryIdentity, cnn, t);
                SqlDataReader reader =  cmdUltimo.ExecuteReader();
                reader.Read();
                int id =  int.Parse(reader["ult"].ToString());
                reader.Close();
                int nro_detalle = 1;
                foreach (DetallePresupuesto detalle in presupuesto.Detalles)
                {
                    string queryDetalle = "INSERT INTO T_DETALLES_PRESUPUESTO (presupuesto_nro, detalle_nro, id_producto, cantidad) VALUES(@presupuesto_nro, @detalle_nro, @id_producto, @cantidad);";
                    SqlCommand cmdD = new SqlCommand(queryDetalle, cnn, t);
                    cmdD.Parameters.AddWithValue("@presupuesto_nro", id);
                    cmdD.Parameters.AddWithValue("@detalle_nro", nro_detalle);
                    cmdD.Parameters.AddWithValue("@id_producto", detalle.Producto.ProductoNro);
                    cmdD.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                    cmdD.ExecuteNonQuery();

                    nro_detalle++;
                }

                t.Commit();
                aux = true;

            }
            catch (Exception Ex)
            {
                if (t != null)
                    t.Rollback();
                aux = false;
            }
            finally
            {
                if (cnn != null && cnn.State == System.Data.ConnectionState.Open)
                    cnn.Close();
            }

            return aux;
        }

        public int ProximoId()
        {
            int aux = 0;
            SqlConnection cnn = null;

            try
            {
                cnn = HelperDB.GetInstancia().ObtenerConexion();
                cnn.Open();
                string queryIdentity = "SELECT max(presupuesto_nro) + 1 FROM T_PRESUPUESTOS";
                SqlCommand cmdUltimo = new SqlCommand(queryIdentity, cnn);
                aux = (int)cmdUltimo.ExecuteScalar();
            }
            finally 
            {
                if (cnn != null && cnn.State == System.Data.ConnectionState.Open)
                    cnn.Close();
            }
            return aux;
        }
    }
}
