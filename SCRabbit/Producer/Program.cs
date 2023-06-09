using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Este es el SENDER-MENSAJERO

            // Crear Conexion
            var Fabrica = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "sa",
                Password = "SAsa123$",
            };

            // Usar la conexion
            using (var objConexion = Fabrica.CreateConnection())
            using (var objCanal = objConexion.CreateModel())
            {
                objCanal.QueueDeclare("ColaUno", false, false, false, null);
                var Mensaje = "Mensaje de bienvenida";
                // Convertir mensaje a una coleccion de bytes
                var Cuerpo = Encoding.UTF8.GetBytes(Mensaje);
                objCanal.BasicPublish("", "ColaUno", null, Cuerpo);
                Console.WriteLine("Mensaje enviado");
            }
            Console.ReadKey();
        }
    }
}