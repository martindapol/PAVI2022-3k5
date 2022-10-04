using CarpinteriaApp.datos;
using CarpinteriaApp.dominio;
using CarpinteriaApp.servicios;
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
            //Generar una dependencia hacia el servicio:
            GestorProducto gestor = new GestorProducto();
            List<Producto> lista = gestor.ConsultarProductosFiltro(txtNombre.Text, chkActivos.Checked);
            //Limpiar grilla
            dgvProductos.Rows.Clear();
            if (lista.Count > 0)
            {
                //Volver a cargar
                foreach (Producto oProducto in lista)
                {
                    dgvProductos.Rows.Add(new object[] { oProducto.Nombre, oProducto.Precio, oProducto.Activo, oProducto.ProductoNro });
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
            if (MessageBox.Show("Seguro que desea salir?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DataGridViewRow fila = dgvProductos.CurrentRow;
            if (fila != null)
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
