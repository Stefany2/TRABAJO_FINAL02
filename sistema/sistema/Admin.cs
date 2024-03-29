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
    public partial class Admin : Form
    {
        public Admin(string nombre)
        {
            InitializeComponent();
            lblmensaje.Text = nombre;
        }

        private void Admin_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Crear una instancia del formulario Form3
            Form3 form3 = new Form3();

            // Mostrar el formulario Form3
            form3.Show();

            // Opcionalmente, puedes ocultar este formulario si ya no lo necesitas
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            // cerrar el formulario
            this.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            // Minimizar el formulario al hacer clic en pictureBox4
            this.WindowState = FormWindowState.Minimized;
        }

        private void lblAdmin_Click(object sender, EventArgs e)
        {

        }
    }
}
