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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace sistema
{
    public partial class Form9 : Form
    {
        // Cadena de conexión a tu base de datos SQL Server
        string connectionString = @"Data Source=(localdb)\stefany;Initial Catalog = almacen; Integrated Security = True";
        public Form9()
        {
            InitializeComponent();
            
            // Agregar los tipos de usuario al ComboBox al cargar el formulario
            comboBox1.Items.Add("Usuario");
            // Agregar más tipos de usuario si es necesario
            comboBox1.SelectedIndex = 0; // Seleccionar el primer elemento por defecto
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Obtener los valores ingresados por el usuario
            string nombreCompleto = textBoxNombreCompleto.Text;
            string usuario = textBoxUsuario.Text;
            string clave = textBoxClave.Text;
            string tipoUsuario = comboBox1.SelectedItem.ToString();

            // Insertar los datos en la base de datos
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO usuarios (nombre, Usuario, clave, Tipo_usuario) " +
                                   "VALUES (@NombreCompleto, @Usuario, @Clave, @TipoUsuario)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@NombreCompleto", nombreCompleto);
                    command.Parameters.AddWithValue("@Usuario", usuario);
                    command.Parameters.AddWithValue("@Clave", clave);
                    command.Parameters.AddWithValue("@TipoUsuario", tipoUsuario);
                    command.ExecuteNonQuery();

                    // Mostrar mensaje de registro exitoso
                    MessageBox.Show("El usuario se registró correctamente", "Mensaje", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar el usuario: " + ex.Message);
            }

            // Cerrar el formulario actual
            this.Close();

            // Mostrar el Form1
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Agregar los tipos de usuario al ComboBox
            comboBox1.Items.Add("Usuario ");
            // Agregar más tipos de usuario si es necesario

            // Seleccionar el primer elemento por defecto
            comboBox1.SelectedIndex = 0;
            // Obtener el tipo de usuario seleccionado
            string tipoUsuario = comboBox1.SelectedItem.ToString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // Verifica si el checkBox está marcado
            if (checkBox1.Checked)
            {
                // Muestra la contraseña
                textBoxClave.PasswordChar = '\0'; // Carácter nulo para mostrar el texto sin ocultar
            }
            else
            {
                // Oculta la contraseña
                textBoxClave.PasswordChar = '*'; // Puedes usar cualquier carácter que desees para ocultar la contraseña
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            // Cierra el formulario actual
            this.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            // Minimizar el formulario al hacer clic en pictureBox4
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
