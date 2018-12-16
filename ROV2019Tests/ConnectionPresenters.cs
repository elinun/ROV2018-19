using Microsoft.VisualStudio.TestTools.UnitTesting;
using ROV2019.Models;
using ROV2019.Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROV2019Tests
{
    [TestClass]
    public class ConnectionPresenters
    {

        [TestMethod]
        public void ConnectionManager_Scan()
        {
            ConnectionManager manager = new ConnectionManager();
            int previousProgress = 0;
            Task<List<ArduinoConnection>> results = manager.Scan("192.168.1.", true, new Progress<int>(progress =>
            {
                Assert.IsTrue(progress >= previousProgress);
                previousProgress = progress;
            }));
            Assert.AreEqual(results.Result.Count, 1);

        }

        [TestMethod]
        public void RunThruster()
        {
            ArduinoConnection connProperties = new ArduinoConnection
            {
                IpAddress = "10.108.114.65",
                Port = 1741
            };
            ConnectionContext conn = new ConnectionContext(connProperties);
            conn.OpenConnection();
            conn.SetThruster(1, 1500);
        }
    }
}
