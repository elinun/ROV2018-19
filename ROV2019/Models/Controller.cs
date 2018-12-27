
using System.Collections.Generic;

namespace ROV2019.Models
{
    public class Controller
    {
        public List<Stick> Sticks { get; private set; }
    }
    
    public class Stick
    {

    }

    public class ConfiguredPollData
    {
        public (int forwardSpeed, int lateralSpeed, int rotationalSpeed, int verticalSpeed, int rollSpeed) Vectors { get; set; }
        public Dictionary<int, int> ServoSpeeds { get; set; }
        //add acessories later
    }
}
