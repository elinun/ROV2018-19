using ROV2019.ControllerConfigurations;
using ROV2019.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ROV2019.Presenters
{
    public class ConnectionControllerMesher
    {
        ConnectionContext conn;
        ControllerConfiguration config;
        Thread pollThread;
        int PollInterval;
        public bool IsMeshing = false;
        public bool IsUsingPID = true;

        public ConnectionControllerMesher(ConnectionContext connection, ControllerConfiguration configuration, int PollRate = 15)
        {
            conn = connection;
            config = configuration;
            PollInterval = PollRate;
        }

        public void StartMesh()
        {
            if (!IsMeshing)
            {
                IsMeshing = true;
                pollThread = new Thread(Poll);
                pollThread.Start();
            }
        }

        private void Poll()
        {
            while(IsMeshing)
            {
                ConfiguredPollData data = config.Poll();
                int VL = data.ThrusterSpeeds.FirstOrDefault(x => x.Key == Thrusters.VerticalLeft).Value;
                int VR = data.ThrusterSpeeds.FirstOrDefault(x => x.Key == Thrusters.VerticalRight).Value;
                int FL = data.ThrusterSpeeds.FirstOrDefault(x => x.Key == Thrusters.FrontLeft).Value;
                int FR = data.ThrusterSpeeds.FirstOrDefault(x => x.Key == Thrusters.FrontRight).Value;
                int BL = data.ThrusterSpeeds.FirstOrDefault(x => x.Key == Thrusters.BackLeft).Value;
                int BR = data.ThrusterSpeeds.FirstOrDefault(x => x.Key == Thrusters.BackRight).Value;

                if (IsUsingPID)
                {
                    //Is Using PID has turned into VerticalStabilize
                    conn.VerticalStabilize(VL, VR);
                    conn.MoveAndAddTrim(FL, FR, BL, BR);
                    //add servo code later
                }
                else
                {
                    conn.MoveAndAddTrim(VL, VR, FL, FR, BR, BL);
                }
                Thread.Sleep(PollInterval);
            }
        }

        public void StopMesh()
        {
            IsMeshing = false;
            if(pollThread != null)
                pollThread.Abort() ;
            pollThread = null;
        }
    }
}
