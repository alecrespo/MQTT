using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;

namespace MQTT_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            MqttBroker broker = new MqttBroker();
            broker.Start();

            Console.ReadLine();

            broker.Stop();
        }
    }
}
