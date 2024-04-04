using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace sistema
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        // Cadena de conexión a tu base de datos SQL Server
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\stefany;Initial Catalog=almacen;Integrated Security=True");

        public void logear(string usuario,string contrasena){
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Nombre, Tipo_usuario FROM usuarios WHERE Usuario = @usuario AND Clave = @Clave",con);
                cmd.Parameters.AddWithValue("Usuario",usuario);
                cmd.Parameters.AddWithValue("Clave", contrasena);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if(dt.Rows.Count ==1){

                    this.Hide();
                    // evaluamos el tipo de usuario que ingresa
                    if (dt.Rows[0][1].ToString()== "Admin"){
                        new Admin(dt.Rows[0][0].ToString()).Show();

                    } else if(dt.Rows[0][1].ToString() == "Usuario")
                    {
                        new Usuario(dt.Rows[0][0].ToString()).Show();
                    }
                }else{
                    MessageBox.Show("usuario y /o Contraseña incorrecta");
                }
            } 
            catch(Exception e){
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }
        }



    private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // Verifica si el checkBox está marcado
            if (checkBox1.Checked)
            {
                // Muestra la contraseña
                textBox2.PasswordChar = '\0'; // Carácter nulo para mostrar el texto sin ocultar
            }
            else
            {
                // Oculta la contraseña
                textBox2.PasswordChar = '*'; // Puedes usar cualquier carácter que desees para ocultar la contraseña
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Cierra el formulario actual
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // boton ingresar
            logear(this.textBox1.Text, this.textBox2.Text);
            
        }
       

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Crear una instancia del formulario Form9
            Form9 form9 = new Form9();

            // Mostrar el formulario Form9
            form9.Show();

            // Ocultar el formulario actual (Form1)
            this.Hide();
        }
        
    }
}
