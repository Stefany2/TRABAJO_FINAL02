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
    public partial class Form11 : Form
    {
        // Cadena de conexión a tu base de datos SQL Server
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\stefany;Initial Catalog=almacen;Integrated Security=True");
        public Form11()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            // Crear una instancia del formulario Form3
            Form3 form3 = new Form3();

            // Mostrar el formulario Form4
            form3.Show();

            // Opcional: Cerrar el formulario actual si es necesario
            this.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // Habilitar los campos de texto
            txtbIDProducto.Enabled = true;
            txtbCliente.Enabled = true;
            txtbMovimiento.Enabled = true;
            txtbSalida.Enabled = true;
            
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {

        }
    }
}
