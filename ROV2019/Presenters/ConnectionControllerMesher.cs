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

        public ConnectionControllerMesher(ConnectionContext connection, ControllerConfiguration configuration, int PollRate = 50)
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
            while (IsMeshing)
            {
                ConfiguredPollData data = config.Poll();
                if (!IsUsingPID)
                    conn.MoveAndAddTrim(data.ThrusterSpeeds);
                else
                    conn.VerticalStabilize(data.ThrusterSpeeds);
                //Add servos later
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
