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

    }
}
