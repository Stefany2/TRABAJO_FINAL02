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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(progressBar1.Value < 100)
            {
                progressBar1.Value += 1;

                label2.Text = progressBar1.Value.ToString() + "%";
            }
           else
            {
                timer1.Stop();

                // Mostrar el MessageBox
                DialogResult result = MessageBox.Show("INICIO SECCIÒN CORRECTAMENTE", "Mensaje", MessageBoxButtons.OK);

                // Verificar si el usuario hizo clic en "Aceptar"
                if (result == DialogResult.OK)
                {
                    // Cerrar Form2
                    this.Close();

                    // Abrir Form3
                    Form3 form3 = new Form3();
                    form3.Show();
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
