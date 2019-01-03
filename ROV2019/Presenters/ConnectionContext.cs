using ROV2019.Models;
using System;
using System.Net.Sockets;
using System.Text;

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
                //stream.ReadTimeout = timeout;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool isConnected()
        {
            try
            {
                stream.Write(ArduinoCommand.GetBytes(" "), 0, 1);
                return true;
            }
            catch (Exception) { return false; }
        }

        public (int AcX, int AcY, int AcZ, int Temp, int GyX, int GyY, int GyZ) GetAccelerations()
        {
            try
            {
                ArduinoCommand cmd = new ArduinoCommand()
                {
                    Command = Command.GetAccelerations
                };
                byte[] toWrite = cmd.GetCommandBytes();
                stream.Write(toWrite, 0, toWrite.Length);
                char c;
                StringBuilder sb = new StringBuilder();
                while ((c = (char)stream.ReadByte()) != '}')
                {
                    if (c != '{')
                        sb.Append(c);
                }
                string str = sb.ToString();
                string[] vals = str.Split(';');
                int X = int.Parse(vals[0].Substring(2));
                int Y = int.Parse(vals[1].Substring(2));
                int Z = int.Parse(vals[2].Substring(2));
                int temp = int.Parse(vals[3].Substring(2));
                int GyX = int.Parse(vals[4].Substring(2));
                int GyY = int.Parse(vals[5].Substring(2));
                int GyZ = int.Parse(vals[6].Substring(2));
                return (X, Y, Z, temp, GyX, GyY, GyZ);
            }
            catch (Exception) { return (0, 0, 0, 0, 0, 0, 0); }
        }

        public void Stop()
        {
            if (isConnected())
            {
                for (int i = 0; i < 6; i++)
                {
                    SetThruster((Thrusters)Enum.Parse(typeof(Thrusters), i.ToString()), 1500);

                }    
            }
        }

        public string GetName()
        {
            if(isConnected())
            {
                try
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
                catch(Exception)
                {
                    return "Failed To Get Name";
                }
            }
            return "Failed to Get Name";
        }

        //Sends the authorize command to arduino. Should be the first command sent, besides GetName.
        public bool Authorize()
        {
            try
            {
                ArduinoCommand command = new ArduinoCommand()
                {
                    Command = Command.Authorize,
                    NumberOfReturnedBytes = 1
                };
                command.Parameters.Add(ArduinoCommand.GetBytes(connection.Password));
                byte[] toWrite = command.GetCommandBytes();
                stream.Write(toWrite, 0, toWrite.Length);
                byte[] toRead = new byte[1];
                stream.Read(toRead, 0, toRead.Length);
                return (toRead[0] == 0x01);
            }
            catch (Exception) { return false; }
        }

        public void MoveAndAddTrim(int VL, int VR, int FL, int FR, int BL, int BR)
        {


            //Trim
            VR += connection.Trim.RollCorrection;
            VL -= connection.Trim.RollCorrection;
                                                                     
            VL = (VL > 1500 ? Math.Min(VL, 1900) : Math.Max(VL, 1100));
            VR = (VR > 1500 ? Math.Min(VR, 1900) : Math.Max(VR, 1100));

            SetThruster(Thrusters.VerticalLeft, VL);
            SetThruster(Thrusters.VerticalRight, VR);
            MoveAndAddTrim(FR, FL, BL, BR);
        }

        //Adds the trim associated with this connection and sends command to turn on the thrusters
        //Parameters are the microsecond pulse lengths generated from ControllerConfig.
        public void MoveAndAddTrim(int FLPwr, int FRPwr, int BLPwr, int BRPwr)
        {        

            //Add trim. May need to add correction factor
            FLPwr -= connection.Trim.LeftToRightCorrection;
            FLPwr -= connection.Trim.FrontToBackCorrection;

            FRPwr += connection.Trim.LeftToRightCorrection;
            FRPwr += connection.Trim.FrontToBackCorrection;

            BLPwr -= connection.Trim.LeftToRightCorrection;
            BLPwr -= connection.Trim.FrontToBackCorrection;

            BRPwr += connection.Trim.LeftToRightCorrection;
            BRPwr += connection.Trim.FrontToBackCorrection;

            FLPwr = (FLPwr > 1500 ? Math.Min(FLPwr, 1900) : Math.Max(FLPwr, 1100));
            FRPwr = (FRPwr > 1500 ? Math.Min(FRPwr, 1900) : Math.Max(FRPwr, 1100));
            BLPwr = (BLPwr > 1500 ? Math.Min(BLPwr, 1900) : Math.Max(BLPwr, 1100));
            BRPwr = (BRPwr > 1500 ? Math.Min(BRPwr, 1900) : Math.Max(BRPwr, 1100));

            //send commands
            SetThruster(Thrusters.FrontLeft, FLPwr);
            SetThruster(Thrusters.FrontRight, FRPwr);
            SetThruster(Thrusters.BackLeft, BLPwr);
            SetThruster(Thrusters.BackRight, BRPwr);
        }

        public bool VerticalStabilize(int verticalLeftPwr, int verticalRightPwr)
        {
            try
            {
                ArduinoCommand command = new ArduinoCommand()
                {
                    Command = Command.VerticalStabilize,
                    NumberOfReturnedBytes = 0
                };
                int verticalSpeed = ((verticalLeftPwr + verticalRightPwr) / 2) - 1500;
                int rollPos = (verticalLeftPwr - verticalRightPwr) / 2;
                command.AddParameter(verticalSpeed);
                command.AddParameter(rollPos);
                byte[] toWrite = command.GetCommandBytes();
                stream.Write(toWrite, 0, toWrite.Length);
                return true;
            }
            catch (Exception) { return false; }
        }

        public bool SetThruster(Thrusters thruster, int value)
        {
            try
            {
                ArduinoCommand command = new ArduinoCommand()
                {
                    Command = Command.SetThruster,
                    NumberOfReturnedBytes = 0
                };
                command.AddParameter((int)thruster);
                command.AddParameter(value);
                {
                    byte[] toWrite = command.GetCommandBytes();
                    stream.Write(toWrite, 0, toWrite.Length);
                    return true;
                }
            }
            catch (Exception) { return false; }
        }
    }
}
