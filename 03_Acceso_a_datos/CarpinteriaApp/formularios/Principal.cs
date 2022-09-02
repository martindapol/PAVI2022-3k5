using CarpinteriaApp.formularios.Productos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarpinteriaApp.formularios
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
        }

        private void nuevoPresupuestoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Frm_Alta_Presupuesto().ShowDialog();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("App Presupuestos Carpintería V1.0", "PAVI2022", MessageBoxButtons.OK, MessageBoxIcon.Information );
        }

        private void gestionarProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmConsultarProductos().ShowDialog();
        }

        private void nuevoProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmNuevoProducto().ShowDialog();
        }
    }
}
