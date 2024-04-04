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
    public partial class Reclamos : Form
    {
        // Cadena de conexión a tu base de datos SQL Server
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\stefany;Initial Catalog=almacen;Integrated Security=True");

        public Reclamos()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // boton minimizar
            // Minimizar el formulario
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtbMotivo_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            // Obtener los valores de los TextBox
            string motivo = txtbMotivo.Text;
            bool devolucionProveedor = true;

            try
            {
                // Abrir la conexión
                con.Open();

                // Query SQL para insertar los datos en la tabla Devolucion
                string query = "INSERT INTO Devolucion (Motivo_Salida, Devolucion_Proveedor) VALUES (@Motivo_Salida, @DevolucionProveedor)";

                // Crear el comando SQL
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    // Agregar parámetros para evitar inyección SQL
                    command.Parameters.AddWithValue("@Motivo_Salida", motivo);
                    command.Parameters.AddWithValue("@Devolucion_Proveedor", devolucionProveedor);

                    // Ejecutar el comando SQL
                    int rowsAffected = command.ExecuteNonQuery();

                    // Verificar si se insertaron filas en la base de datos
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Reclamo enviado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Hubo un problema al enviar el reclamo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar a la base de datos: " + ex.Message, "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Cerrar la conexión después de realizar la operación
                con.Close();
            }
        }
    }
}
