using IndiceMC.CONEXION;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IndiceMC
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        int peso_maximo;
        private void BtnCalular_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtaltura.Text) && !string.IsNullOrEmpty(txtpeso.Text))
            {
                calcularImc();
                Insertar_imc();
                
            }
            else
            {
                DisplayAlert("Campos vacios","LLenar los campos obligatirio * ","OK");
            }
        }
        private void Insertar_imc()
        {
            try
            {
                CONEXIONMAESTRA.Abrir();
                SqlCommand cmd = new SqlCommand("Insertar_imc", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Altura", txtaltura.Text);
                cmd.Parameters.AddWithValue("@Peso", txtpeso.Text);
                cmd.Parameters.AddWithValue("@Resultado", txtresultado.Text);
                cmd.ExecuteNonQuery();
                CONEXIONMAESTRA.Cerrar();
            }
            catch (Exception ex)
            {
                DisplayAlert("Error en BD", ex.Message, "OK");

            }


        }
        private void calcularImc()
        {
            double altura = Convert.ToDouble(txtaltura.Text);
            double peso = Convert.ToDouble(txtpeso.Text);
            double resultado = peso / (altura * altura);
            txtresultado.Text = resultado.ToString();
            string mensaje = "";
            if (resultado < 18.5)
            {
                mensaje = "tiene bajo peso";
            }
            else if (resultado >= 18.5 && resultado <= 24.9)
            {
                mensaje = "su peso es normal";
            }
            else if (resultado >= 25 && resultado <= 29.9)
            {
                mensaje = "tiene sobrepeso";
            }
            else
            {
                mensaje = "cuidado tiene obesidad";
            }
            DisplayAlert("resultado", mensaje, "OK");
        }
        private void BtnObtenerDatos_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                CONEXIONMAESTRA.Abrir();
                SqlCommand cmd = new SqlCommand("mostrar_peso_mas_alto", CONEXIONMAESTRA.conectar);
                peso_maximo = Convert.ToInt32(cmd.ExecuteScalar());
                CONEXIONMAESTRA.Cerrar();
                DisplayAlert("peso maximo", Convert.ToString(peso_maximo), "ok");
            }
            catch (Exception ex)
            {
                DisplayAlert(" peso maximo", ex.Message, "ok");
            }
        }
    }

}
