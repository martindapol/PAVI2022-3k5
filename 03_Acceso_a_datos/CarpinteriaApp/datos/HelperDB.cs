using CarpinteriaApp.dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CarpinteriaApp.datos
{
    class HelperDB
    {
        private SqlConnection cnn;

        public HelperDB()
        {
            cnn = new SqlConnection(Properties.Resources.cnnString);
        }

        public DataTable ConsultaSQL(string strSql, List<Parametro> lst)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable tabla = new DataTable();

            cnn.Open();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSql;

            //if (lst != null && lst.Count > 0)
            foreach (Parametro p in lst)
            {
                cmd.Parameters.AddWithValue(p.Nombre, p.Valor);
            }

            tabla.Load(cmd.ExecuteReader());
            cnn.Close();

            return tabla;
        }


        //Permite ejecutar INSERT/UPDATE/DELETE
        //a partir de una consulta SQL con parametros
        public int EjecutarSQL(string strSql, List<Parametro> lst)
        {
            int afectadas = 0;
            SqlCommand cmd = new SqlCommand();
            cnn.Open();
            cmd.Connection = cnn;
            cmd.CommandText = strSql;

            foreach (Parametro p in lst)
            {
                cmd.Parameters.AddWithValue(p.Nombre, p.Valor);
            }

            afectadas = cmd.ExecuteNonQuery();
            cnn.Close();

            return afectadas;
        }

        public int ProximoPresupuesto()
        {
            SqlCommand cmd = new SqlCommand();
            cnn.Open();
            cmd.Connection = cnn;
            cmd.CommandText = "SP_PROXIMO_ID";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter pOut = new SqlParameter();
            pOut.ParameterName = "@next";
            pOut.DbType = DbType.Int32;
            pOut.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(pOut);
            cmd.ExecuteNonQuery();

            cnn.Close();
            return (int)pOut.Value;

        }

        public bool ConfirmarPresupuesto(Presupuesto oPresupuesto)
        {
            bool ok = true;

            SqlCommand cmd = new SqlCommand();

            cnn.Open();
            cmd.Connection = cnn;
            cmd.CommandText = "SP_INSERTAR_MAESTRO";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@cliente", oPresupuesto.Cliente);
            cmd.Parameters.AddWithValue("@dto", oPresupuesto.Descuento);
            cmd.Parameters.AddWithValue("@total", oPresupuesto.CalcularTotal());

            //parámetro de salida:
            SqlParameter pOut = new SqlParameter();
            pOut.ParameterName = "@presupuesto_nro";
            pOut.DbType = DbType.Int32;
            pOut.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(pOut);
            cmd.ExecuteNonQuery();

            int presupuestoNro = (int)pOut.Value;

            SqlCommand cmdDetalle;
            int detalleNro = 1;
            foreach (DetallePresupuesto item in oPresupuesto.Detalles)
            {
                cmdDetalle = new SqlCommand();

                cmdDetalle.Connection = cnn;
                cmdDetalle.CommandText = "SP_INSERTAR_MAESTRO";
                cmdDetalle.CommandType = CommandType.StoredProcedure;
                cmdDetalle.Parameters.AddWithValue("@presupuesto_nro", presupuestoNro);
                cmdDetalle.Parameters.AddWithValue("@detalle", detalleNro);
                cmdDetalle.Parameters.AddWithValue("@id_producto", item.Producto.ProductoNro);
                cmdDetalle.Parameters.AddWithValue("@cantidad", item.Cantidad);
                cmd.ExecuteNonQuery();


                detalleNro++;
            }

            cnn.Close();

            return ok;
        }
    }
}

