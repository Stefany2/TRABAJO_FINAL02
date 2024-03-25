using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sistema
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
            Form2 form2 = new Form2(); // Crear una instancia de Form2
            form2.Show(); // Mostrar Form2
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
