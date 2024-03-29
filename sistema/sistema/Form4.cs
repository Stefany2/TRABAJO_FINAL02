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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        // Cadena de conexión a tu base de datos SQL Server
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\stefany;Initial Catalog=almacen;Integrated Security=True");
        
        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Crear una instancia del nuevo formulario
            Form5 form5 = new Form5();

            // Mostrar el nuevo formulario
            form5.Show();

            // Opcional: Cerrar el formulario actual si es necesario
             this.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            
        }
       
        private void btnMenu_Click(object sender, EventArgs e)
        {
            // Cerrar el formulario actual
            this.Close();

            // Mostrar el Form3
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // Habilitar todos los TextBox excepto txtbCodigo
            txtbNombre.Enabled = true;
            txtbDescripcion.Enabled = true;
            txtbCategoria.Enabled = true;
            txtbPrecio.Enabled = true;
            txtbStock.Enabled = true;
            txtbCantidadVendida.Enabled = true;
            dateTimePicker1.Enabled = true;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            // Guardar los datos en la base de datos
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Productos (Nombre_Producto, Descripcion_Producto, Categoria, Precio, Stock, Cantidad_Vendida, Fecha_Vencimiento) VALUES (@Nombre, @Descripcion, @Categoria, @Precio, @Stock, @CantidadVendida, @FechaVencimiento)", con);
                cmd.Parameters.AddWithValue("@Nombre", txtbNombre.Text);
                cmd.Parameters.AddWithValue("@Descripcion", txtbDescripcion.Text);
                cmd.Parameters.AddWithValue("@Categoria", txtbCategoria.Text);
                cmd.Parameters.AddWithValue("@Precio", Convert.ToDecimal(txtbPrecio.Text));
                cmd.Parameters.AddWithValue("@Stock", Convert.ToInt32(txtbStock.Text));
                cmd.Parameters.AddWithValue("@CantidadVendida", Convert.ToInt32(txtbCantidadVendida.Text));
                cmd.Parameters.AddWithValue("@FechaVencimiento", dateTimePicker1.Enabled);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Datos insertados correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar datos: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

            // Mostrar los datos en el DataGridView
            MostrarDatos();
        }

        private void MostrarDatos()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Productos", con);
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar datos: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void txtbCodigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtbNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtbDescripcion_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtbCategoria_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
