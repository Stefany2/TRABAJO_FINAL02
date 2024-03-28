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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Crear una instancia del nuevo formulario
            Form5 form5 = new Form5();

            // Mostrar el nuevo formulario
            form5.Show();

            // Opcional: Cerrar el formulario actual si es necesario
             this.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            // Cerrar el formulario actual
            this.Close();

            // Mostrar el Form3
            Form3 form3 = new Form3();
            form3.Show();
        }
    }
}
