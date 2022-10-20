using CarpinteriaApp.datos;
using CarpinteriaApp.dominio;
using CarpinteriaApp.servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CarpinteriaApp.formularios
{
    public partial class Frm_Alta_Presupuesto : Form
    {
        private GestorPresupuesto gestor;
        private Presupuesto nuevo;
        public Frm_Alta_Presupuesto()
        {
            InitializeComponent();
            gestor = new GestorPresupuesto();
            CargarProductos();
            //Crear nuevo presupuesto:
            nuevo = new Presupuesto();
            

        }

        private void Frm_Alta_Presupuesto_Load(object sender, EventArgs e)
        {
            ProximoPresupuesto();
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtCliente.Text = "CONSUMIDOR FINAL";
            txtDto.Text = "0";
            this.ActiveControl = cboProductos; // Set foco al combo
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cboProductos.Text.Equals(String.Empty))
            {
                MessageBox.Show("Debe seleccionar un PRODUCTO!", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtCantidad.Text == "" || !int.TryParse(txtCantidad.Text, out _))
            {
                MessageBox.Show("Debe ingresar una cantidad válida!", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            foreach (DataGridViewRow row in dgvDetalles.Rows)
            {
                if (row.Cells["colProd"].Value.ToString().Equals(cboProductos.Text))
                {
                    MessageBox.Show("PRODUCTO: " + cboProductos.Text + " ya se encuentra como detalle!", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;

                }
            }

            Producto p = (Producto)cboProductos.SelectedItem;
            int cantidad = Convert.ToInt32(txtCantidad.Text);

            DetallePresupuesto detalle = new DetallePresupuesto(p, cantidad);
            nuevo.AgregarDetalle(detalle);
            dgvDetalles.Rows.Add(new object[] { p.ProductoNro, p.Nombre, p.Precio, cantidad});

            CalcularTotal();
        }

        private void dgvDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDetalles.CurrentCell.ColumnIndex == 4)
            {
                nuevo.QuitarDetalle(dgvDetalles.CurrentRow.Index);
                //click button:
                dgvDetalles.Rows.Remove(dgvDetalles.CurrentRow);
                //presupuesto.quitarDetalle();
                CalcularTotal();

            }
        }

        private void CalcularTotal()
        {
            double total = nuevo.CalcularTotal();
            txtTotal.Text = total.ToString();

            if (txtDto.Text != "")
            {
                double dto = (total * Convert.ToDouble(txtDto.Text)) / 100;
                txtFinal.Text = (total - dto).ToString();
            }

        }

        private void CargarProductos()
        {
            GestorProducto gestorProducto = new GestorProducto();

            List<Producto> lst = gestorProducto.ConsultarProductosFiltro("", true);//gestor.ConsultaSQL("SP_CONSULTAR_PRODUCTOS");
            if (lst != null)
            {
                cboProductos.DataSource = lst;
                cboProductos.DisplayMember = "Nombre";
                cboProductos.ValueMember = "ProductoNro";
            }
        }
        private void ProximoPresupuesto()
        {
            int next = gestor.ObtenerUltimoId();
            if (next > 0)
                lblNroPresupuesto.Text = "Presupuesto Nº: " + next.ToString();
            else
                MessageBox.Show("Error de datos. No se puede obtener Nº de presupuesto!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void GuardarPresupuesto()
        {
            //datos del presupuesto:
            Presupuesto oPresupuesto = new Presupuesto();
            oPresupuesto.Cliente = txtCliente.Text;
            oPresupuesto.Descuento = Convert.ToDouble(txtDto.Text);
            oPresupuesto.Fecha = Convert.ToDateTime(txtFecha.Text);

            if (gestor.ConfirmarPresupuesto(oPresupuesto))
            {
                MessageBox.Show("Presupuesto registrado", "Informe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
            }
            else
            {
                MessageBox.Show("ERROR. No se pudo registrar el presupuesto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtCliente.Text == "")
            {
                MessageBox.Show("Debe ingresar un cliente!", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (dgvDetalles.Rows.Count == 0)
            {
                MessageBox.Show("Debe ingresar al menos detalle!", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            GuardarPresupuesto();
        }
    }
}
