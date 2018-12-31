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
                stream.ReadTimeout = timeout;
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

        public (int AcX, int AcY, int AcZ, int Temp, int GyX, int GyY, int GyZ) GetAccelerations()
        {
            if(isConnected())
            {
                ArduinoCommand cmd = new ArduinoCommand()
                {
                    Command = Command.GetAccelerations
                };
                byte[] toWrite = cmd.GetCommandBytes();
                stream.Write(toWrite, 0, toWrite.Length);
                char c;
                StringBuilder sb = new StringBuilder();
                while((c=(char)stream.ReadByte()) != '}')
                {
                    if(c != '{')
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
            return (0,0,0,0,0,0,0);
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
                return (toRead[0] == 0x01);
            }
            return false;
        }

        public void MoveVectors(int forwardSpeed, int lateralSpeed, int rotationalSpeed, int verticalSpeed, int rollSpeed)
        {
            //calculate the power to send to each thruster.
            int leftPower = 1500 + verticalSpeed;
            int rightPower = 1500 + verticalSpeed;

            leftPower += rollSpeed;
            rightPower -= rollSpeed;

            //TODO: Add trim

            leftPower = (leftPower > 1500 ? Math.Min(leftPower, 1900) : Math.Max(leftPower, 1100));
            rightPower = (rightPower > 1500 ? Math.Min(rightPower, 1900) : Math.Max(rightPower, 1100));

            SetThruster(Thrusters.VerticalLeft, leftPower);
            SetThruster(Thrusters.VerticalRight, rightPower);
            MoveVectors(forwardSpeed, lateralSpeed, rotationalSpeed);
        }

        public void MoveVectors(int forwardSpeed, int lateralSpeed, int rotationalSpeed)
        {
            /*The thrusters do not produce the same amount of thrust 
             *when they go forward as when they go backwards. This means that
             * when we move side to side we have to slow down the forward
             * thrusters. The variable below is how much to slow it down.
             * I currently have it set according to bluerobotic's documentation.
             * Max Backwards thrust/Max Forward thrust
             */
            double CorrectionFactor = 0.78846153846;
            //calculate the power to send to each thruster.
            int FLPwr = 1500;
            int FRPwr = 1500;
            int BLPwr = 1500;
            int BRPwr = 1500;

            //forward vector
            FLPwr += forwardSpeed;
            FRPwr += forwardSpeed;
            BLPwr += forwardSpeed;
            BRPwr += forwardSpeed;

            //lateral Vector
            if(lateralSpeed>0)
            {
                //right
                FLPwr += (int)(lateralSpeed * CorrectionFactor);
                FRPwr -= lateralSpeed;
                BLPwr -= lateralSpeed;
                BRPwr += (int)(lateralSpeed * CorrectionFactor);
            }
            else if(lateralSpeed < 0)
            {
                //left
                //remember, adding a negative
                FLPwr += lateralSpeed;
                FRPwr -= (int)(lateralSpeed * CorrectionFactor);
                BLPwr -= (int)(lateralSpeed * CorrectionFactor);
                BRPwr += lateralSpeed;
            }

            //rotation
            if(rotationalSpeed>0)
            {
                //clockwise
                FLPwr += (int)(lateralSpeed * CorrectionFactor);
                FRPwr -= lateralSpeed;
                BLPwr += (int)(lateralSpeed * CorrectionFactor);
                BRPwr -= lateralSpeed;
            }
            else if(rotationalSpeed<0)
            {
                //counter-clockwise, or anti-clockwise if ur British
                FLPwr -= lateralSpeed;
                FRPwr += (int)(lateralSpeed * CorrectionFactor);
                BLPwr -= lateralSpeed;
                BRPwr += (int)(lateralSpeed * CorrectionFactor);
            }

            //TODO: Add trim.

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

        public bool VerticalStabilize(int verticalSpeed, int rollSpeed)
        {
            ArduinoCommand command = new ArduinoCommand()
            {
                Command = Command.VerticalStabilize,
                NumberOfReturnedBytes = 0
            };
            command.AddParameter(verticalSpeed);
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
