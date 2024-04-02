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
    public partial class ProveedorBuscar : Form
    {
        public ProveedorBuscar()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\stefany;Initial Catalog=almacen;Integrated Security=True");
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            // Limpiar los TextBox
            txtbProveedor.Text = "";
           
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            // Minimizar el formulario
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            // Cerrar el formulario actual
            this.Close();
        }
        private void CargarDatos()
        {
            // Consulta SQL para obtener todos los productos
            string query = "SELECT * FROM Proveedor";

            // Llenar el DataGridView con los datos de la base de datos
            using (SqlCommand command = new SqlCommand(query, con))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;

                // Mostrar la cantidad de registros cargados en el cuadro de texto txtbTotalRegistros
                txtbTotalRegistros.Text = dataGridView1.Rows.Count.ToString();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Limpiar el DataGridView antes de cargar los nuevos datos
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();


            // Obtener el valor de búsqueda
            string busqueda = txtbProveedor.Text.Trim();

            // Construir la consulta SQL dinámica
            string query = "SELECT * FROM Proveedor WHERE Nombre_Contacto LIKE @Busqueda";

            // Ejecutar la consulta y llenar el DataGridView con los resultados
            using (SqlCommand command = new SqlCommand(query, con))
            {
                command.Parameters.AddWithValue("@Busqueda", "%" + busqueda + "%");
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                    dataGridView1.DataSource = table;

                    // Mostrar la cantidad de registros encontrados en el cuadro de texto txtbTotalRegistros
                    txtbTotalRegistros.Text = dataGridView1.Rows.Count.ToString();
                }
                else
                {
                    MessageBox.Show("No se encontraron proveedores con el nombre ingresado.", "No se encontraron resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }
    }
}
