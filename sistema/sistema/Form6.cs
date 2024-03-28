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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Crear una instancia del nuevo formulario
            Form7 form7 = new Form7();

            // Mostrar el nuevo formulario
            form7.Show();

            // Opcional: Cerrar el formulario actual si es necesario
            this.Close();
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            // Cerrar el formulario actual
            this.Close();

            // Mostrar el Form1
            Form3 form3 = new Form3();
            form3.Show();
        }
    }
}
