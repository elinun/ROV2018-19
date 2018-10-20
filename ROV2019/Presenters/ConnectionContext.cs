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

        public bool OpenConnection()
        {
            try
            {
                client = new TcpClient(connection.IpAddress, connection.Port);
                stream = client.GetStream();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public bool isConnected()
        {
            return client.Connected;
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
    }
}
