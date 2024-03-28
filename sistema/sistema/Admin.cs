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
            lblmensajeAdmin.Text = nombre;
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
    }
}
