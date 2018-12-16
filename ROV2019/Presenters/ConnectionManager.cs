using ROV2019.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROV2019.Presenters
{
    public class ConnectionManager
    {
        private List<ArduinoConnection> SavedConnections;

        public ConnectionManager()
        {
            //populate connections
            if(Properties.Settings.Default.SavedConnections == null)
            {
                Properties.Settings.Default.SavedConnections = new ArduinoConnections()
                {
                    connections = new List<ArduinoConnection>()
                };
            }
            SavedConnections = Properties.Settings.Default.SavedConnections.connections;
        }

        //firstThreeBytes: First three bytes of IP address. "192.168.1" will scan for arduinos
        //with IP from 192.168.1.0-192.168.1.255.
        //save: Wheather the discovered arduinos should be saved
        public async Task<List<ArduinoConnection>> Scan(string firstThreeBytes, bool save, IProgress<int> progress, int port = 1740)
        {
            List<ArduinoConnection> discoveredConnections = new List<ArduinoConnection>();
            await Task.Run(() =>
            {
                for (int lastByte = 0; lastByte < 256; lastByte++)
                {
                    ArduinoConnection connectionProperties = new ArduinoConnection()
                    {
                        Port = port,
                        IpAddress = firstThreeBytes + lastByte
                    };
                    ConnectionContext connection = new ConnectionContext(connectionProperties);
                    if (connection.OpenConnection(500))
                    {
                        discoveredConnections.Add(connectionProperties);
                        if (save)
                            Save(connectionProperties);
                    }
                    connection.Close();
                    progress.Report(lastByte / 256);
                }
            });
            
            return discoveredConnections;
        }

        public void Save(ArduinoConnection connection)
        {
            this.SavedConnections.Add(connection);
            Properties.Settings.Default.SavedConnections.connections = this.SavedConnections;
        }
    }
}
