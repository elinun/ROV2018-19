using ROV2019.Models;
using ROV2019.Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace ROV2019.ControllerConfigurations
{
    public abstract class ControllerConfiguration
    {
        //It makes sense for a controller configuration to work with a certain thruster layout.
        //See ConnectionClass class.
        public string Layout;
        public abstract ConfiguredPollData Poll();
        //not sure if this will be necessary.
        public ControllerConfiguration(Controller controller)
        {

        }
    }

    public class Tank : ControllerConfiguration
    {
        public Tank(Controller c) : base(c)
        {

        }
        public override ConfiguredPollData Poll()
        {
            throw new NotImplementedException();
        }
    }

    public class Arcade : ControllerConfiguration
    {
        Controller controller;
        bool canVerticalMove = true;
        bool previousUp = false;
        bool previousDown = false;
        int rollSpeed = 0;
        public Arcade(Controller c) : base(c)
        {
            controller = c;
            Layout = ThrusterLayout.TL1;
        }
        public override ConfiguredPollData Poll()
        {
            //Poll controller, and interpret that into the vectors, etc.
            controller.Poll();

            //map ROV movements to controlls on controller

            //try to prevent swapping directions on the thrusters,
            int verticalSpeed = 0;
            if(canVerticalMove)
            {
                verticalSpeed = (controller.RotationX > 0 ? -controller.RotationX : controller.RotationY);
            }
            canVerticalMove = !(previousUp && previousDown);
            previousDown = controller.Buttons[6];
            previousUp = controller.Buttons[7];

            rollSpeed -= (controller.Buttons[4] && rollSpeed > -250 ? 1 : 0);
            rollSpeed += (controller.Buttons[5]  && rollSpeed < 250? 1 : 0);

            Dictionary<Thrusters, int> thrusterSpeeds = new Dictionary<Thrusters, int>();

            /*The thrusters do not produce the same amount of thrust 
             *when they go forward as when they go backwards. This means that
             * when we move side to side we have to slow down the forward
             * thrusters. The variable below is how much to slow it down.
             * I currently have it set according to bluerobotic's documentation.
             * Max Backwards thrust/Max Forward thrust
             */
            double CorrectionFactor = 0.78846153846;
            //calculate the power to send to each thruster.
            int FLPwr = 1500;
            int FRPwr = 1500;
            int BLPwr = 1500;
            int BRPwr = 1500;

            //forward vector
            FLPwr += controller.X;
            FRPwr += controller.X;
            BLPwr += controller.X;
            BRPwr += controller.X;

            //lateral Vector
            if (controller.Y > 0)
            {
                //right
                FLPwr += (int)(controller.Y * CorrectionFactor);
                FRPwr -= controller.Y;
                BLPwr -= controller.Y;
                BRPwr += (int)(controller.Y * CorrectionFactor);
            }
            else if (controller.Y < 0)
            {
                //left
                //remember, adding a negative
                FLPwr += controller.Y;
                FRPwr -= (int)(controller.Y * CorrectionFactor);
                BLPwr -= (int)(controller.Y * CorrectionFactor);
                BRPwr += controller.Y;
            }

            //rotation
            if (controller.Z > 0)
            {
                //clockwise
                FLPwr += (int)(controller.Y * CorrectionFactor);
                FRPwr -= controller.Y;
                BLPwr += (int)(controller.Y * CorrectionFactor);
                BRPwr -= controller.Y;
            }
            else if (controller.Z < 0)
            {
                //counter-clockwise, or anti-clockwise if ur British
                FLPwr -= controller.Y;
                FRPwr += (int)(controller.Y * CorrectionFactor);
                BLPwr -= controller.Y;
                BRPwr += (int)(controller.Y * CorrectionFactor);
            }

            //vertical
            //calculate the power to send to each thruster.
            int leftPower = 1500 + verticalSpeed;
            int rightPower = 1500 + verticalSpeed;

            leftPower += rollSpeed;
            rightPower -= rollSpeed;

            thrusterSpeeds.Add(Thrusters.FrontLeft, FLPwr);
            thrusterSpeeds.Add(Thrusters.FrontRight, FRPwr);
            thrusterSpeeds.Add(Thrusters.BackRight, BRPwr);
            thrusterSpeeds.Add(Thrusters.BackLeft, BLPwr);
            thrusterSpeeds.Add(Thrusters.VerticalLeft, leftPower);
            thrusterSpeeds.Add(Thrusters.VerticalRight, rightPower);

            ConfiguredPollData data = new ConfiguredPollData()
            {
                ThrusterSpeeds = thrusterSpeeds,
                ServoSpeeds = new Dictionary<int, int>()
            };
            return data;
        }
    }

    public class Helicopter : ControllerConfiguration
    {
        Controller controller;
        public Helicopter(Controller c) : base(c)
        {
            controller = c;
            //Configuration for when we have 4 vertical thrusters.
            Layout = ThrusterLayout.TL2;
        }

        public override ConfiguredPollData Poll()
        {
            //TODO: Implement configuration
            controller.Poll();
            //TODO: Add logic to move straight forward when tilted (pitched).
            int L = 1500 + controller.X + controller.Y;
            int R = 1500 + controller.X - controller.Y;
            int VFL = 1500 + controller.RotationZ + controller.Z;
            int VFR = 1500 + controller.RotationZ - controller.Z;
            int VBL = 1500 - controller.RotationZ + controller.Z;
            int VBR = 1500 - controller.RotationZ - controller.Z;

            L = L > 0 ? Math.Min(L, 1900) : Math.Max(1100, L);
            R = R > 0 ? Math.Min(R, 1900) : Math.Max(1100, R);
            VFL = VFL > 0 ? Math.Min(VFL, 1900) : Math.Max(1100, VFL);
            VBL = VBL > 0 ? Math.Min(VBL, 1900) : Math.Max(1100, VBL);
            VBR = VBR > 0 ? Math.Min(VBR, 1900) : Math.Max(1100, VBR);
            VFR = VFR > 0 ? Math.Min(VFR, 1900) : Math.Max(1100, VFR);

            Dictionary<Thrusters, int> thrusterSpeeds = new Dictionary<Thrusters, int>();
            thrusterSpeeds.Add(Thrusters.Left, L);
            thrusterSpeeds.Add(Thrusters.Right, R);
            thrusterSpeeds.Add(Thrusters.VerticalFrontLeft, VFL);
            thrusterSpeeds.Add(Thrusters.VerticalFrontRight, VFR);
            thrusterSpeeds.Add(Thrusters.VerticalBackLeft, VBL);
            thrusterSpeeds.Add(Thrusters.VerticalBackRight, VBR);

            ConfiguredPollData data = new ConfiguredPollData()
            {
                ThrusterSpeeds = thrusterSpeeds
            };
            return data;
        }
    }
}
