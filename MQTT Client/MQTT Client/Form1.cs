using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace MQTT_Client
{
    public partial class Form1 : Form
    {
        static Form1 frm;
        MqttClient client;
        public Form1()
        {
            frm = this;
            InitializeComponent();
        }

        private void txtHost_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {

            // create client instance 
            client = new MqttClient(txtHost.Text);

            // register to message received 
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            string clientId = Guid.NewGuid().ToString();
            client.Connect(clientId);

            // subscribe to the topic "/home/temperature" with QoS 2 
            client.Subscribe(new string[] { string.Format("/home/{0}",txtTopic.Text) }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

            btnConnect.Enabled = false;
            btnSend.Enabled = true;
        }
        static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            // handle message received 
            frm.txtReceive.BeginInvoke((Action)(() => frm.txtReceive.Text = System.Text.UTF8Encoding.UTF8.GetString(e.Message)));

        }
       
        private void btnSend_Click(object sender, EventArgs e)
        {
            // create client instance 

            string strValue = Convert.ToString(txtMessage.Text);

            // publish a message on "/home/temperature" topic with QoS 2 
            client.Publish(string.Format("/home/{0}", txtTopic.Text), Encoding.UTF8.GetBytes(strValue), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);

        }

        private void txtReceive_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
