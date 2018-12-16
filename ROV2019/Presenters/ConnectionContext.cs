using ROV2019.Models;
using System;
using System.Net.Sockets;

namespace ROV2019.Presenters
{
    public class ConnectionContext
    {
        private ArduinoConnection connection;
        private TcpClient client;
        private NetworkStream stream;

        public ConnectionContext(ArduinoConnection connection)
        {
            this.connection = connection;
        }

        public void Close()
        {
            if (isConnected())
            {
                stream.Close();
                client.Close();
            }
        }

        public bool OpenConnection()
        {
           return OpenConnection(1000);
        }

        public bool OpenConnection(int timeout)
        {
            try
            {
                client = new TcpClient();
                var result = client.BeginConnect(connection.IpAddress, connection.Port, null, null);
                var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(timeout));

                if (!success)
                {
                    return false;
                }

                stream = client.GetStream();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool isConnected()
        {
            if(client != null && stream != null)
            {
                return client.Connected;
            }
            return false;
        }

        //Sends the authorize command to arduino. Should be the first command sent.
        public bool Authorize()
        {
            ArduinoCommand command = new ArduinoCommand()
            {
                Command = Command.Authorize,
                NumberOfReturnedBytes = 1
            };
            command.Parameters.Add(ArduinoCommand.GetBytes(connection.Password));
            if (isConnected())
            {
                byte[] toWrite = command.GetCommandBytes();
                stream.Write(toWrite, 0, toWrite.Length);
                return true;
            }
            return false;
        }

        public bool SetThruster(int thruster, int value)
        {
            ArduinoCommand command = new ArduinoCommand()
            {
                Command = Command.SetThruster,
                NumberOfReturnedBytes = 0
            };
            if (isConnected())
            {
                
            }
        }
    }
}
