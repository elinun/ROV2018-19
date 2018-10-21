using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROV2019.Models
{
    public abstract class Controller
    {
        public int[] Thrusters { get; }
        public bool[] Buttons { get; }
        public int[] Switches { get; }
        public int[] IntegratedTrim { get; }
        public object[] Miscelaneous { get; }

        public ValueChanged OnValueChanged { get; set; }
        public IntChanged OnThrusterChanged { get; set; }
        public ButtonChanged OnButtonChanged { get; set; }
        public IntChanged OnSwitchChanged { get; set; }
        public IntChanged OnTrimChanged { get; set; }
        public MiscelaneousChanged OnMiscelaneousChanged { get; set; }

        public interface MiscelaneousChanged
        {
            void OnMiscelaneousChanged(int index, object oldValue, object newValue);
        }

        public interface ValueChanged
        {
            void OnValueChanged(Controller c);
        }

        public interface IntChanged
        {
            void OnIntValueChanged(int index, int oldValue, int newValue);
        }

        public interface ButtonChanged
        {
            void OnButtonChanged(int index, bool oldValue, bool newValue);
        }
    }
}
