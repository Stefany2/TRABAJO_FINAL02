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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        // Cadena de conexión a tu base de datos SQL Server
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\stefany;Initial Catalog=almacen;Integrated Security=True");
        
        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Verificar si se ha seleccionado una fila en el DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    // Obtener el ID del elemento seleccionado en el DataGridView
                    int idProducto = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID_Productos"].Value);

                    // Abrir la conexión a la base de datos
                    con.Open();

                    // Ejecutar una consulta SQL para eliminar el registro correspondiente en la base de datos
                    SqlCommand cmd = new SqlCommand("DELETE FROM Productos WHERE ID_Productos = @ID", con);
                    cmd.Parameters.AddWithValue("@ID", idProducto);
                    cmd.ExecuteNonQuery();

                    // Cerrar la conexión a la base de datos
                    con.Close();

                    // Eliminar la fila seleccionada del DataGridView
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);

                    // Mostrar un mensaje de éxito
                    MessageBox.Show("Producto eliminado correctamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el producto: " + ex.Message);
                }
                finally
                {
                    // Cerrar la conexión a la base de datos si está abierta
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila para eliminar.");
            }
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
            // Rellenar el ComboBox con los datos deseados
            comboBox1.Items.Add("Kilo");
            comboBox1.Items.Add("Gramo");
            comboBox1.Items.Add("Saco");
            comboBox1.Items.Add("Unidad");
            comboBox1.Items.Add("Paquete");
            comboBox1.Items.Add("Litro");

           
        }
       
        private void btnMenu_Click(object sender, EventArgs e)
        {
            // Cerrar el formulario actual
            this.Close();

            // Mostrar el Form3
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // Habilitar todos los TextBox excepto txtbCodigo
            txtbNombre.Enabled = true;
            txtbDescripcion.Enabled = true;
            txtbCategoria.Enabled = true;
            txtbPrecio.Enabled = true;
            txtbMayor.Enabled = true;
            txtbStock.Enabled = true;
            dateTimePicker1.Enabled = true;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            // Verificar si se ha seleccionado una fila en el DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Obtener los datos de la fila seleccionada
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                txtbNombre.Text = row.Cells["Nombre_Producto"].Value.ToString();
                txtbDescripcion.Text = row.Cells["Descripcion_Producto"].Value.ToString();
                txtbCategoria.Text = row.Cells["Categoria"].Value.ToString();
                txtbPrecio.Text = row.Cells["Precio_Minudeo"].Value.ToString();
                txtbMayor.Text = row.Cells["Precio_Mayoreo"].Value.ToString();
                txtbStock.Text = row.Cells["Stock"].Value.ToString();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila antes de hacer clic en este botón.");
            }
        }
              
        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                // Abrir la conexión a la base de datos
                con.Open();

                // Crear una consulta SQL para insertar datos en la tabla Productos
                string query = @"INSERT INTO Productos (Nombre_Producto, Descripcion_Producto, Categoria, Unidad, Precio_Minudeo, Precio_Mayoreo, Stock, Fecha_Vencimiento, Fecha_Ingreso)
                         VALUES (@Nombre, @Descripcion, @Categoria, @Unidad, @Precio_Minudeo, @Precio_Mayoreo, @Stock, @FechaVencimiento, @FechaIngreso)";

                // Crear un comando SQL
                SqlCommand cmd = new SqlCommand(query, con);

                // Pasar los valores de los controles al comando SQL como parámetros
                cmd.Parameters.AddWithValue("@Nombre", txtbNombre.Text);
                cmd.Parameters.AddWithValue("@Descripcion", txtbDescripcion.Text);
                cmd.Parameters.AddWithValue("@Categoria", txtbCategoria.Text);
                cmd.Parameters.AddWithValue("@Unidad", comboBox1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Precio_Minudeo", Convert.ToDecimal(txtbPrecio.Text));
                cmd.Parameters.AddWithValue("@Precio_Mayoreo", Convert.ToDecimal(txtbMayor.Text));
                cmd.Parameters.AddWithValue("@Stock", Convert.ToInt32(txtbStock.Text));
                cmd.Parameters.AddWithValue("@FechaVencimiento", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@FechaIngreso", DateTime.Now);

                // Ejecutar el comando SQL
                cmd.ExecuteNonQuery();

                // Cerrar la conexión a la base de datos
                con.Close();

                // Mostrar un mensaje de éxito
                MessageBox.Show("Datos insertados correctamente.");

                // Actualizar el DataGridView con los nuevos datos de la tabla Productos
                ActualizarDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar datos: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos si está abierta
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }


        private void ActualizarDataGridView()
        {
            // Consulta SQL para seleccionar todos los datos de la tabla Productos
            string query = "SELECT * FROM Productos";

            // Crear un adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);

            // Crear un DataSet para almacenar los datos
            DataSet ds = new DataSet();

            // Llenar el DataSet con los datos de la tabla Productos
            adapter.Fill(ds, "Productos");

            // Asignar el DataSet como origen de datos del DataGridView
            dataGridView1.DataSource = ds.Tables["Productos"];
        }
    

        private void txtbCodigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtbNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtbDescripcion_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtbCategoria_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // Verificar si hay al menos una fila seleccionada en el dataGridView1
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Obtener la fila seleccionada
                DataGridViewRow row = dataGridView1.SelectedRows[0];

                // Mostrar los datos de la fila seleccionada en los TextBox correspondientes
                txtbNombre.Text = row.Cells["Nombre_Producto"].Value.ToString();
                txtbDescripcion.Text = row.Cells["Descripcion_Producto"].Value.ToString();
                txtbCategoria.Text = row.Cells["Categoria"].Value.ToString();
                txtbMayor.Text = row.Cells["Precio_Mayoreo"].Value.ToString(); // Precio_Mayoreo o Precio_Minudeo, dependiendo de tu lógica
                txtbPrecio.Text = row.Cells["Precio_Minudeo"].Value.ToString(); // Precio_Mayoreo o Precio_Minudeo, dependiendo de tu lógica
                txtbStock.Text = row.Cells["Stock"].Value.ToString();

                // Buscar la unidad correspondiente en la fila seleccionada
                string unidad = row.Cells["Unidad"].Value.ToString();

                // Seleccionar la unidad correspondiente en el ComboBox
                if (comboBox1.Items.Contains(unidad))
                {
                    comboBox1.SelectedItem = unidad;
                }
                else
                {
                    // Si la unidad no está en la lista, limpiar la selección del ComboBox
                    comboBox1.SelectedIndex = -1;
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si hay al menos una fila seleccionada en el dataGridView1
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Obtener la fila seleccionada
                DataGridViewRow row = dataGridView1.SelectedRows[0];


                // Mostrar los datos de la fila seleccionada en los TextBox correspondientes
                txtbNombre.Text = row.Cells["Nombre_Producto"].Value.ToString();
                txtbDescripcion.Text = row.Cells["Descripcion_Producto"].Value.ToString();
                txtbCategoria.Text = row.Cells["Categoria"].Value.ToString();
                txtbMayor.Text = row.Cells["Precio_Mayoreo"].Value.ToString(); // Precio_Mayoreo o Precio_Minudeo, dependiendo de tu lógica
                txtbPrecio.Text = row.Cells["Precio_Minudeo"].Value.ToString(); // Precio_Mayoreo o Precio_Minudeo, dependiendo de tu lógica
                txtbStock.Text = row.Cells["Stock"].Value.ToString();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Capturar el valor seleccionado en el ComboBox
            string unidadSeleccionada = comboBox1.SelectedItem.ToString();

            // Hacer algo con el valor seleccionado, como mostrarlo en un MessageBox
            MessageBox.Show("Unidad seleccionada: " + unidadSeleccionada);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            // Limpiar los TextBox
            txtbNombre.Clear();
            txtbDescripcion.Clear();
            txtbCategoria.Clear();
            txtbPrecio.Clear();
            txtbStock.Clear();
            txtbMayor.Clear();
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            try
            {
                // Abrir la conexión a la base de datos
                con.Open();

                // Comando SQL para seleccionar todos los datos de la tabla Productos
                string query = "SELECT * FROM Productos";

                // Crear un adaptador de datos para ejecutar el comando SQL y llenar un DataSet
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);

                // Crear un DataSet para almacenar los datos
                DataSet ds = new DataSet();

                // Llenar el DataSet con los datos de la tabla Productos
                adapter.Fill(ds, "Productos");

                // Asignar el DataSet como origen de datos del DataGridView
                dataGridView1.DataSource = ds.Tables["Productos"];
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Verificar si se ha seleccionado una fila en el DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    // Obtener el ID del elemento seleccionado en el DataGridView
                    int idProducto = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID_Productos"].Value);

                    // Abrir la conexión a la base de datos
                    con.Open();

                    // Comando SQL para actualizar los datos en la base de datos
                    SqlCommand cmd = new SqlCommand("UPDATE Productos SET Nombre_Producto = @nombre, Descripcion_Producto = @descripcion, Categoria = @categoria, Precio_Minudeo = @precioMinudeo, Precio_Mayoreo = @precioMayoreo, Stock = @stock WHERE ID_Productos = @id", con);
                    cmd.Parameters.AddWithValue("@nombre", txtbNombre.Text);
                    cmd.Parameters.AddWithValue("@descripcion", txtbDescripcion.Text);
                    cmd.Parameters.AddWithValue("@categoria", txtbCategoria.Text);
                    cmd.Parameters.AddWithValue("@precioMinudeo", Convert.ToDecimal(txtbPrecio.Text));
                    cmd.Parameters.AddWithValue("@precioMayoreo", Convert.ToDecimal(txtbMayor.Text));
                    cmd.Parameters.AddWithValue("@stock", Convert.ToInt32(txtbStock.Text));
                    cmd.Parameters.AddWithValue("@id", idProducto);

                    // Ejecutar el comando SQL
                    cmd.ExecuteNonQuery();

                    // Cerrar la conexión a la base de datos
                    con.Close();

                    // Mostrar un mensaje de éxito
                    MessageBox.Show("Datos guardados correctamente.");

                    // Actualizar el DataGridView con los datos actualizados
                    ActualizarDataGridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar datos: " + ex.Message);
                }
                finally
                {
                    // Cerrar la conexión a la base de datos si está abierta
                    if (con.State == System.Data.ConnectionState.Open)
                        con.Close();
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila antes de hacer clic en este botón.");
            }
        }
    }
}
    

