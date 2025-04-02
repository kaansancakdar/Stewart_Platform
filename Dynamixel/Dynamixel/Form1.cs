using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using dynamixel_sdk;

namespace Dynamixel
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }


        // Protocol version
        public const int PROTOCOL_VERSION = 2;                   // See which protocol version is used in the Dynamixel

        // Default setting
        public const int DXL_ID = 1;                   // Dynamixel ID: 1
        public const int BAUDRATE = 1000000;
        public const string DEVICENAME = "COM3";              // Check which port is being used on your controller
                                                              // ex) Windows: "COM1"   Linux: "/dev/ttyUSB0" Mac: "/dev/tty.usbserial-*"
        
        public const int COMM_SUCCESS = 0;                   // Communication Success result value
        public const int COMM_TX_FAIL = -1001;               // Communication Tx Failed

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Initialize PortHandler Structs
            // Set the port path
            // Get methods and members of PortHandlerLinux or PortHandlerWindows
            int port_num = dynamixel.portHandler(DEVICENAME);
        }
    }
}
