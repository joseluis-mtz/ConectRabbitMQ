using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Es el RECEIVER-RECEPTOR
            
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
                var Consumer = new EventingBasicConsumer(objCanal);
                Consumer.Received += (modelo, ea) => {
                    // ea -> Representacion del objeto mensaje que viene dentro del Queue
                    var Cuerpo = ea.Body.ToArray();
                    // Convertir el mensaje a texto
                    var Mensaje = Encoding.UTF8.GetString(Cuerpo);
                    Console.WriteLine("Mensaje recibido {0}", Mensaje);
                };
                // El true lo consume para que no quede en cola
                objCanal.BasicConsume("ColaUno", true, Consumer);
            }
            Console.ReadKey();
        }
    }
}
