using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MX106_Control_Table
{
    internal class MX106
    {
        //Address of datas
        public const int ADDR_MODEL_NUMBER                  = 0;
        public const int ADDR_MODEL_INFORMATION             = 2;
        public const int ADDR_FIRMWARE_VERSION              = 6;
        public const int ADDR_ID                            = 7;
        public const int ADDR_BAUD_RATE                     = 8;
        public const int ADDR_RETURN_DELAY_TIME             = 9;
        public const int ADDR_DRIVE_MODE                    = 10;
        public const int ADDR_OPERATING_MODE                = 11;
        public const int ADDR_SECONDARY_ID                  = 12;
        public const int ADDR_PROTOCOL_TYPE                 = 13;
        public const int ADDR_HOMING_OFFSET                 = 20;
        public const int ADDR_MOVING_THRESHOLD              = 24;
        public const int ADDR_TEMPERATURE_LIMIT             = 31;
        public const int ADDR_MAX_VOLTAGE_LIMIT             = 32;
        public const int ADDR_MIN_VOLTAGE_LIMIT             = 34;
        public const int ADDR_PWM_LIMIT                     = 36;
        public const int ADDR_CURRENT_LIMIT                 = 38;
        public const int ADDR_ACCELERATION_LIMIT            = 40;
        public const int ADDR_VELOCITY_LIMIT                = 44;
        public const int ADDR_MAX_POSITION_LIMIT            = 48;
        public const int ADDR_MIN_POSITION_LIMIT            = 52;
        public const int ADDR_SHUTDOWN                      = 63;

        public const int ADDR_TORQUE_ENABLE                 = 64;
        public const int ADDR_LED                           = 65;
        public const int ADDR_STATUS_RETURN_LEVEL           = 68;
        public const int ADDR_REGISTERED_INSTRUCTION        = 69;
        public const int ADDR_HARDWARE_ERROR_STATUS         = 70;
        public const int ADDR_VELOCITY_I_GAIN               = 76;
        public const int ADDR_VELOCITY_P_GAIN               = 78;
        public const int ADDR_POSITION_D_GAIN               = 80;
        public const int ADDR_POSITION_I_GAIN               = 82;
        public const int ADDR_POSITION_P_GAIN               = 84;
        public const int ADDR_FEEDFORWARD_2ND_GAIN          = 88;
        public const int ADDR_FEEDFORWARD_1ST_GAIN          = 90;
        public const int ADDR_BUS_WATCHDOG                  = 98;
        public const int ADDR_GOAL_PWM                      = 100;
        public const int ADDR_GOAL_CURRENT                  = 102;
        public const int ADDR_GOAL_VELOCITY                 = 104;
        public const int ADDR_PROFILE_ACCELERATION          = 108;
        public const int ADDR_PROFILE_VELOCITY              = 112;
        public const int ADDR_GOAL_POSITION                 = 116;
        public const int ADDR_REALTIME_TICK                 = 120;
        public const int ADDR_MOVING                        = 122;
        public const int ADDR_MOVING_STATUS                 = 123;
        public const int ADDR_PRESENT_PWM                   = 124;
        public const int ADDR_PRESENT_CURRENT               = 126;
        public const int ADDR_PRESENT_VELOCITY              = 128;
        public const int ADDR_PRESENT_POSITION              = 132;
        public const int ADDR_VELOCITY_TRAJECTORY           = 136;
        public const int ADDR_POSITION_TRAJECTORY           = 140;
        public const int ADDR_PRESENT_INPUT_VOLTAGE         = 144;
        public const int ADDR_PRESENT_TEMPERATURE           = 146;

        //Length of datas
        public const int LEN_MODEL_NUMBER                   = 2;
        public const int LEN_MODEL_INFORMATION              = 4;
        public const int LEN_FIRMWARE_VERSION               = 1;
        public const int LEN_ID                             = 1;
        public const int LEN_BAUD_RATE                      = 1;
        public const int LEN_RETURN_DELAY_TIME              = 1;
        public const int LEN_DRIVE_MODE                     = 1;
        public const int LEN_OPERATING_MODE                 = 1;
        public const int LEN_SECONDARY_ID                   = 1;
        public const int LEN_PROTOCOL_TYPE                  = 1;
        public const int LEN_HOMING_OFFSET                  = 4;
        public const int LEN_MOVING_THRESHOLD               = 4;
        public const int LEN_TEMPERATURE_LIMIT              = 1;
        public const int LEN_MAX_VOLTAGE_LIMIT              = 2;
        public const int LEN_MIN_VOLTAGE_LIMIT              = 2;
        public const int LEN_PWM_LIMIT                      = 2;
        public const int LEN_CURRENT_LIMIT                  = 2;
        public const int LEN_ACCELERATION_LIMIT             = 4;
        public const int LEN_VELOCITY_LIMIT                 = 4;
        public const int LEN_MAX_POSITION_LIMIT             = 4;
        public const int LEN_MIN_POSITION_LIMIT             = 4;
        public const int LEN_SHUTDOWN                       = 1;

        public const int LEN_TORQUE_ENABLE                  = 1;
        public const int LEN_LED                            = 1;
        public const int LEN_STATUS_RETURN_LEVEL            = 1;
        public const int LEN_REGISTERED_INSTRUCTION         = 1;
        public const int LEN_HARDWARE_ERROR_STATUS          = 1;
        public const int LEN_VELOCITY_I_GAIN                = 2;
        public const int LEN_VELOCITY_P_GAIN                = 2;
        public const int LEN_POSITION_D_GAIN                = 2;
        public const int LEN_POSITION_I_GAIN                = 2;
        public const int LEN_POSITION_P_GAIN                = 2;
        public const int LEN_FEEDFORWARD_2ND_GAIN           = 2;
        public const int LEN_FEEDFORWARD_1ST_GAIN           = 2;
        public const int LEN_BUS_WATCHDOG                   = 1;
        public const int LEN_GOAL_PWM                       = 2;
        public const int LEN_GOAL_CURRENT                   = 2;
        public const int LEN_GOAL_VELOCITY                  = 4;
        public const int LEN_PROFILE_ACCELERATION           = 4;
        public const int LEN_PROFILE_VELOCITY               = 4;
        public const int LEN_GOAL_POSITION                  = 4;
        public const int LEN_REALTIME_TICK                  = 2;
        public const int LEN_MOVING                         = 1;
        public const int LEN_MOVING_STATUS                  = 1;
        public const int LEN_PRESENT_PWM                    = 2;
        public const int LEN_PRESENT_CURRENT                = 2;
        public const int LEN_PRESENT_VELOCITY               = 4;
        public const int LEN_PRESENT_POSITION               = 4;
        public const int LEN_VELOCITY_TRAJECTORY            = 4;
        public const int LEN_POSITION_TRAJECTORY            = 4;
        public const int LEN_PRESENT_INPUT_VOLTAGE          = 2;
        public const int LEN_PRESENT_TEMPERATURE            = 1;

        //General Constans
        public const int DRIVE_TIME_BASED                   = 4;
        public const int DRIVE_VELOCITY_BASED               = 0;

    }
}
