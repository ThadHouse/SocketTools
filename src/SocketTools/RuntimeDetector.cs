using System;
using System.Runtime.InteropServices;

namespace SocketTools
{
    enum Runtime
    {
        NotCached,
        Mono,
        NetFramework,
        NetCoreWindows,
        NetCoreUnix
    }

    internal static class RuntimeDetector
    {
        

        private static Runtime s_runtimeState = Runtime.NotCached;

        /// <summary>
        /// Gets if the runtime has sockets that support proper connections
        /// </summary>
        /// <returns></returns>
        public static Runtime GetRuntime()
        {
            if (s_runtimeState == Runtime.NotCached)
            {
                Type type = Type.GetType("Mono.Runtime");
                if (type == null)
                {
#if NETSTANDARD1_3
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        // Windows
                        s_runtimeState = Runtime.NetCoreWindows;
                    }
                    else
                    {
                        // Unix
                        s_runtimeState = Runtime.NetCoreUnix;
                    }
#else
                    // Full .net framework
                    s_runtimeState = Runtime.NetFramework;
#endif
                }
                else
                {
                    s_runtimeState = Runtime.Mono;
                }
            }
            return s_runtimeState;
        }
    }
}
