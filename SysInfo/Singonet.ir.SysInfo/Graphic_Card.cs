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
        /// Class about gettng graphic card information
        /// </summary>

#if (!SYSINFO_INT)
        public class Graphic_Card

#else
            internal  class Graphic_Card
#endif
        {
            private ManagementObject gc;

            /// <summary>
            /// Graphic_Card class constructor
            /// </summary>
            public Graphic_Card()
            {
                try
                {

                    gc = new ManagementObjectSearcher("select * from Win32_VideoController").Get().Cast<ManagementObject>().First();

                    if (((string)gc["Name"]).Length == 0)
                    {
                        throw new Exception("Could not get Graphic card information.");
                    }

                }

                catch (Exception ex)
                {
                    throw ex;
                }

            }


            /// <summary>
            /// returns Graphic Card Name
            /// </summary>
            public string GC_Name
            {
                get
                {
                    return (string)gc["Name"];
                }
            }



            /// <summary>
            /// returns Graphic Card GPU speed
            /// </summary>
            public string GC_GPU_SPEED
            {
                get
                {
                    return (string)gc["AdapterDACType"];
                }
            }


            /// <summary>
            /// returns Graphic Card Memory Capacity
            /// </summary>
            public uint GC_Memory_Capacity
            {
                get
                {
                    return (uint)gc["AdapterRAM"];
                }
            }



            /// <summary>
            /// returns Graphic Card Current Horizontal Resolution
            /// </summary>
            public uint GC_Current_Horizontal_Resolution
            {
                get
                {
                    return (uint)gc["CurrentHorizontalResolution"];
                }
            }



            /// <summary>
            /// returns Graphic Card Current Vertical Resolution
            /// </summary>
            public uint GC_Current_Vertical_Resolution
            {
                get
                {
                    return (uint)gc["CurrentVerticalResolution"];
                }
            }



            /// <summary>
            /// returns Graphic Card Driver Date
            /// </summary>
            public string GC_Driver_Date
            {
                get
                {
                    return (string)gc["DriverDate"];
                }
            }


            /// <summary>
            /// returns Graphic Card Driver Version
            /// </summary>
            public string GC_Driver_Version
            {
                get
                {
                    return (string)gc["DriverVersion"];
                }
            }

        }

    }
}