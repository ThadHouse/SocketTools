using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using SocketTools;
using SocketTools.SocketTimeoutExtensions;

namespace SocketToolsTest
{
    [TestFixture]
    public class SocketExtensionStringInt
    {
        [Test]
        public void TimeoutConnectAlreadyListening()
        {
            if (RuntimeDetector.GetRuntime() == Runtime.NetCoreUnix)
            {
                Assert.Pass("Method not supported on NetCore Unix");
                return;
            }

            int port = 1791;
            SocketListener listener = new SocketListener(IPAddress.Any, port);
            listener.Start();
            Socket testSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            bool connected = testSocket.ConnectWithTimeout("127.0.0.1", port, TimeSpan.FromSeconds(1));

            Assert.That(connected, Is.True);
            Assert.That(testSocket.Connected, Is.True);

            listener.Stop();
        }

        [Test]
        public void TimeoutConnectFailure()
        {
            if (RuntimeDetector.GetRuntime() == Runtime.NetCoreUnix)
            {
                Assert.Pass("Method not supported on NetCore Unix");
                return;
            }

            int port = 1792;
            //SocketListener listener = new SocketListener(IPAddress.Any, port);
            //listener.Start();
            Socket testSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            bool connected = testSocket.ConnectWithTimeout("127.0.0.1", port, TimeSpan.FromSeconds(1));

            Assert.That(connected, Is.False);
            Assert.That(testSocket.Connected, Is.False);

            //listener.Stop();
        }

        [Test]
        public void TimeoutConnectListenHalfwayListening()
        {
            if (RuntimeDetector.GetRuntime() == Runtime.NetCoreUnix || 
                RuntimeDetector.GetRuntime() == Runtime.Mono)
            {
                Assert.Pass("Method not supported");
                return;
            }

            int port = 1793;
            SocketListener listener = null;
            
            Thread t = new Thread(() =>
            {
                Thread.Sleep(200);
                listener = new SocketListener(IPAddress.Any, port);
                listener.Start();
            });
            t.Start();
            Socket testSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            bool connected = testSocket.ConnectWithTimeout("127.0.0.1", port, TimeSpan.FromSeconds(2));

            Assert.That(connected, Is.True);
            Assert.That(testSocket.Connected, Is.True);

            listener?.Stop();
        }
    }
}
