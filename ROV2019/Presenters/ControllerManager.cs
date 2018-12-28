using ROV2019.Models;
using SlimDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROV2019.Presenters
{
    class ControllerManager
    {
        public List<ControllerInfo> SavedControllers { get; private set; }

        public ControllerManager()
        {
            //populate
            if (Properties.Settings.Default.SavedControllers == null)
            {
                Properties.Settings.Default.SavedControllers = new Models.Controllers()
                {
                    SavedControllers = new List<ControllerInfo>()
                };
            }
            SavedControllers = Properties.Settings.Default.SavedControllers.SavedControllers;
        }

        public Controller GetController(ControllerInfo info)
        {
            switch (info.Type)
            {
                case ControllerType.SlimDX:
                    DirectInput directInput = new DirectInput();

                    return (Controller)System.Reflection.Assembly.GetExecutingAssembly().CreateInstance("ROV2019.Controllers." + info.ControllerClass, true, System.Reflection.BindingFlags.CreateInstance, null, 
                        new object[] { directInput, directInput.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly).FirstOrDefault().InstanceGuid}, null, null);
                default:
                    return null;
            }
        }

        public void Save(ControllerInfo controller)
        {
            ControllerInfo oldConToReplace = SavedControllers.FirstOrDefault(c => c.FriendlyName == controller.FriendlyName);
            SavedControllers.Remove(oldConToReplace);
            SavedControllers.Add(controller);
            Properties.Settings.Default.SavedControllers.SavedControllers = SavedControllers;
            Properties.Settings.Default.Save();
        }
        public void Add(ControllerInfo controller)
        {
            SavedControllers.Add(controller);
            Properties.Settings.Default.SavedControllers.SavedControllers = SavedControllers;
            Properties.Settings.Default.Save();
        }

        public void Remove(ControllerInfo controller)
        {
            SavedControllers.Remove(controller);
            Properties.Settings.Default.SavedControllers.SavedControllers = SavedControllers;
            Properties.Settings.Default.Save();
        }
    }
}
