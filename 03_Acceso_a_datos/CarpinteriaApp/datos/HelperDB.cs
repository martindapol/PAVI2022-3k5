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
        private static HelperDB instancia;

        private HelperDB()
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

        public static HelperDB GetInstancia()
        {
            if (instancia == null)
                instancia = new HelperDB();
            return instancia;
        }

        public SqlConnection ObtenerConexion()
        {
            return cnn;
        }
      
    }
}

