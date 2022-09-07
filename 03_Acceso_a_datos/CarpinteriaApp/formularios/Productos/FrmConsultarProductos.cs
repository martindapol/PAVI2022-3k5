using CarpinteriaApp.datos;
using CarpinteriaApp.dominio;
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
            new FrmNuevoProducto(1, new Producto()).ShowDialog();
            this.btnConsultar_Click(null, null);
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
            if (resultados.Rows.Count > 0) { 
                //Volver a cargar
                foreach (DataRow fila in resultados.Rows)
                {
                    bool activo = fila["activo"].ToString().Equals("S");

                    int id = (int)fila["id_producto"];
                    dgvProductos.Rows.Add(new object[] { fila["n_producto"].ToString(), fila["precio"], activo, id });
                }
                habitarControles(true);
            }
            else
            {
                habitarControles(false);
            }
        }

        private void habitarControles(bool v)
        {
            btnEditar.Enabled = v;
            btnEliminar.Enabled = v;
        }
    

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Seguro que desea salir?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {   DataGridViewRow fila = dgvProductos.CurrentRow;
            if ( fila != null)
            {
                new FrmNuevoProducto(3, mapper(fila)).ShowDialog();
                this.btnConsultar_Click(null, null);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            DataGridViewRow fila = dgvProductos.CurrentRow;
            if (fila != null)
            {
                new FrmNuevoProducto(2, mapper(fila)).ShowDialog();
                this.btnConsultar_Click(null, null);
            }
        }

        private Producto mapper(DataGridViewRow fila)
        {
            Producto oSeleted = new Producto();
            oSeleted.Nombre = fila.Cells["colNombre"].Value.ToString();
            oSeleted.Precio = double.Parse(fila.Cells["colPrecio"].Value.ToString());
            oSeleted.Activo = (bool)fila.Cells["colActivo"].Value;
            oSeleted.ProductoNro = (int)fila.Cells["colId"].Value;
            return oSeleted;
        }

    }
}
