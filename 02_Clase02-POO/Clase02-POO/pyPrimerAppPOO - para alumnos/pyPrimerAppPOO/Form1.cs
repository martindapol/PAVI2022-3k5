using System;
using System.Windows.Forms;

namespace pyPrimerAppPOO
{
    public partial class FrmParametros : Form
    {
        public FrmParametros()
        {
            InitializeComponent();
        }

        private void btnProbarCnn_Click(object sender, EventArgs e)
        {
            
        }

        private void FrmParametros_Load(object sender, EventArgs e)
        {
          
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
                  }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void chkIntegrada_CheckedChanged(object sender, EventArgs e)
        {
 
            txtPassword.Enabled = !chkIntegrada.Checked;
            txtUsuario.Enabled = !chkIntegrada.Checked;

        }
    }
}
