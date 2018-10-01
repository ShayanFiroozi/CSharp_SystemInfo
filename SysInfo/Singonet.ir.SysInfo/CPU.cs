/******************************************************************* 
 *             In the name of the unique Creator                   *
 *                                                                 *
 *                 ----> Singonet.ir <----                         *
 *                                                                 *
 * C# Singnet.ir System Information Namespace                      *
 *                                                                 *
 * By Shayan Firoozi 2016 Bandar Abbas - Iran                      *
 * EMail : Singonet.ir@gmail.com                                   *
 * Phone : +98 936 517 5800                                        *
 *                                                                 *
 *******************************************************************/

using System;
using System.Linq;
using System.Management;

namespace Singonet.ir  // Singonet.ir namespace
{
    namespace SysInfo   // Hardware Info namespace
    {


        /// <summary>
        /// Class about getting CPU information
        /// </summary>
#if (!SYSINFO_INT)
        public class CPU
#else
        internal class CPU
#endif

        { 
        private ManagementObject cpu;

            /// <summary>
            /// CPU class constructor
            /// </summary>
            public CPU()
            {
                try
                {

                    cpu = new ManagementObjectSearcher("select * from Win32_Processor").Get().Cast<ManagementObject>().First();

                    if (((string)cpu["NAME"]).Length == 0)
                    {
                        throw new Exception("Could not get CPU information.");
                    }

                }

                catch (Exception ex)
                {
                    throw ex;
                }

            }



            /// <summary>
            /// returns CPU ID
            /// </summary>
            public string CPU_ID
            {
                get
                {
                    return (string)cpu["ProcessorId"];
                }
            }



            /// <summary>
            /// returns CPU Name
            /// </summary>
            public string CPU_Name
            {
                get
                {
                    return ((string)cpu["Name"]).Replace("(TM)", "™").Replace("(tm)", "™").Replace("(R)", "®").Replace("(r)", "®")
                                                                .Replace("(C)", "©").Replace("(c)", "©").Replace("    ", " ").Replace("  ", " ");
                }
            }



            /// <summary>
            /// returns CPU Description
            /// </summary>
            public string CPU_Description
            {
                get
                {
                    return (string)cpu["Caption"];
                }
            }


            /// <summary>
            /// returns CPU Manufacturer
            /// </summary>
            public string CPU_Manufacturer
            {
                get
                {
                    return (string)cpu["Manufacturer"];
                }
            }



            /// <summary>
            /// returns CPU AddresssWidth
            /// </summary>
            public ushort CPU_AddresssWidth
            {
                get
                {
                    return (ushort)cpu["AddressWidth"];
                }
            }



            /// <summary>
            /// returns CPU DataWidth
            /// </summary>
            public ushort CPU_DataWidth
            {
                get
                {
                    return (ushort)cpu["DataWidth"];
                }
            }



            /// <summary>
            /// returns CPU Architecture
            /// </summary>
            public ushort CPU_Architecture
            {
                get
                {
                    return (ushort)cpu["Architecture"];
                }
            }



            /// <summary>
            /// returns CPU Max Speed in MHZ
            /// </summary>
            public uint CPU_Max_Speed_MHZ
            {
                get
                {
                    return (uint)cpu["MaxClockSpeed"];
                }
            }



            /// <summary>
            /// returns CPU L2 Cache Size
            /// </summary>
            public ulong CPU_L2_Cache_Size
            {
                get
                {
                    return (uint)cpu["L2CacheSize"] * (ulong)1024;
                }
            }



            /// <summary>
            /// returns CPU L3 Cache Size
            /// </summary>
            public ulong CPU_L3_Cache_Size
            {
                get
                {
                    return (uint)cpu["L3CacheSize"] * (ulong)1024;
                }
            }


            /// <summary>
            /// returns CPU number of cores
            /// </summary>
            public uint CPU_Number_Of_Cores
            {
                get
                {
                    return (uint)cpu["NumberOfCores"];
                }
            }



            /// <summary>
            /// returns CPU Load Percentage
            /// </summary>
            public ushort CPU_Load_Percentage
            {
                get
                {
                    return (ushort)cpu["LoadPercentage"];
                }
            }



            /// <summary>
            /// returns CPU number of cores
            /// </summary>
            public uint CPU_Number_Of_Logical_Processors
            {
                get
                {
                    return (uint)cpu["NumberOfLogicalProcessors"];
                }
            }
        }
    }
}