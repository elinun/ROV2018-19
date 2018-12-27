using ROV2019.Models;
using System;
using System.Net.Sockets;

namespace ROV2019.Presenters
{
    public class ConnectionContext
    {
        public ArduinoConnection connection;
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

        public bool OpenConnection(int timeout = 1000)
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
            catch (Exception)
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

        public string GetName()
        {
            if(isConnected())
            {
                ArduinoCommand command = new ArduinoCommand()
                {
                    Command = Command.GetName,
                    NumberOfReturnedBytes = 16
                };
                byte[] toWrite = command.GetCommandBytes();
                stream.Write(toWrite, 0, toWrite.Length);
                byte[] toRead = new byte[command.NumberOfReturnedBytes];
                stream.Read(toRead, 0, toRead.Length);
                return ArduinoCommand.GetString(toRead);
            }
            return "Failed to Get Name";
        }

        //Sends the authorize command to arduino. Should be the first command sent, besides GetName.
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
                byte[] toRead = new byte[1];
                stream.Read(toRead, 0, toRead.Length);
                return (toRead[0] == 0x01 ? true : false);
            }
            return false;
        }

        public void MoveVectorWithTrim(int forwardSpeed, int lateralSpeed, int rotationalSpeed, int verticalSpeed, int rollSpeed)
        {

        }

        public bool MoveVectorWithPIDAssist(int forwardSpeed, int lateralSpeed, int rotationalSpeed, int verticalSpeed, int rollSpeed)
        {
            ArduinoCommand command = new ArduinoCommand()
            {
                Command = Command.MoveWithPID,
                NumberOfReturnedBytes = 0
            };
            command.AddParameter(lateralSpeed);
            command.AddParameter(forwardSpeed);
            command.AddParameter(verticalSpeed);
            command.AddParameter(rotationalSpeed);
            command.AddParameter(rollSpeed);
            if (isConnected())
            {
                byte[] toWrite = command.GetCommandBytes();
                stream.Write(toWrite, 0, toWrite.Length);
                return true;
            }

            return false;
        }

        public bool SetThruster(Thrusters thruster, int value)
        {
            ArduinoCommand command = new ArduinoCommand()
            {
                Command = Command.SetThruster,
                NumberOfReturnedBytes = 0
            };
            command.AddParameter((int)thruster);
            command.AddParameter(value);
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
