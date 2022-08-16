using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pyPrimerCSharp
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void btnAzul_Click(object sender, EventArgs e)
        {
            lblTexto.ForeColor = Color.Blue;
        }

        private void btnAmarillo_Click(object sender, EventArgs e)
        {
            lblTexto.ForeColor = Color.Yellow;
        }

        private void FrmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            if (MessageBox.Show("¿Seguro que desea salir?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                e.Cancel = true;
        }

    }
}
