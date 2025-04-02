using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stewart_Platform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        readonly Calculations sc = new Calculations();

        public double base_radius = 100.55;
        public double platform_radius = 84.39;
        public double h0 = 109.98;

        public double a = 45; //Servo operating arm length
        public double s = 113;//Platform connection link length 

        public double[] betha = { 0, 240, 120, 0, 240, 120 }; //Angle of servo arm plane relative to x axis 


        public double[] B_Angle = { 270, 330, 30, 90, 150, 210 }; //Angle of servo arm rotation point around base x axis
        public double[] P_Angle = { 295.92, 304.08, 55.92, 64.08, 175.92, 184.04 }; //Angle of platform connection point around platform x axis

        public double[] Translation_Vector = { 0, 0, 0 }; //Translation vector x,y,z

        private void btn_Calculate_Click(object sender, EventArgs e)
        {
            double roll = Convert.ToDouble(txt_Roll.Text);
            double pitch = Convert.ToDouble(txt_Pitch.Text);
            double yaw = Convert.ToDouble(txt_Yaw.Text);
            Translation_Vector[0] = Convert.ToDouble(txt_X.Text);
            Translation_Vector[1] = Convert.ToDouble(txt_Y.Text);
            Translation_Vector[2] = Convert.ToDouble(txt_Z.Text);

            //Base rotation point coordinates
            double[,] B_List =sc.Matrix_Transpose( sc.point2coordinate(B_Angle, base_radius, 0));

            Console.WriteLine("Base Rotation Point Coordinates");
            for (int i= 0; i<B_List.GetLength(0); i++)
            {
                for(int j=0; j< B_List.GetLength(1); j++)
                {
                    Console.Write(Math.Round(B_List[i,j],2).ToString()+"\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            //Platform connection point coordinates
            double[,] P_List =sc.Matrix_Transpose( sc.point2coordinate(P_Angle, platform_radius, h0));

            Console.WriteLine("Platform Point Coordinates");
            for (int i = 0; i < P_List.GetLength(0); i++)
            {
                for (int j = 0; j < P_List.GetLength(1); j++)
                {
                    Console.Write(Math.Round(P_List[i, j],2).ToString() + "\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            //Lengths of base rotation point and platform connection points
            double[] Link_Length = sc.LinkLength(roll, pitch, yaw, B_List, P_List, Translation_Vector);
            Console.WriteLine("Calculated Link Lengths");
            for (int i=0; i< Link_Length.Length; i++)
            {
                Console.WriteLine(Math.Round(Link_Length[i],2).ToString());
            }
            Console.WriteLine();

            double[] servoAngle = sc.Servo_Angle(betha, roll, pitch, yaw, P_List, B_List, a, s, Translation_Vector);
            Console.WriteLine("Servo Angles");
            for (int i = 0; i < servoAngle.Length; i++)
            {
                Console.WriteLine(Math.Round(servoAngle[i],2).ToString());
            }
            Console.WriteLine();

            if(Link_Length.Min()>(s-a)+2)
            {
                txt_S1Angle.Text =Math.Round(servoAngle[0],3).ToString();
                txt_S2Angle.Text = Math.Round(servoAngle[1], 3).ToString();
                txt_S3Angle.Text = Math.Round(servoAngle[2], 3).ToString();
                txt_S4Angle.Text = Math.Round(servoAngle[3], 3).ToString();
                txt_S5Angle.Text = Math.Round(servoAngle[4], 3).ToString();
                txt_S6Angle.Text = Math.Round(servoAngle[5], 3).ToString();

                txt_L1Length.Text = Link_Length[0].ToString();
                txt_L2Length.Text = Link_Length[1].ToString();
                txt_L3Length.Text = Link_Length[2].ToString();
                txt_L4Length.Text = Link_Length[3].ToString();
                txt_L5Length.Text = Link_Length[4].ToString();
                txt_L6Length.Text = Link_Length[5].ToString();
            }
        }
    }
}
