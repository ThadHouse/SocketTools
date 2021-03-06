﻿using System;
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
    public class SocketExceptionEndPoint
    {
        [Test]
        public void TimeoutConnectAlreadyListening()
        {
            int port = 1785;
            SocketListener listener = new SocketListener(IPAddress.Any, port);
            listener.Start();
            Socket testSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            bool connected = testSocket.ConnectWithTimeout(new IPEndPoint(IPAddress.Loopback, port), TimeSpan.FromSeconds(1));

            Assert.That(connected, Is.True);
            Assert.That(testSocket.Connected, Is.True);

            listener.Stop();
        }

        [Test]
        public void TimeoutConnectFailure()
        {
            int port = 1786;
            Socket testSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            bool connected = testSocket.ConnectWithTimeout(new IPEndPoint(IPAddress.Parse("10.10.10.10"), port), TimeSpan.FromSeconds(1));

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

            int port = 1787;
            SocketListener listener = null;

            Thread t = new Thread(() =>
            {
                Thread.Sleep(200);
                listener = new SocketListener(IPAddress.Any, port);
                listener.Start();
            });
            t.Start();
            Socket testSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            bool connected = testSocket.ConnectWithTimeout(new IPEndPoint(IPAddress.Loopback, port), TimeSpan.FromSeconds(1));

            Assert.That(connected, Is.True);
            Assert.That(testSocket.Connected, Is.True);

            listener?.Stop();
        }
    }
}
