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
using System.Collections;
using System.Management;


namespace Singonet.ir  // Singonet.ir namespace
{
    namespace SysInfo   // Hardware Info namespace
    {


        /// <summary>
        /// Physical Disks class
        /// </summary>


#if (!SYSINFO_INT)
        public class PhysicalDisks : IEnumerable
#else
        internal class PhysicalDisks : IEnumerable
#endif

    {

            /// <summary>
            /// Physical Disks List
            /// </summary>
            public List<PhysicalDisk> Physical_Disks = new List<PhysicalDisk>();

            /// <summary>
            /// Enable Iteration over List
            /// </summary>
            /// <returns></returns>
            public IEnumerator GetEnumerator()
            {
                return Physical_Disks.GetEnumerator();
            }

            /// <summary>
            /// Physical Disk class
            /// </summary>
            public class PhysicalDisk : IEnumerable
            {
                /// <summary>
                /// Physyical Disk Information class
                /// </summary>
                public string Disk_Name { get; internal set; } = "N/A";

                /// <summary>
                /// Physyical Disk interface type
                /// </summary>
                public string Disk_Interface_Type { get; internal set; } = "N/A";

                /// <summary>
                /// Physyical Disk media type
                /// </summary>
                public string Disk_Media_Type { get; internal set; } = "N/A";

                /// <summary>
                /// Physyical Disk partition counts
                /// </summary>
                public uint Disk_Partition_Count { get; internal set; } = 0;

                /// <summary>
                /// Physyical Disk serial number
                /// </summary>
                public string Disk_Serial_Number { get; internal set; } = "N/A";

                /// <summary>
                /// Physyical Disk size in bytes
                /// </summary>
                public ulong Disk_Size { get; internal set;} = 0;
               
                /// <summary>
                /// Physyical Disk Index in disk array
                /// </summary>
                public uint Disk_Index { get; internal set; } = 0;

                /// <summary>
                /// Disk Partitions list
                /// </summary>
                public List<Partition> Disk_Partitions = new List<Partition>();

                public IEnumerator GetEnumerator()
                {
                    return Disk_Partitions.GetEnumerator();
                }

                /// <summary>
                /// Physical Disk Partition information class
                /// </summary>
                    public class Partition
                    {

                    /// <summary>
                    /// Physyical Disk Partition Name
                    /// </summary>
                        public string Partition_Name { get; internal set; } = "N/A";

                    /// <summary>
                    /// Is this partition a boot partition ?
                    /// </summary>
                    public bool Is_Boot_Partition { get; internal set; } = false;

                    /// <summary>
                    /// Is this partition is a primary partition
                    /// </summary>
                        public bool Is_Primary_Partition { get; internal set; } = false;

                    /// <summary>
                    /// Physyical Disk partition size in byte
                    /// </summary>
                        public ulong Partition_Size { get; internal set; } = 0;

                    /// <summary>
                    /// Physical Disk Partition type
                    /// </summary>
                        public string Partition_Type { get; internal set; } = "N/A";


                }

            }


           /// <summary>
           /// class constructor
           /// </summary>
            public PhysicalDisks()
            {
                try
                {
                     
                    ManagementObjectSearcher _HDDS = new ManagementObjectSearcher("select * from Win32_DiskDrive");

                    if (_HDDS.Get().Count == -1)
                    {
                        throw new Exception("Could not get HDD(s) information.");
                    }

                    
                    // Iterate over Physical Disks
                    foreach (ManagementObject _Hdd in _HDDS.Get())
                    {


                        PhysicalDisk Hdd_Info = new PhysicalDisk();

                        // Iterate over physical disk info
                        foreach (PropertyData PC in _Hdd.Properties)
                        {
                            if (PC.Value != null)
                            {

                               if(PC.Name =="InterfaceType") Hdd_Info.Disk_Interface_Type = (string)_Hdd[PC.Name];
                               if (PC.Name == "MediaType")  Hdd_Info.Disk_Media_Type = (string)_Hdd[PC.Name];
                               if (PC.Name == "Name") Hdd_Info.Disk_Name = (string)_Hdd[PC.Name];
                               if (PC.Name == "Partitions") Hdd_Info.Disk_Partition_Count = (uint)_Hdd[PC.Name];
                               if (PC.Name == "SerialNumber") Hdd_Info.Disk_Serial_Number = (string)_Hdd[PC.Name];
                               if (PC.Name == "Size") Hdd_Info.Disk_Size = Convert.ToUInt64(_Hdd[PC.Name]);
                               if (PC.Name == "Index") Hdd_Info.Disk_Index = Convert.ToUInt32(_Hdd[PC.Name]);
                            }
                        }

                       

                        // get physical disk partition information
                        ManagementObjectSearcher partition_info;

                        if (new ManagementObjectSearcher("select * from Win32_DiskPartition where DiskIndex="
                                                                      + Hdd_Info.Disk_Index.ToString()).Get().Count != 0)
                        {
                            partition_info = new ManagementObjectSearcher("select * from Win32_DiskPartition where DiskIndex=" + Hdd_Info.Disk_Index.ToString());

                            // Iterate over Physical disk partitions
                            foreach (ManagementObject ObjPartition in partition_info.Get())
                            {

                                PhysicalDisk.Partition hdd_partition_info = new PhysicalDisk.Partition();

                                // Iterate over Physical disk partition information
                                foreach (PropertyData PC in ObjPartition.Properties)
                                {
                                    if (PC.Value != null)
                                    {

                                        if (PC.Name == "Name") hdd_partition_info.Partition_Name = (string)ObjPartition[PC.Name];

                                        if (PC.Name == "BootPartition") hdd_partition_info.Is_Boot_Partition = (bool)ObjPartition[PC.Name];

                                        if (PC.Name == "PrimaryPartition") hdd_partition_info.Is_Primary_Partition = (bool)ObjPartition[PC.Name];

                                        if (PC.Name == "Size") hdd_partition_info.Partition_Size = Convert.ToUInt64(ObjPartition[PC.Name]);

                                        if (PC.Name == "Type") hdd_partition_info.Partition_Type = (string)ObjPartition[PC.Name];

                                    }
                                }

                                // Add Partition information to the list
                                 Hdd_Info.Disk_Partitions.Add(hdd_partition_info);
                              
                            }
                        }

                        // Add Physyical disk to the Physical Disks list
                        Physical_Disks.Add(Hdd_Info); // add current Disk Drive information to the list

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

