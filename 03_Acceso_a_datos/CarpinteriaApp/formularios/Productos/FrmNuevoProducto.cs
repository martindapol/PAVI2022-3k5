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
    public partial class FrmNuevoProducto : Form
    {
        public FrmNuevoProducto()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close(); // cierra y descarga los recursos
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //1. Validar los datos obligatorios y los tipos de datos
            if (String.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("Campo Nombre es requerido", "Validaciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //2. Tomar los datos de los controles y traducir a objetos como parámetros

            /*string activo = "N";
            if (chkActivo.Checked)
                activo = "S";
            */

            string activo = chkActivo.Checked ? "S" : "N";
            List<Parametro> lst = new List<Parametro>();
            lst.Add(new Parametro("@nombre", txtNombre.Text));
            lst.Add(new Parametro("@precio", nudPrecio.Value));
            lst.Add(new Parametro("@activo", activo));


            string insert = "INSERT INTO T_PRODUCTOS (n_producto, precio, activo) VALUES(@nombre, @precio, @activo)";
            int respuesta = new HelperDB().EjecutarSQL(insert, lst);

            if (respuesta == 1)
            {
                MessageBox.Show("Producto insertado!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Ha ocurrido un error al insertar el producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
