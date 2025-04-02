using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace syncwrite1
{
    internal class dynamixel
    {
        const string dll_path = "D:\\05-3D_Skecth\00-Parts\\Catia Parts\\Electromechanical Parts\\Motor\\Servo Motor\\DynamixShield-master\\DynamixShield-master\\libraries\\DynamixelSerial\\DynamixelSerial.cpp";
        [DllImport(dll_path)];

        private static extern void beginCom(long baudRate); 




    }
}
