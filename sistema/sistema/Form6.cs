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
using ExcelDataReader;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Security.Cryptography;
using System.IO;

namespace sistema
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }
        // Cadena de conexión a tu base de datos SQL Server
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\stefany;Initial Catalog=almacen;Integrated Security=True");

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Crear una instancia del formulario ProveedorBuscar
            ProveedorBuscar proveedorBuscarForm = new ProveedorBuscar();

            // Mostrar el formulario ProveedorBuscar
            proveedorBuscarForm.Show();

            // Cerrar el formulario actual
            this.Close();

        }

        private void Form6_Load(object sender, EventArgs e)
        {
            // Llenar el ComboBox con los tipos de pago disponibles
            comboBoxTiposPago.Items.Add("Tarjeta de crédito");
            comboBoxTiposPago.Items.Add("Transferencia bancaria");
            comboBoxTiposPago.Items.Add("PayPal");
            comboBoxTiposPago.Items.Add("Efectivo");

            // Suscribir el evento SelectionChanged del dataGridView1
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;

        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            // Cerrar el formulario actual
            this.Close();

            // Mostrar el Form1
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // Habilitar los campos de texto
            txtbEmpresa.Enabled = true;
            txtbContacto.Enabled = true;
            txtbDireccion.Enabled = true;
            txtbTelefono.Enabled = true;
            txtbCorreo.Enabled = true;
            comboBoxTiposPago.Enabled = true;

            // Limpiar los campos de texto
            txtbEmpresa.Clear();
            txtbContacto.Clear();
            txtbDireccion.Clear();
            txtbTelefono.Clear();
            txtbCorreo.Clear();

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            // Verificar si hay al menos una fila seleccionada en el dataGridView1
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    // Abrir la conexión a la base de datos
                    con.Open();

                    // Obtener la fila seleccionada
                    DataGridViewRow row = dataGridView1.SelectedRows[0];

                    // Comando SQL para actualizar los datos en la tabla Proveedor
                    SqlCommand cmd = new SqlCommand("UPDATE Proveedor SET Nombre_Empresa = @NombreEmpresa, Nombre_Contacto = @NombreContacto, Direccion = @Direccion, Telefono = @Telefono, Correo = @Correo, Tipo_Pago = @TipoPago WHERE ID_Proveedor = @IDProveedor", con);

                    // Pasar los valores de los campos de texto y el ComboBox como parámetros
                    cmd.Parameters.AddWithValue("@NombreEmpresa", txtbEmpresa.Text);
                    cmd.Parameters.AddWithValue("@NombreContacto", txtbContacto.Text);
                    cmd.Parameters.AddWithValue("@Direccion", txtbDireccion.Text);
                    cmd.Parameters.AddWithValue("@Telefono", txtbTelefono.Text);
                    cmd.Parameters.AddWithValue("@Correo", txtbCorreo.Text);
                    cmd.Parameters.AddWithValue("@TipoPago", comboBoxTiposPago.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@IDProveedor", row.Cells["ID_Proveedor"].Value);

                    // Ejecutar el comando SQL
                    cmd.ExecuteNonQuery();

                    // Mostrar un mensaje de éxito
                    MessageBox.Show("Datos actualizados correctamente.");

                    // Actualizar el DataGridView con los datos actualizados
                    ActualizarDataGridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar datos: " + ex.Message);
                }
                finally
                {
                    // Cerrar la conexión a la base de datos
                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila para actualizar.");
            }
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                // Abrir la conexión a la base de datos
                con.Open();

                // Comando SQL para insertar datos en la tabla Proveedor
                SqlCommand cmd = new SqlCommand("INSERT INTO Proveedor (Nombre_Empresa, Nombre_Contacto, Direccion, Telefono, Correo, Tipo_Pago) VALUES (@NombreEmpresa, @NombreContacto, @Direccion, @Telefono, @Correo, @TipoPago)", con);

                // Pasar los valores de los campos de texto y el ComboBox como parámetros
                cmd.Parameters.AddWithValue("@NombreEmpresa", txtbEmpresa.Text);
                cmd.Parameters.AddWithValue("@NombreContacto", txtbContacto.Text);
                cmd.Parameters.AddWithValue("@Direccion", txtbDireccion.Text);
                cmd.Parameters.AddWithValue("@Telefono", txtbTelefono.Text);
                cmd.Parameters.AddWithValue("@Correo", txtbCorreo.Text);
                cmd.Parameters.AddWithValue("@TipoPago", comboBoxTiposPago.SelectedItem.ToString());

                // Ejecutar el comando SQL
                cmd.ExecuteNonQuery();

                // Mostrar un mensaje de éxito
                MessageBox.Show("Datos insertados correctamente.");

                // Actualizar el DataGridView con los nuevos datos
                ActualizarDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar datos: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                con.Close();
            }
        }
        private void ActualizarDataGridView()
        {
            // Consulta SQL para seleccionar todos los datos de la tabla Proveedor
            string query = "SELECT * FROM Proveedor";

            // Crear un adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);

            // Crear un DataSet para almacenar los datos
            DataSet ds = new DataSet();

            // Llenar el DataSet con los datos de la tabla Proveedor
            adapter.Fill(ds, "Proveedor");

            // Asignar el DataSet como origen de datos del DataGridView
            dataGridView1.DataSource = ds.Tables["Proveedor"];
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Verificar si hay al menos una fila seleccionada en el dataGridView1
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    // Abrir la conexión a la base de datos
                    con.Open();

                    // Obtener el ID del proveedor seleccionado en el dataGridView1
                    int idProveedor = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID_Proveedor"].Value);

                    // Comando SQL para eliminar la fila correspondiente de la base de datos
                    SqlCommand cmd = new SqlCommand("DELETE FROM Proveedor WHERE ID_Proveedor = @IDProveedor", con);
                    cmd.Parameters.AddWithValue("@IDProveedor", idProveedor);

                    // Ejecutar el comando SQL
                    cmd.ExecuteNonQuery();

                    // Eliminar la fila seleccionada del dataGridView1
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);

                    // Mostrar un mensaje de éxito
                    MessageBox.Show("Proveedor eliminado correctamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar proveedor: " + ex.Message);
                }
                finally
                {
                    // Cerrar la conexión a la base de datos
                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila para eliminar.");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el tipo de pago seleccionado
            string tipoPago = comboBoxTiposPago.SelectedItem.ToString();

            // Hacer algo con el tipo de pago seleccionado, como mostrarlo en un MessageBox
            MessageBox.Show("Tipo de pago seleccionado: " + tipoPago);
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // Verificar si hay al menos una fila seleccionada en el dataGridView1
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Obtener la fila seleccionada
                DataGridViewRow row = dataGridView1.SelectedRows[0];

                // Mostrar los datos de la fila seleccionada en los campos de texto
                txtbEmpresa.Text = row.Cells["Nombre_Empresa"].Value.ToString();
                txtbContacto.Text = row.Cells["Nombre_Contacto"].Value.ToString();
                txtbDireccion.Text = row.Cells["Direccion"].Value.ToString();
                txtbTelefono.Text = row.Cells["Telefono"].Value.ToString();
                txtbCorreo.Text = row.Cells["Correo"].Value.ToString();
                comboBoxTiposPago.SelectedItem = row.Cells["Tipo_Pago"].Value.ToString();
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            // Limpiar los TextBox estableciendo su texto en una cadena vacía
            txtbEmpresa.Text = "";
            txtbContacto.Text = "";
            txtbDireccion.Text = "";
            txtbTelefono.Text = "";
            txtbCorreo.Text = "";
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Archivos de Excel|*.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Crear una aplicación de Excel
                        Excel.Application excelApp = new Excel.Application();
                        excelApp.Visible = false;

                        // Crear un nuevo libro de Excel
                        Workbook wb = excelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                        Worksheet ws = (Worksheet)wb.Worksheets[1];

                        // Copiar los encabezados de las columnas
                        for (int i = 1; i <= dataGridView1.Columns.Count; i++)
                        {
                            ws.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                        }

                        // Copiar los datos del DataGridView a Excel
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            for (int j = 0; j < dataGridView1.Columns.Count; j++)
                            {
                                // Verificar si el valor de la celda no es null antes de acceder a él
                                if (dataGridView1.Rows[i].Cells[j].Value != null)
                                {
                                    ws.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                                }
                                else
                                {
                                    ws.Cells[i + 2, j + 1] = "";
                                }
                            }
                        }


                        // Guardar el libro de Excel en el archivo seleccionado
                        wb.SaveAs(sfd.FileName);
                        wb.Close();

                        // Cerrar la aplicación de Excel
                        excelApp.Quit();

                        MessageBox.Show("Exportación exitosa.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al exportar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            try
            {
                // Abrir la conexión a la base de datos
                con.Open();

                // Llamar al método ActualizarDataGridView para mostrar los datos en el DataGridView
                ActualizarDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar datos: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                con.Close();
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            // boton cerrar
            this.Close();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            // boton minimizar
            // Minimizar el formulario
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
    

