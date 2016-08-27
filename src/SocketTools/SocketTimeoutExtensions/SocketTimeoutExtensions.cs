using System;
using System.Net;
using System.Net.Sockets;

namespace SocketTools.SocketTimeoutExtensions
{
    public static class SocketTimeoutExtensions
    {
        public static bool ConnectWithTimeout(this Socket socket, string host, int port, TimeSpan timeout)
        {
            if (timeout <= TimeSpan.Zero)
            {
                socket.Connect(host, port);
                return true;
            }

            try
            {
                socket.Blocking = false;
                socket.Connect(host, port);
                // If connect succeeds, we have properly connected
                return true;
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.WouldBlock || ex.SocketErrorCode == SocketError.InProgress)
                {
                    DateTime waitUntil = DateTime.UtcNow + timeout;

                    try
                    {
                        while (true)
                        {
                            if (socket.Poll(100, SelectMode.SelectWrite))
                            {
                                // If poll succeeds, the socket properly connectds
                                // Some runtimes require a connect call to be made again.
                                // Will be caught properly on the runtimes that don't require it
                                socket.Connect(host, port);
                                return true;
                            }
                            else
                            {
                                if (DateTime.UtcNow >= waitUntil)
                                {
                                    // Timed out
                                    // TODO: Log
                                    return false;
                                }
                            }
                        }
                    }
                    catch (SocketException ex2)
                    {
                        if (ex2.SocketErrorCode == SocketError.IsConnected)
                        {
                            // Catching already connected, and just returning
                            return true;
                        }
                        throw;
                    }
                }
                else
                {
                    if (ex.SocketErrorCode == SocketError.ConnectionRefused)
                    {
                        // Refused connection is an unexceptional case.
                        return false;
                    }
                    throw;
                }
            }
            finally
            {
                socket.Blocking = true;
            }
        }

        public static bool ConnectWithTimeout(this Socket socket, EndPoint endPoint, TimeSpan timeout)
        {
            if (timeout <= TimeSpan.Zero)
            {
                // Connect without a timeout
                socket.Connect(endPoint);
                return true;
            }

            try
            {
                socket.Blocking = false;
                socket.Connect(endPoint);
                // If connect succeeds, we have properly connected
                return true;
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.WouldBlock || ex.SocketErrorCode == SocketError.InProgress)
                {
                    DateTime waitUntil = DateTime.UtcNow + timeout;

                    try
                    {
                        while (true)
                        {
                            if (socket.Poll(100, SelectMode.SelectWrite))
                            {
                                // If poll succeeds, the socket properly connectds
                                // Some runtimes require a connect call to be made again.
                                // Will be caught properly on the runtimes that don't require it
                                socket.Connect(endPoint);
                                return true;
                            }
                            else
                            {
                                if (DateTime.UtcNow >= waitUntil)
                                {
                                    // Timed out
                                    // TODO: Log
                                    return false;
                                }
                            }
                        }
                    }
                    catch (SocketException ex2)
                    {
                        if (ex2.SocketErrorCode == SocketError.IsConnected)
                        {
                            // Catching already connected, and just returning
                            return true;
                        }
                        throw;
                    }
                }
                else
                {
                    if (ex.SocketErrorCode == SocketError.ConnectionRefused)
                    {
                        // Refused connection is an unexceptional case.
                        return false;
                    }
                    throw;
                }
            }
            finally
            {
                socket.Blocking = true;
            }
        }

        public static bool ConnectWithTimeout(this Socket socket, IPAddress address, int port, TimeSpan timeout)
        {
            if (timeout <= TimeSpan.Zero)
            {
                // Connect without a timeout
                socket.Connect(address, port);
                return true;
            }

            try
            {
                socket.Blocking = false;
                socket.Connect(address, port);
                // If connect succeeds, we have properly connected
                return true;
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.WouldBlock || ex.SocketErrorCode == SocketError.InProgress)
                {
                    DateTime waitUntil = DateTime.UtcNow + timeout;

                    try
                    {
                        while (true)
                        {
                            if (socket.Poll(100, SelectMode.SelectWrite))
                            {
                                // If poll succeeds, the socket properly connectds
                                // Some runtimes require a connect call to be made again.
                                // Will be caught properly on the runtimes that don't require it
                                socket.Connect(address, port);
                                return true;
                            }
                            else
                            {
                                if (DateTime.UtcNow >= waitUntil)
                                {
                                    // Timed out
                                    // TODO: Log
                                    return false;
                                }
                            }
                        }
                    }
                    catch (SocketException ex2)
                    {
                        if (ex2.SocketErrorCode == SocketError.IsConnected)
                        {
                            // Catching already connected, and just returning
                            return true;
                        }
                        throw;
                    }
                }
                else
                {
                    if (ex.SocketErrorCode == SocketError.ConnectionRefused)
                    {
                        // Refused connection is an unexceptional case.
                        return false;
                    }
                    throw;
                }
            }
            finally
            {
                socket.Blocking = true;
            }
        }

        public static bool ConnectWithTimeout(this Socket socket, IPAddress[] addresses, int port, TimeSpan timeout)
        {
            if (timeout <= TimeSpan.Zero)
            {
                // Connect without a timeout
                socket.Connect(addresses, port);
                return true;
            }

            try
            {
                socket.Blocking = false;
                socket.Connect(addresses, port);
                // If connect succeeds, we have properly connected
                return true;
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.WouldBlock || ex.SocketErrorCode == SocketError.InProgress)
                {
                    DateTime waitUntil = DateTime.UtcNow + timeout;

                    try
                    {
                        while (true)
                        {
                            if (socket.Poll(100, SelectMode.SelectWrite))
                            {
                                // If poll succeeds, the socket properly connectds
                                // Some runtimes require a connect call to be made again.
                                // Will be caught properly on the runtimes that don't require it
                                socket.Connect(addresses, port);
                                return true;
                            }
                            else
                            {
                                if (DateTime.UtcNow >= waitUntil)
                                {
                                    // Timed out
                                    // TODO: Log
                                    return false;
                                }
                            }
                        }
                    }
                    catch (SocketException ex2)
                    {
                        if (ex2.SocketErrorCode == SocketError.IsConnected)
                        {
                            // Catching already connected, and just returning
                            return true;
                        }
                        throw;
                    }
                }
                else
                {
                    if (ex.SocketErrorCode == SocketError.ConnectionRefused)
                    {
                        // Refused connection is an unexceptional case.
                        return false;
                    }
                    throw;
                }
            }
            finally
            {
                socket.Blocking = true;
            }
        }
    }
}
