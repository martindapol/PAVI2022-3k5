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
    public partial class FrmNuevoProducto : Form
    {
        private int accion; // 1-2-3
        private Producto oProducto;
        private GestorProducto gestor;
        public FrmNuevoProducto(int accion, Producto oProducto)
        {
            InitializeComponent();
            this.accion = accion;
            this.oProducto = oProducto;
            gestor = new GestorProducto();
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

            if (accion == 1)
            {
                int respuesta =  gestor.CrearProducto(oProducto);
                
                if (respuesta == 1)
                {
                    MessageBox.Show("Producto insertado!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error al insertar el producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }else if(accion == 3)
            {
                List<Parametro> lst = new List<Parametro>();
                lst.Add(new Parametro("@id", oProducto.ProductoNro));
                //Baja física:
                //string delete = "DELETE FROM T_PRODUCTOS WHERE id_producto = @id";

                //baja lógica:
                string delete = "UPDATE T_PRODUCTOS SET activo = 'N' WHERE id_producto = @id";

                int respuesta = HelperDB.GetInstancia().EjecutarSQL(delete, lst);

                if (respuesta == 1)
                {
                    MessageBox.Show("Producto eliminado!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error al borrar el producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                //Editar:
                List<Parametro> lst = new List<Parametro>();
                lst.Add(new Parametro("@id", oProducto.ProductoNro));
                lst.Add(new Parametro("@nombre", txtNombre.Text));
                lst.Add(new Parametro("@precio", nudPrecio.Value));

                string update = "UPDATE T_PRODUCTOS SET n_producto = @nombre, precio = @precio WHERE id_producto = @id";

                int respuesta = HelperDB.GetInstancia().EjecutarSQL(update, lst);

                if (respuesta == 1)
                {
                    MessageBox.Show("Producto actualizado!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error al actualizar el producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void FrmNuevoProducto_Load(object sender, EventArgs e)
        {
            if (accion != 1) // Editar o Borrar
            {
                txtNombre.Text = oProducto.Nombre;
                nudPrecio.Value = (decimal)oProducto.Precio;
                chkActivo.Checked = oProducto.Activo;

                if (accion == 3)
                {
                    gbDatos.Enabled = false;
                    this.Text = "Registrar baja de Producto";
                }
                else
                {
                    this.Text = "Modificar Producto";
                }

            }
            
        }

        
    }
}
