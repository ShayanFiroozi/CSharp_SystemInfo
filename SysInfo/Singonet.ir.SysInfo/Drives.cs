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
using System.Management;
using System.Collections;

namespace Singonet.ir  // Singonet.ir namespace
{
    namespace SysInfo   // Hardware Info namespace
    {


        /// <summary>
        /// Local Logical Drives class
        /// </summary>
        /// 



#if (!SYSINFO_INT)
        public class LogicalDrives  : IEnumerable
            
#else
            internal  class LogicalDrives : IEnumerable
#endif

        {


            /// <summary>
            /// Logical Drive Information
            /// </summary>
            public class LogicalDrive
            {
                /// <summary>
                /// Logical Drive Name
                /// </summary>
                public string Drive_Name { get; internal set; } = "N/A";

                /// <summary>
                /// Logical Drive Volume Name
                /// </summary>
                public string Volume_Name { get; internal set; } = "N/A";

                /// <summary>
                /// Logical Drive Size in byte
                /// </summary>
                public ulong Drive_Size { get; internal set; } = 0;

                /// <summary>
                /// Logical Drive Used Space in byte
                /// </summary>
                public ulong Used_Space { get; internal set; } = 0;

                /// <summary>
                /// Logical Drive free space in bye
                /// </summary>
                public ulong Free_Space { get; internal set; } = 0;

                /// <summary>
                /// Logical Drive file system(NTFS , Fat , Fat32 etc.)
                /// </summary>
                public string Drive_File_System { get; internal set; } = "N/A";

                /// <summary>
                /// Logical Drive Type in numbers ( Fixed , Removable , CDROM and etc)
                /// </summary>
                public ushort Drive_Type { get; internal set; } = 0;

                /// <summary>
                /// Logical drive serial number
                /// </summary>
                public string Drive_Serial_Number { get; internal set; } = "N/A";

                /// <summary>
                /// Indicates this drive is compressed or not ?
                /// </summary>
                public bool Is_Drive_Compressed { get; internal set; } = false;


            }


            /// <summary>
            /// List of Logical Drives in local system
            /// </summary>
            public List<LogicalDrive> Logical_drives = new List<LogicalDrive>();

            /// <summary>
            /// Enable Enumeration over list(foreach...)
            /// </summary>
            /// <returns></returns>
            public IEnumerator GetEnumerator()
            {
                return Logical_drives.GetEnumerator();
            }




            /// <summary>
            /// class constructor
            /// </summary>
            public LogicalDrives()
            {
                try
                {



                    ManagementObjectSearcher drv = new ManagementObjectSearcher("select * from Win32_LogicalDisk");

                    if (drv.Get().Count == -1)
                    {
                        throw new Exception("Could not get Drive(s) information.");
                    }



                    // Iterate over all logical drives
                    foreach (ManagementObject ObjDrv in drv.Get())
                    {
                        LogicalDrive Drv_Info = new LogicalDrive();
                         //Iterate over each logical drive informations
                        foreach (PropertyData PC in ObjDrv.Properties)
                        {
                            if (PC.Value != null)
                            {

                                if(PC.Name=="FileSystem") Drv_Info.Drive_File_System = (string)ObjDrv[PC.Name];

                                if (PC.Name == "Name") Drv_Info.Drive_Name = (string)ObjDrv[PC.Name];

                                if (PC.Name == "VolumeName") Drv_Info.Volume_Name = (string)ObjDrv[PC.Name];

                                if (PC.Name == "VolumeSerialNumber") Drv_Info.Drive_Serial_Number = (string)ObjDrv[PC.Name];

                                if (PC.Name == "Size") Drv_Info.Drive_Size = Convert.ToUInt64(ObjDrv[PC.Name]);

                                if (PC.Name == "DriveType") Drv_Info.Drive_Type =  Convert.ToUInt16(ObjDrv[PC.Name]);

                                if (PC.Name == "FreeSpace") Drv_Info.Free_Space = Convert.ToUInt64(ObjDrv[PC.Name]);

                                if (PC.Name == "Compressed") Drv_Info.Is_Drive_Compressed = (bool)ObjDrv[PC.Name];

                                Drv_Info.Used_Space = Convert.ToUInt64(Drv_Info.Drive_Size - Drv_Info.Free_Space);

                            }
                        }

                         // add current logical drive to the logical drives list
                        Logical_drives.Add(Drv_Info);

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