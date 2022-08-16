using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            //Probar conexión al server:
            SqlConnection cnn = new SqlConnection();

            if (txtNombreDB.Text.Equals(""))
                MessageBox.Show("Falta indicar nombre de la Base de Datos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                try
                {
                    if (chkIntegrada.Checked)
                        cnn.ConnectionString = @"Data Source=MARTIN-PC\SQLEXPRESS;Initial Catalog=" + txtNombreDB.Text + ";Integrated Security=True";
                    else
                        cnn.ConnectionString = @"Data Source=MARTIN-PC\SQLEXPRESS;Initial Catalog=" + txtNombreDB.Text + ";User ID= " + txtUsuario.Text + ";Password=" + txtPassword.Text;
                    cnn.Open();
                    
                    //ejecutar algún comando
                    MessageBox.Show("Conexión establecida", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                
                catch (SqlException ex)
                {
                    MessageBox.Show("Conexión NO establecida!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error inesperado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                finally // opcional
                {
                    if (cnn.State == ConnectionState.Open)
                        cnn.Close();
                }

            }



        }

        private void FrmParametros_Load(object sender, EventArgs e)
        {
            Configuracion oConfiguracion = Configuracion.Instance;
            txtNombreDB.Text = oConfiguracion.NombreDB;
            txtUsuario.Text = oConfiguracion.Usuario;
            txtPassword.Text = oConfiguracion.Clave;
    
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtNombreDB.Text) || String.IsNullOrEmpty(txtUsuario.Text) || String.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Todos los campos son requeridos!", "Valilación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            

            Configuracion oConfiguracion = Configuracion.Instance;
            oConfiguracion.NombreDB = txtNombreDB.Text;
            oConfiguracion.Usuario = txtUsuario.Text;
            oConfiguracion.Clave = txtPassword.Text;
            oConfiguracion.Guardar();

            MessageBox.Show("Todos los parámetros se guardaron exitosamente!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
