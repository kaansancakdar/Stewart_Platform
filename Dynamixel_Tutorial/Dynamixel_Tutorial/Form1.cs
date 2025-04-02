using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using dynamixel_sdk;
using MX106_Control_Table;

namespace Dynamixel_Tutorial
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


        }


        public const int LED_ON = 1;                   
        public const int LED_OFF = 0;
        // Protocol version
        public const int PROTOCOL_VERSION = 2;                     // See which protocol version is used in the Dynamixel

        // Default setting
        public const int DXL_ID1 = 1;                              // Dynamixel ID: 1
        public const int DXL_ID2 = 2;                              // Dynamixel ID: 2
        public const int DXL_ID3 = 3;                              // Dynamixel ID: 3
        public const int DXL_ID4 = 4;                              // Dynamixel ID: 4
        public const int DXL_ID5 = 5;                              // Dynamixel ID: 5
        public const int DXL_ID6 = 6;                              // Dynamixel ID: 6
        public const int BAUDRATE = 1000000;
        public const string DEVICENAME = "COM3";


        public const int DXL_MOVING_STATUS_THRESHOLD = 01;

        public const byte ESC_ASCII_VALUE = 0x1b;

        public const int COMM_SUCCESS = 0;                        // Communication Success result value
        public const int COMM_TX_FAIL = -1001;                    // Communication Tx Failed

        bool dxl_addparam_result = false;

        UInt16 dxl_model_number;                                 // Dynamixel model number

        public int port_num = dynamixel.portHandler(DEVICENAME);

        private void Form1_Load(object sender, EventArgs e)
        {

            // Initialize PortHandler Structs
            // Set the port path
            // Get methods and members of PortHandlerLinux or PortHandlerWindows
            

            // Initialize PacketHandler Structs
            dynamixel.packetHandler();

            int dxl_comm_result = COMM_TX_FAIL;                                   // Communication result

            byte dxl_error = 0;                                                   // Dynamixel error
            

            // Open port
            if (dynamixel.openPort(port_num))
            {
                Console.WriteLine("Succeeded to open the port!");
                textBox2.AppendText("Succeeded to open the port!\r\n");
            }
            else
            {
                Console.WriteLine("Failed to open the port!");
                Console.WriteLine("Press any key to terminate...");
                textBox2.AppendText("Failed to open the port!\r\n");
                return;
            }

            // Set port baudrate
            if (dynamixel.setBaudRate(port_num, BAUDRATE))
            {
                Console.WriteLine("Succeeded to change the baudrate!");
                textBox2.AppendText("Succeeded to change the baudrate!\r\n");
            }
            else
            {
                Console.WriteLine("Failed to change the baudrate!");
                Console.WriteLine("Press any key to terminate...");
                textBox2.AppendText("Failed to change the baudrate!\r\n");
                return;
            }

            //Change drive mode to time based and sync read from motors
            int groupwrite_num= dynamixel.groupSyncWrite(port_num, PROTOCOL_VERSION, MX106.ADDR_DRIVE_MODE, MX106.LEN_DRIVE_MODE);

            for (int i = 1;i<=6;i++)
            {

                dxl_addparam_result = dynamixel.groupSyncWriteAddParam(groupwrite_num, (byte)i, (UInt32)MX106.DRIVE_TIME_BASED, MX106.LEN_DRIVE_MODE);
                if (dxl_addparam_result != true)
                {
                    textBox2.AppendText("ID:" + i + "groupSyncWrite addparam failed \r\n");
                    return;
                }
            }

            dynamixel.groupSyncWriteTxPacket(groupwrite_num);
            dynamixel.groupSyncWriteClearParam(groupwrite_num);

            int groupread_num = dynamixel.groupSyncRead(port_num, PROTOCOL_VERSION, MX106.ADDR_DRIVE_MODE, MX106.LEN_DRIVE_MODE);

            for (int i = 1; i <= 6; i++)
            {

                dxl_addparam_result=dynamixel.groupSyncReadAddParam(groupread_num, (byte)i);
                if (dxl_addparam_result != true)
                {
                    textBox2.AppendText("ID:" + i + "groupSyncWrite addparam failed \r\n");
                    return;
                }

            }

            dynamixel.groupSyncReadTxRxPacket(groupread_num);

            for (int i = 1; i <= 6; i++)
            {
                int dxl_drive_mode = (Int32)dynamixel.groupSyncReadGetData(groupread_num, (byte)i, MX106.ADDR_DRIVE_MODE, MX106.LEN_DRIVE_MODE);
                textBox2.AppendText("ID"+i+ "Drive Mode:" + dxl_drive_mode.ToString() + "\r\n");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dynamixel.write1ByteTxRx(port_num, PROTOCOL_VERSION, DXL_ID1, MX106.ADDR_LED, LED_ON);
            textBox2.AppendText("ID " + DXL_ID1 + "LED ON" + "\r\n");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dynamixel.write1ByteTxRx(port_num, PROTOCOL_VERSION, DXL_ID1, MX106.ADDR_LED, LED_OFF);
            textBox2.AppendText("ID " + DXL_ID1 + "LED OFF" + "\r\n");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dxl_model_number = dynamixel.pingGetModelNum(port_num, PROTOCOL_VERSION, DXL_ID1);
            textBox1.Text=dxl_model_number.ToString();
            textBox2.AppendText("ID" + DXL_ID1 + "Model Number:" + dxl_model_number.ToString()+ "\r\n");
        }
        
        public uint LED_index = 0;

        private void button4_Click(object sender, EventArgs e)
        {
            
            int groupwrite_num = dynamixel.groupSyncWrite(port_num, PROTOCOL_VERSION, MX106.ADDR_LED, MX106.LEN_LED);
            dxl_addparam_result = dynamixel.groupSyncWriteAddParam(groupwrite_num, DXL_ID1, LED_index, MX106.LEN_LED);
            dxl_addparam_result = dynamixel.groupSyncWriteAddParam(groupwrite_num, DXL_ID2, LED_index, MX106.LEN_LED);
            dynamixel.groupSyncWriteTxPacket(groupwrite_num);
            
            if(LED_index == 0)
            {
                LED_index = 1;
            }
            else
            {
                LED_index = 0;
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dynamixel.write1ByteTxRx(port_num, PROTOCOL_VERSION, DXL_ID1, MX106.ADDR_LED, LED_ON);
            }
            else
            {
                dynamixel.write1ByteTxRx(port_num, PROTOCOL_VERSION, DXL_ID1, MX106.ADDR_LED, LED_OFF);
            }
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int groupwrite_num = dynamixel.groupSyncWrite(port_num, PROTOCOL_VERSION, MX106.ADDR_PROFILE_VELOCITY, MX106.LEN_PROFILE_VELOCITY);

            for (int i = 1; i <= 6; i++)
            {

                dxl_addparam_result = dynamixel.groupSyncWriteAddParam(groupwrite_num, (byte)i, (UInt32)numericUpDown7.Value, MX106.LEN_PROFILE_VELOCITY);
                if (dxl_addparam_result != true)
                {
                    textBox2.AppendText("ID: " + i + "groupSyncWrite addparam failed \r\n");
                    return;
                }
            }

            dynamixel.groupSyncWriteTxPacket(groupwrite_num);
            dynamixel.groupSyncWriteClearParam(groupwrite_num);


            int groupread_num = dynamixel.groupSyncRead(port_num, PROTOCOL_VERSION, MX106.ADDR_PROFILE_VELOCITY, MX106.LEN_PROFILE_VELOCITY);

            for (int i = 1; i <= 6; i++)
            {

                dxl_addparam_result = dynamixel.groupSyncReadAddParam(groupread_num, (byte)i);
                if (dxl_addparam_result != true)
                {
                    textBox2.AppendText("ID:" + i + "groupSyncWrite addparam failed \r\n");
                    return;
                }

            }

            dynamixel.groupSyncReadTxRxPacket(groupread_num);

            for (int i = 1; i <= 6; i++)
            {
                int dxl_profile_mode = (Int32)dynamixel.groupSyncReadGetData(groupread_num, (byte)i, MX106.ADDR_PROFILE_VELOCITY, MX106.LEN_PROFILE_VELOCITY);
                textBox2.AppendText("ID " + i + "Profile:" + dxl_profile_mode.ToString() + "\r\n");
            }

        }


        private void button5_Click(object sender, EventArgs e)
        {
            int groupwrite_num = dynamixel.groupSyncWrite(port_num, PROTOCOL_VERSION, MX106.ADDR_GOAL_POSITION, MX106.LEN_GOAL_POSITION);

            foreach (Control control in groupBox1.Controls)
            {
                NumericUpDown? numControls = control as NumericUpDown;

                if (numControls != null)
                {
                    int id = Convert.ToInt32(numControls.Name.Substring(13));
                    dxl_addparam_result = dynamixel.groupSyncWriteAddParam(groupwrite_num, (byte)id, (UInt32)numControls.Value, MX106.LEN_GOAL_POSITION);
                    if (dxl_addparam_result != true)
                    {
                        textBox2.AppendText("ID:" + DXL_ID1 + "groupSyncWrite addparam failed \r\n");
                        return;
                    }
                    textBox2.AppendText(numControls.Name + ":" + numControls.Value.ToString() + "\r\n");
                }
            }

            dynamixel.groupSyncWriteTxPacket(groupwrite_num);
            dynamixel.groupSyncWriteClearParam(groupwrite_num);


        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox2.Checked == true)
            {
                for (int i = 1; i <= 6; i++)
                {
                    dynamixel.write1ByteTxRx(port_num, PROTOCOL_VERSION, (byte)i, MX106.ADDR_TORQUE_ENABLE, 1);
                }
            }
            else
            {
                for (int i = 1; i <= 6; i++)
                {
                    dynamixel.write1ByteTxRx(port_num, PROTOCOL_VERSION, (byte)i, MX106.ADDR_TORQUE_ENABLE, 0);
                }
            }

        }
    }

   
}
