using CarpinteriaApp.datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarpinteriaApp.formularios.Productos
{
    public partial class FrmConsultarProductos : Form
    {
        public FrmConsultarProductos()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            new FrmNuevoProducto().ShowDialog();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM T_PRODUCTOS WHERE 1=1";
            if (!string.IsNullOrEmpty(txtNombre.Text))
            {
                query += " AND n_producto LIKE '%" + txtNombre.Text + "%'";
            }
            if (chkActivos.Checked)
            {
                query += " AND activo = 'S'";
            }
            else
            {
                query += " AND activo = 'N'";
            }

            List<Parametro> lst = new List<Parametro>();
            /* lst.Add(new Parametro("@nombre", txtNombre.Text));
            */
            DataTable resultados = new HelperDB().ConsultaSQL(query, lst);
            //Limpiar grilla
            dgvProductos.Rows.Clear();

            //Volver a cargar
            foreach (DataRow fila in resultados.Rows)
            {
                bool activo = fila["activo"].ToString().Equals("S");
                dgvProductos.Rows.Add(new object[] { fila["n_producto"].ToString(), fila["precio"].ToString(), activo });
            }

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Seguro que desea salir?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
