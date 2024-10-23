using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CalculadoraPrj1
{
    public partial class Form1 : Form
    {
        // Proyecto 1
        // Cesar Santiago
        // Sara Lee

        // Calculadora


        // 1. Instanciamos variables a utilizar
        double primerNumero = 0;
        string operacion = "";
        bool operacionSeleccionada = false;
        private SqlConnection connection;

        public Form1()
        {
            InitializeComponent();
            ConectarBaseDeDatos();
        }

        private void ConectarBaseDeDatos()
        {
            try
            {
                // Cadena de conexión a la base de datos (ajusta el Data Source según tu configuración)
                string connectionString = @"Server=.\SQLEXPRESS;Database=calculadora;Trusted_Connection=True;";
                connection = new SqlConnection(connectionString);
                connection.Open(); // Conexión abierta al iniciar el programa

                // Mensaje opcional de éxito
                MessageBox.Show("Conexión a la base de datos establecida.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                MessageBox.Show("Error al conectar con la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

            // 2. Creamos los siguientes metodos para usar en multiples botones
            // a. Metodo AgregarNumero para que agregue numero al textbox cada vez que usamos un boton numerico
            // b. Metodo EsNumeroValido para hacer un constraint que no permita ingresar valores no numericos
            // c. Metodo SeleccionarOperacion para que realice la distincion entre el primer numero a calcular y el segundo antes y despues del operador
            private void AgregarNumero(string numero)
        {
           
            if (operacionSeleccionada)
            {
                textBox1.Clear();
                operacionSeleccionada = false;
            }

            // Concatenamos el número en el TextBox
            textBox1.Text += numero;
        }

        private bool EsNumeroValido(string texto)
        {
            double numero;
            return double.TryParse(texto, out numero);
        }

        private void SeleccionarOperacion(string operacionSeleccionada)
        {
            if (EsNumeroValido(textBox1.Text))
            {
                primerNumero = Convert.ToDouble(textBox1.Text);
                operacion = operacionSeleccionada;
                this.operacionSeleccionada = true;
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un número válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // 3. Creamos los botones del 0 al 9 para ingresar numeros a la calculadora

        private void button1_Click(object sender, EventArgs e)
        {
            Button boton = (Button)sender;
            AgregarNumero(boton.Text);
        }

        private void btn1_Click(object sender, EventArgs e)
        {

            Button boton = (Button)sender;
            AgregarNumero(boton.Text);
        }

        private void btn2_Click(object sender, EventArgs e)
        {

            Button boton = (Button)sender;
            AgregarNumero(boton.Text);
        }

        private void btn3_Click(object sender, EventArgs e)
        {

            Button boton = (Button)sender;
            AgregarNumero(boton.Text);
        }

        private void btn4_Click(object sender, EventArgs e)
        {

            Button boton = (Button)sender;
            AgregarNumero(boton.Text);
        }

        private void btn5_Click(object sender, EventArgs e)
        {

            Button boton = (Button)sender;
            AgregarNumero(boton.Text);
        }

        private void btn6_Click(object sender, EventArgs e)
        {

            Button boton = (Button)sender;
            AgregarNumero(boton.Text);
        }

        private void btn7_Click(object sender, EventArgs e)
        {

            Button boton = (Button)sender;
            AgregarNumero(boton.Text);
        }

        private void btn8_Click(object sender, EventArgs e)
        {

            Button boton = (Button)sender;
            AgregarNumero(boton.Text);
        }

        private void btn9_Click(object sender, EventArgs e)
        {

            Button boton = (Button)sender;
            AgregarNumero(boton.Text);
        }

        // 4. Creamos un boton de Punto decimal para numeros decimales
        private void btnPunto_Click_1(object sender, EventArgs e)
        {
            if (!textBox1.Text.Contains("."))
            {
                if (textBox1.Text == "")
                {
                    textBox1.Text = "0.";
                }
                else
                {
                    textBox1.Text += ".";
                }
            }
        }

        // 5. Creamos un boton de = para calcular resultado

        private void btnIgual_Click_1(object sender, EventArgs e)
        {
            if (EsNumeroValido(textBox1.Text))
            {
                double segundoNumero = Convert.ToDouble(textBox1.Text);
                double resultado = 0;

                switch (operacion)
                {
                    case "+":
                        resultado = primerNumero + segundoNumero;
                        break;
                    case "-":
                        resultado = primerNumero - segundoNumero;
                        break;
                    case "*":
                        resultado = primerNumero * segundoNumero;
                        break;
                    case "/":
                        if (segundoNumero != 0)
                            resultado = primerNumero / segundoNumero;
                        else
                            MessageBox.Show("Error: División por cero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                textBox1.Text = resultado.ToString();

                GuardarOperacionEnBaseDeDatos(primerNumero, segundoNumero, operacion, resultado);
            
        }
            else
            {
                MessageBox.Show("Por favor, ingrese un número válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // 6. Creamos un boton de limpiar la calculadora y anular operaciones realizadas
        private void btnLimpiar_Click_1(object sender, EventArgs e)
        {
            textBox1.Clear();
            primerNumero = 0;
            operacion = "";
        }

        // 7. Creamos un boton para borrar el ultimo numero ingresado
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
            }
        }

        // 8. Creamos un boton que cambie el signo de positivo a negativo y viceversa
        private void btnPosNeg_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.StartsWith("-"))
            {
                textBox1.Text = textBox1.Text.Substring(1); // Remover el signo negativo
            }
            else
            {
                textBox1.Text = "-" + textBox1.Text; // Agregar el signo negativo
            }
        }


        // 9. Creamos los botones de operaciones suma, resta, mult, div llamando el metodo de seleccionarOperacion
        private void btnSumar_Click(object sender, EventArgs e)
        {

            Button boton = (Button)sender;
            SeleccionarOperacion(boton.Text);
        }

        private void btnRestar_Click(object sender, EventArgs e)
        {
            Button boton = (Button)sender;
            SeleccionarOperacion(boton.Text);
        }

        private void btnMultiplicar_Click(object sender, EventArgs e)
        {
            Button boton = (Button)sender;
            SeleccionarOperacion(boton.Text);
        }

        private void btnDividir_Click(object sender, EventArgs e)
        {
            Button boton = (Button)sender;
            SeleccionarOperacion(boton.Text);
        }

        // Adicionalmente creamos botones para calcular la raiz cuadrada y el cuadrado de un numero
        private void btnRaiz_Click(object sender, EventArgs e)
        {
            if (EsNumeroValido(textBox1.Text))
            {
                double numero = Convert.ToDouble(textBox1.Text);
                double resultado = Math.Sqrt(numero); // Calcular la raíz cuadrada

                textBox1.Text = resultado.ToString();
                GuardarOperacionEnBaseDeDatos(numero, 0, "√", resultado); // Guardar en la base de datos
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un número válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnCuadrado_Click_1(object sender, EventArgs e)
        {
            if (EsNumeroValido(textBox1.Text))
            {
                double numero = Convert.ToDouble(textBox1.Text);
                double resultado = Math.Pow(numero, 2); // Elevar al cuadrado

                textBox1.Text = resultado.ToString();
                GuardarOperacionEnBaseDeDatos(numero, 0, "^2", resultado); // Guardar en la base de datos
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un número válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Adicionalmente creamos un boton para calcular el factorial de un numero
        private void btnFactorial_Click(object sender, EventArgs e)
        {
            if (EsNumeroValido(textBox1.Text))
            {
                int numero = Convert.ToInt32(textBox1.Text);

                if (numero >= 0)
                {
                    double resultado = Factorial(numero); // Calcular el factorial

                    textBox1.Text = resultado.ToString();
                    GuardarOperacionEnBaseDeDatos(numero, 0, "!", resultado); // Guardar en la base de datos
                }
                else
                {
                    MessageBox.Show("El factorial no está definido para números negativos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un número válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Este metodo calcula la factorial de un numero, es utilizado en el evento btnFactorial al hacer click en el boton.
        private double Factorial(int numero)
        {
            double resultado = 1;

            for (int i = 1; i <= numero; i++)
            {
                resultado *= i;
            }

            return resultado;
        }

        private void GuardarOperacionEnBaseDeDatos(double primerNumero, double segundoNumero, string operacion, double resultado)
        {
            try
            {
                // Comando SQL para insertar los valores en la tabla
                string query = "INSERT INTO calculadora_log (primernumero, segundonumero, operador, resultado) VALUES (@primerNumero, @segundoNumero, @operacion, @resultado)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Asignar los parámetros con los valores calculados
                    command.Parameters.AddWithValue("@primerNumero", primerNumero);
                    command.Parameters.AddWithValue("@segundoNumero", segundoNumero);
                    command.Parameters.AddWithValue("@operacion", operacion);
                    command.Parameters.AddWithValue("@resultado", resultado);

                    // Ejecutar el comando
                    command.ExecuteNonQuery();
                }

                // Mensaje opcional de éxito
                MessageBox.Show("Operación guardada correctamente en la base de datos.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                MessageBox.Show("Error al guardar la operación en la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
