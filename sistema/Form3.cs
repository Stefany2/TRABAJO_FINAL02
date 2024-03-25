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
    public partial class Form3 : Form
    { 
        public Form3()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Crear una instancia del formulario Form4
            Form6 form6 = new Form6();

            // Mostrar el formulario Form4
            form6.Show();

            // Opcional: Cerrar el formulario actual si es necesario
             this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            // Cierra el formulario actual
            this.Close();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Crear una instancia del formulario Form4
            Form4 form4 = new Form4();

            // Mostrar el formulario Form4
            form4.Show();

            // Opcional: Cerrar el formulario actual si es necesario
             this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // boton inventario
            // Cerrar el formulario actual
            this.Close();

            // Mostrar el Form1
            Form8 form8 = new Form8();
            form8.Show();
        }
    }
}
