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
        public List<byte[]> Parameters { get; set; }
        public int NumberOfReturnedBytes { get; set; }

        public static byte[] GetBytes(string s)
        {
            return Encoding.ASCII.GetBytes(s);
        }
        public static string GetString(byte[] bytes)
        {
            return Encoding.ASCII.GetString(bytes);
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
    }
}
