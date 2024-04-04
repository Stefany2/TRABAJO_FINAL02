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


namespace sistema
{
    public partial class factura : Form
    {
        // Cadena de conexión a tu base de datos SQL Server
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\stefany;Initial Catalog=almacen;Integrated Security=True");

        public factura()
        {
            InitializeComponent();
        }

        private void factura_Load(object sender, EventArgs e)
        {
            // Cargar los productos en el ComboBox al cargar el formulario
            CargarProductos();
        }

        private void CargarProductos()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Codigo, Nombre_Producto, Precio FROM Productos", con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBoxProductos.Items.Add(new Producto((string)reader["Codigo"], (string)reader["Nombre_Producto"], (decimal)reader["Precio"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void comboBoxProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el producto seleccionado del ComboBox
            Producto productoSeleccionado = (Producto)comboBoxProductos.SelectedItem;

            // Mostrar el código y precio del producto en los controles correspondientes
            lblCodigoProducto.Text = productoSeleccionado.Codigo;
            lblPrecioProducto.Text = productoSeleccionado.Precio.ToString();

            // Calcular y mostrar el total a pagar
            CalcularTotal();
        }

        private void CalcularTotal()
        {
            if (!string.IsNullOrEmpty(txtCantidad.Text))
            {
                decimal precio = Convert.ToDecimal(lblPrecioProducto.Text);
                int cantidad = Convert.ToInt32(txtCantidad.Text);
                decimal total = precio * cantidad;
                txtTotalPagar.Text = total.ToString();
            }
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            CalcularTotal();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Obtener los datos
            string codigoProducto = lblCodigoProducto.Text;
            string nombreProducto = comboBoxProductos.Text;
            decimal precioProducto = Convert.ToDecimal(lblPrecioProducto.Text);
            int cantidad = Convert.ToInt32(txtCantidad.Text);
            decimal totalPagar = Convert.ToDecimal(txtTotalPagar.Text);

            // Agregar los datos al DataGridView
            dataGridView1.Rows.Add(codigoProducto, nombreProducto, precioProducto, cantidad, totalPagar);
        }
    }

    public class Producto
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }

        public Producto(string codigo, string nombre, decimal precio)
        {
            Codigo = codigo;
            Nombre = nombre;
            Precio = precio;
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
