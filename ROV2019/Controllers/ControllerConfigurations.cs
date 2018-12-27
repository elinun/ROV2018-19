using ROV2019.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROV2019.ControllerConfigurations
{
    public abstract class ControllerConfiguration
    {
        public abstract ConfiguredPollData Poll();
        public abstract ControllerConfiguration(Controller controller);
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
            ConfiguredPollData data = new ConfiguredPollData()
            {

            };
            return data;
        }
    }
}
