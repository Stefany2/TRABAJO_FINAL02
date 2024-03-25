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
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Mostrar mensaje de registro exitoso
            MessageBox.Show("El usuario se registró correctamente", "Mensaje", MessageBoxButtons.OK);

            // Cerrar el formulario actual
            this.Close();

            // Mostrar el Form1
            Form1 form1 = new Form1();
            form1.Show();
        }
    }
}
