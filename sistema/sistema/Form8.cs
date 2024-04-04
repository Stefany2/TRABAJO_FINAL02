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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace sistema
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
           
        }

        // Cadena de conexión a tu base de datos SQL Server
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\stefany;Initial Catalog=almacen;Integrated Security=True");

        private void label4_Click(object sender, EventArgs e)
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

        private void Form8_Load(object sender, EventArgs e)
        {
            // Llenar el DataGridView al cargar el formulario
            FillDataGridView();
        }
        private void FillDataGridView()
        {
            try
            {
                // Verificar si la conexión está cerrada antes de abrirla
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                // Consulta SQL para obtener los datos de la tabla Entrada junto con los nombres de productos, proveedores y devoluciones
                string query = @"SELECT e.ID_Entrada, p.Nombre_Producto AS Nombre_Producto, pr.Nombre_Contacto AS Nombre_Contacto, 
                                     d.Devolucion_Proveedor, e.Cantidad_Recibida, e.Fecha_Entrada 
                                 FROM Entrada e 
                                 INNER JOIN Productos p ON e.ID_Productos = p.ID_Productos 
                                 INNER JOIN Proveedor pr ON e.ID_Proveedor = pr.ID_Proveedor 
                                 LEFT JOIN Devolucion d ON e.ID_Devolucion = d.ID_Devolucion";

                // Crear un SqlDataAdapter
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);

                // Crear un DataTable para almacenar los datos
                DataTable dt = new DataTable();

                // Llenar el DataTable con los datos del SqlDataAdapter
                adapter.Fill(dt);

                // Asignar el DataTable al DataGridView
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al llenar DataGridView: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión
                con.Close();
            }
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                // Abrir la conexión
                con.Open();

                // Consulta SQL para insertar los datos en la tabla Entrada
                string query = @"INSERT INTO Entrada (ID_Productos, Cantidad_Recibida, Fecha_Entrada, ID_Devolucion, ID_Proveedor) 
                                 VALUES (@ID_Productos, @Cantidad_Recibida, @Fecha_Entrada, @ID_Devolucion, @ID_Proveedor)";

                // Crear un SqlCommand
                SqlCommand command = new SqlCommand(query, con);

                // Agregar parámetros
                command.Parameters.AddWithValue("@ID_Productos", textBox2.Text);
                command.Parameters.AddWithValue("@Cantidad_Recibida", txtbCantidad.Text);
                command.Parameters.AddWithValue("@Fecha_Entrada", DateTime.Now); // Usar la fecha y hora actual
                command.Parameters.AddWithValue("@ID_Devolucion", txtbDevolucion.Text);
                command.Parameters.AddWithValue("@ID_Proveedor", textBox3.Text);

                // Ejecutar la consulta
                command.ExecuteNonQuery();

                // Limpiar los TextBox después de la inserción
                ClearTextBoxes();

                // Actualizar el DataGridView para mostrar los datos recién insertados
                FillDataGridView();

                MessageBox.Show("Datos insertados correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar datos: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión
                con.Close();
            }
        }

        private void ClearTextBoxes()
        {
            // Limpiar los TextBox
            txtbProducto.Clear();
            txtbCantidad.Clear();
            textBox2.Clear();
            textBox3.Clear();
            txtbDevolucion.Clear();
            txtbEmpresa.Clear();
        }
    
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

       

       
       
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // Habilitar los TextBox para ingresar nuevos datos
            
            txtbProducto.Enabled = true;
            txtbDevolucion.Enabled = true;
            txtbEmpresa.Enabled = true;
            txtbCantidad.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            // boton minimizar
            // Minimizar el formulario
            this.WindowState = FormWindowState.Minimized;
        }
       
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Obtener el índice de la fila seleccionada
            selectedRowIndex = e.RowIndex;
        }
       

         
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            // Verificar si se ha seleccionado una fila
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Obtener los datos de la fila seleccionada y llenar los TextBox correspondientes
                txtbProducto.Text = dataGridView1.SelectedRows[0].Cells["Nombre_Producto"].Value.ToString();
                txtbCantidad.Text = dataGridView1.SelectedRows[0].Cells["Cantidad_Recibida"].Value.ToString();
                txtbDevolucion.Text = dataGridView1.SelectedRows[0].Cells["Devolucion_Proveedor"].Value.ToString();
                txtbEmpresa.Text = dataGridView1.SelectedRows[0].Cells["Nombre_Contacto"].Value.ToString();
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una fila para actualizar.");
            }
        }
        private int selectedRowIndex = -1; // Variable para almacenar el índice de la fila seleccionada
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Verificar si se ha seleccionado una fila
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Obtener el identificador único (ID_Entrada) de la fila seleccionada
                int idEntrada = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID_Entrada"].Value);

                // Eliminar la fila seleccionada de la base de datos
                EliminarFilaDeBaseDeDatos(idEntrada);

                // Eliminar la fila seleccionada del DataGridView
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);

                MessageBox.Show("Entrada eliminada correctamente.");
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una fila para eliminar.");
            }
        }

        private void EliminarFilaDeBaseDeDatos(int idEntrada)
        {
            try
            {
                // Abrir la conexión
                con.Open();

                // Consulta SQL para eliminar la entrada
                string query = "DELETE FROM Entrada WHERE ID_Entrada = @ID_Entrada";

                // Crear un SqlCommand
                SqlCommand command = new SqlCommand(query, con);

                // Agregar parámetro
                command.Parameters.AddWithValue("@ID_Entrada", idEntrada);

                // Ejecutar la consulta
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar entrada de la base de datos: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión
                con.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Verificar si se ha seleccionado una fila
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Obtener el ID_Entrada de la fila seleccionada
                int idEntrada = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID_Entrada"].Value);

                // Actualizar los datos en la base de datos con los valores de los TextBox
                ActualizarDatosEnBaseDeDatos(idEntrada);

                // Actualizar el DataGridView para reflejar los cambios
                FillDataGridView();

                MessageBox.Show("Cambios guardados correctamente.");
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una fila para guardar los cambios.");
            }
        }

        private void ActualizarDatosEnBaseDeDatos(int idEntrada)
        {
            try
            {
                // Abrir la conexión
                con.Open();

                // Consulta SQL para actualizar los datos en la tabla Entrada
                string query = @"UPDATE Entrada 
                         SET Nombre_Producto = @Nombre_Producto, 
                             Cantidad_Recibida = @Cantidad_Recibida, 
                             Devolucion_Proveedor = @Devolucion_Proveedor, 
                             ID_Proveedor = @ID_Proveedor
                         WHERE ID_Entrada = @ID_Entrada";

                // Crear un SqlCommand
                SqlCommand command = new SqlCommand(query, con);

                // Agregar parámetros
                command.Parameters.AddWithValue("@Nombre_Producto", txtbProducto.Text);
                command.Parameters.AddWithValue("@Cantidad_Recibida", txtbCantidad.Text);
                command.Parameters.AddWithValue("@Devolucion_Proveedor", txtbDevolucion.Text);
                command.Parameters.AddWithValue("@ID_Proveedor", txtbEmpresa.Text);
                command.Parameters.AddWithValue("@ID_Entrada", idEntrada);

                // Ejecutar la consulta
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar cambios en la base de datos: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión
                con.Close();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            // boton para limpiar los textBox
            txtbProducto.Clear();
            txtbCantidad.Clear();
            textBox2.Clear();
            textBox3.Clear();
            txtbDevolucion.Clear();
            txtbEmpresa.Clear();
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            // Llamar al método FillDataGridView para mostrar todos los datos en el DataGridView
            FillDataGridView();
        }
    }
}
