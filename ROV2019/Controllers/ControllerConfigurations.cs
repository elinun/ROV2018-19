using ROV2019.Models;
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
        public Arcade(Controller c) : base(c)
        {
            controller = c;
        }
        public override ConfiguredPollData Poll()
        {
            //TODO: Poll controller, and interpret that into the vectors, etc.
            controller.Poll();

            //map ROV movements to controlls on controller
            int verticalSpeed = (controller.RotationX > 0 ? controller.RotationX : controller.RotationY);
            (int forwardSpeed, int lateralSpeed, int rotationalSpeed, int verticalSpeed, int rollSpeed) mVectors = (
            controller.X,
            controller.Y, controller.Z, verticalSpeed, 0);

            ConfiguredPollData data = new ConfiguredPollData()
            {
                Vectors = mVectors,
                ServoSpeeds = new Dictionary<int, int>()
            };
            return data;
        }
    }
}
