using ROV2019.Models;
using SlimDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROV2019.Controllers
{
    public class PlayStation : Controller
    {
        Joystick Joystick;
        public PlayStation(DirectInput input, Guid guid)
        {
            Joystick = new Joystick(input, guid);
            SlimDX.Result acquireSuccess = Joystick.Acquire();
            //if (acquireSuccess.IsFailure)
                //throw new Exception("Failed to Acquire PlayStation Controller.");
        }

        public override void Poll()
        {
            JoystickState state = Joystick.GetCurrentState();
            //Straight forward for PS Controllers
            X = state.X;
            Y = state.Y;
            Z = state.Z;
            RotationX = state.RotationX;
            RotationY = state.RotationY;
            RotationZ = state.RotationZ;
            PointOfViewControllers = state.GetPointOfViewControllers();
            Buttons = state.GetButtons();
                        
            //add more as we need more
        }
    }
}
