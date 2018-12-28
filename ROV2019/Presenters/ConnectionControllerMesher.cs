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
        private bool isPolling;
        public bool IsUsingPID = true;

        public ConnectionControllerMesher(ConnectionContext connection, ControllerConfiguration configuration, int PollRate = 15)
        {
            conn = connection;
            config = configuration;
            PollInterval = PollRate;
        }

        public void StartMesh()
        {
            isPolling = true;
            pollThread = new Thread(Poll);
            pollThread.Start();
        }

        private void Poll()
        {
            while(isPolling)
            {
                ConfiguredPollData data = config.Poll();
                if (IsUsingPID)
                {
                    conn.MoveVectorWithPIDAssist(data.Vectors.forwardSpeed, data.Vectors.lateralSpeed, data.Vectors.rotationalSpeed, data.Vectors.verticalSpeed, data.Vectors.rollSpeed);
                    //add servo code later
                }
                else
                {
                    conn.MoveVectorWithTrim(data.Vectors.forwardSpeed, data.Vectors.lateralSpeed, data.Vectors.rotationalSpeed, data.Vectors.verticalSpeed, data.Vectors.rollSpeed);
                }
                Thread.Sleep(PollInterval);
            }
        }

        public void StopMesh()
        {
            isPolling = false;
            pollThread.Suspend();
            pollThread = null;
        }
    }
}
