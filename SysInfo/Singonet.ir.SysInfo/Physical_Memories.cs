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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Collections;



namespace Singonet.ir  // Singonet.ir namespace
{
    namespace SysInfo   // Hardware Info namespace
    {

        /// <summary>
        /// Physical Memories class
        /// </summary>

#if (!SYSINFO_INT)
        public class PhysicalMemories : IEnumerable
#else
            
        internal class PhysicalMemories : IEnumerable
#endif

    {

            /// <summary>
            /// Physyical Memory class
            /// </summary>
            public class PhysicalMemory
            {

                /// <summary>
                /// Physyical Slot which memory is installed
                /// </summary>
                public string Localtion { get; internal set; }

                /// <summary>
                /// Physyical Memory capacity in byte
                /// </summary>
                public ulong Capacity { get; internal set; }

                /// <summary>
                /// Physical Memory Manufacturer
                /// </summary>
                public string Manufacturer { get; internal set; }

                /// <summary>
                /// Physyical Memory Part Number
                /// </summary>
                public string Part_Number { get; internal set; }

                /// <summary>
                /// Physical Memory serial number
                /// </summary>
                public string Serial_Number { get; internal set; }

                /// <summary>
                /// Physyical Memory Bus Speed
                /// </summary>
                public uint Speed { get; internal set; }

                              
            }

            /// <summary>
            /// List of Physyical Memories
            /// </summary>
            public List<PhysicalMemory> Physical_Memories = new List<PhysicalMemory>();

            /// <summary>
            /// Total capacity of all installed memories
            /// </summary>
            public ulong Total_Memory_Capacity { get; internal set; } = 0;

            /// <summary>
            /// Number of all physyical memory active slot(s)
            /// </summary>
            public ushort Total_Memory_Active_Slots { get; internal set; } = 0;


            /// <summary>
            /// Enable Iteration over list
            /// </summary>
            /// <returns></returns>
            public IEnumerator GetEnumerator()
            {
                return Physical_Memories.GetEnumerator();
            }


            /// <summary>
            /// Class constructor
            /// </summary>
            public PhysicalMemories()
            {
                try
                {

                    ManagementObjectSearcher _memories = new ManagementObjectSearcher("select * from Win32_PhysicalMemory");

                    if (_memories.Get().Count == -1)
                    {
                        throw new Exception("Could not get Memory(s) information.");
                    }

                    // Iterate over all memories
                   foreach (ManagementObject _memory in _memories.Get())
                    {

                        PhysicalMemory memInfo = new PhysicalMemory();

                        // Iterate over mempry settings
                        foreach (PropertyData PC in _memory.Properties)
                        {
                            if (PC.Value != null)
                            {
                                if (PC.Name == "Capacity") memInfo.Capacity = Convert.ToUInt64(PC.Value);
                                if (PC.Name == "DeviceLocator") memInfo.Localtion = (string)PC.Value;
                                if (PC.Name == "Manufacturer") memInfo.Manufacturer = (string)PC.Value;
                                if (PC.Name == "PartNumber") memInfo.Part_Number = (string)PC.Value;
                                if (PC.Name == "SerialNumber") memInfo.Serial_Number = (string)PC.Value;
                                if (PC.Name == "Speed") memInfo.Speed = (uint)PC.Value;
                            }
                        }

                        // sum each memory capacity for total capacity
                        Total_Memory_Capacity += memInfo.Capacity;

                        // sum active slot
                        Total_Memory_Active_Slots++;

                        // add memory setting to the memories list
                        Physical_Memories.Add(memInfo);

                    }

                }

                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }

    }

}
