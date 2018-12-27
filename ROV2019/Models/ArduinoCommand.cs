using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROV2019.Models
{
    public class ArduinoCommand
    {
        public string Command { get; set; }
        public List<byte[]> Parameters = new List<byte[]>();
        public int NumberOfReturnedBytes { get; set; }

        public void AddParameter(string value)
        {

        }

        public void AddParameter(int value)
        {
            byte[] intBytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(intBytes);
            byte[] result = intBytes;
            Parameters.Add(result);
        }
        public static byte[] GetBytes(string s)
        {
            return Encoding.ASCII.GetBytes(s?? string.Empty);
        }
        public static string GetString(byte[] bytes)
        {
            string str = Encoding.ASCII.GetString(bytes);
            return str.Replace("\0", "");
        }
        
        private List<byte> addAllBytes(List<byte> byteList, byte[] bytes)
        {
            foreach (byte b in bytes)
            {
                byteList.Add(b);
            }
            return byteList;
        }
        public byte[] GetCommandBytes()
        {
            List<byte> command = new List<byte>();
            //add first part of the command
            string cmd = ("{" + Command + ":");
            byte[] stringPart = GetBytes(cmd);
            command = addAllBytes(command, stringPart);
            //add parameters
            foreach(byte[] bytes in Parameters)
            {
                command = addAllBytes(command, bytes);
                command = addAllBytes(command, GetBytes(","));
            }
            if(Parameters.Count>0)
                command.RemoveAt(command.Count - 1);
            //add bytes to return
            cmd = ":" + NumberOfReturnedBytes + "}";
            command = addAllBytes(command, GetBytes(cmd));
            return command.ToArray();
        }
    }
    
    public class Command
    {
        public static readonly string Authorize = "authorize";
        public static readonly string SetThruster = "setThruster";
        public static readonly string AnalogRead = "analogRead";
        public static readonly string GetName = "GetName";
        public static readonly string MoveWithPID = "moveWithPIDAssist";
    }

    public enum Thrusters
    {
        FrontLeft = 0,
        FrontRight = 1,
        BackLeft = 2,
        BackRight = 3,
        VerticalLeft = 4,
        VerticalRight = 5
    }
}
