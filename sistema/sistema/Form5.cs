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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        // Cadena de conexión a tu base de datos SQL Server
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\stefany;Initial Catalog=almacen;Integrated Security=True");
        private void Form5_Load(object sender, EventArgs e)
        {
            
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            // Cerrar el formulario actual
            this.Close();

            // Mostrar el Form3
            Form4 form4 = new Form4();
            form4.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            // Cerrar el formulario actual
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            // Minimizar el formulario
            this.WindowState = FormWindowState.Minimized;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtbProducto_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtbCodigo_TextChanged(object sender, EventArgs e)
        {
            
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Limpiar el DataGridView antes de cargar los nuevos datos
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();

            // Obtener el valor de búsqueda
            string busqueda = "";

            if (!string.IsNullOrWhiteSpace(txtbProducto.Text))
            {
                // Si el cuadro de texto de producto tiene un valor, se realizará la búsqueda por nombre de producto
                busqueda = txtbProducto.Text.Trim();
            }
            else if (!string.IsNullOrWhiteSpace(txtbCodigo.Text))
            {
                // Si el cuadro de texto de código tiene un valor, se realizará la búsqueda por código
                busqueda = txtbCodigo.Text.Trim();
            }
            else
            {
                // Si ambos cuadros de texto están vacíos, salir del método sin realizar la búsqueda
                return;
            }

            // Construir la consulta SQL dinámica
            string query = "SELECT * FROM Productos WHERE Nombre_Producto LIKE @Busqueda OR codigo = @Codigo";

            // Ejecutar la consulta y llenar el DataGridView con los resultados
            using (SqlCommand command = new SqlCommand(query, con))
            {
                command.Parameters.AddWithValue("@Busqueda", "%" + busqueda + "%");
                command.Parameters.AddWithValue("@Codigo", busqueda);
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
                    MessageBox.Show("No se encontraron productos con el código o nombre ingresado.", "No se encontraron resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        private void CargarDatos()
         {
            // Consulta SQL para obtener todos los productos
            string query = "SELECT * FROM Productos";

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

          private void btnLimpiar_Click(object sender, EventArgs e)
          {
            // Limpiar los TextBox
            txtbProducto.Text = "";
            txtbCodigo.Text = "";
            
           }
        

    }
}
        
    











