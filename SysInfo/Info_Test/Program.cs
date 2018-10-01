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
using Singonet.ir.SysInfo;
namespace HW_Info_Test
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Console.WriteLine("SingoNet System Information");
            Console.WriteLine("Written and Developed by Shayan Firoozi 2016 Bandar Abbas - Iran");
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Phone : +98(936)517-5800");
            Console.WriteLine("Email : Shayan.Firoozi@gmail.com");
            Console.WriteLine("Email : SingoNet.ir@gmail.com");
            Console.WriteLine("Telegram Channel : https://t.me/SingoNetHormoz");
            Console.WriteLine("Telegram ID : @SingoNet_ir");
            



            Console.WriteLine(new string('~',120));


            Console.WriteLine(Environment.NewLine);
         





            // Print BIOS Info
            Console.WriteLine(new string('*',20) + " BIOS Info " +  new string('*',20) + Environment.NewLine + Environment.NewLine +
                                  "BIOS Manufacture : " + new BIOS().BIOS_Manufacturer + Environment.NewLine + 
                                  "BIOS Release Date : " + new BIOS().BIOS_Release_Date + Environment.NewLine + 
                                  "BIOS Verison : " + new BIOS().BIOS_Version  + Environment.NewLine +Environment.NewLine +
                                   new string('*', 51) + Environment.NewLine);


           


            // Print Main Board Info
              Console.WriteLine(new string('*', 20) + " Main Board Info " + new string('*', 20) + Environment.NewLine + Environment.NewLine +
                                "Main Board Manufacture : " + new Main_Board().MB_Manufacturer + Environment.NewLine +
                                "Main Board Product : " + new Main_Board().MB_Product + Environment.NewLine +Environment.NewLine +
                                new string('*', 51) + Environment.NewLine);



        



            // Print CPU Info

            Console.WriteLine("Please wait loading CPU Info..." + Environment.NewLine);

            Console.WriteLine(new string('*', 20) + " CPU Info " + new string('*', 20) + Environment.NewLine + Environment.NewLine +
                              "CPU ID : " + new CPU().CPU_ID + Environment.NewLine +
                              "CPU Name : " + new CPU().CPU_Name + Environment.NewLine +
                              "CPU Speed : " + new CPU().CPU_Max_Speed_MHZ.ToString() + " MHZ"  + Environment.NewLine +
                              "CPU Core(s) : " + new CPU().CPU_Number_Of_Cores.ToString() + Environment.NewLine +
                              "CPU Logical Processor(s) : " + new CPU().CPU_Number_Of_Logical_Processors.ToString() + Environment.NewLine +
                              "CPU Manufacturer : " + new CPU().CPU_Manufacturer + Environment.NewLine +
                              "CPU Architecture : " + new CPU().CPU_Architecture.ToString() + Environment.NewLine +
                              "CPU L2 Cache : " + new CPU().CPU_L2_Cache_Size.ToString() + Environment.NewLine +
                              "CPU L3 Cache : " + new CPU().CPU_L3_Cache_Size.ToString() + Environment.NewLine +
                              "CPU Description : " + new CPU().CPU_Description.ToString() + Environment.NewLine +  Environment.NewLine +
                              new string('*', 51) + Environment.NewLine);



            Console.Read();

            // Print RAM Info
            Console.WriteLine(new string('*', 20) + " Physical Memory Info " + new string('*', 20) + Environment.NewLine + Environment.NewLine +
                   "Physical Memory Active Slot(s) : " + new PhysicalMemories().Total_Memory_Active_Slots.ToString() + Environment.NewLine +
                   "Physical Memory Total Capacity :  " + string.Format("{0:#,###0}",new PhysicalMemories().Total_Memory_Capacity)  + " bytes. " 
                   + Environment.NewLine + Environment.NewLine +
                   new string('*', 51) + Environment.NewLine);




           


            // Print Physical Disk(s) Info
            Console.WriteLine(new string('*', 20) + " Physical Disk(s) Info " + new string('*', 20) + Environment.NewLine + Environment.NewLine +
                   "Physical Disk(s) Count : " + new PhysicalDisks().Physical_Disks.Count.ToString() + Environment.NewLine);


            // Loop over each physical disk
              Console.Write(Environment.NewLine);
         

            foreach (var physical in new PhysicalDisks().Physical_Disks)
            {
                Console.WriteLine(new string('-', 50));

                Console.Write(Environment.NewLine);
                
                    Console.WriteLine("Disk Index : " + physical.Disk_Index.ToString());
                    Console.WriteLine("Disk Name : " + physical.Disk_Name);
                    Console.WriteLine("Disk Disk Serial Number : " + physical.Disk_Serial_Number);

                    Console.WriteLine("Disk Interface Type : " + physical.Disk_Interface_Type);
                    Console.WriteLine("Disk Media Type : " + physical.Disk_Media_Type);
                    Console.WriteLine("Disk Partition Count : " + physical.Disk_Partition_Count.ToString() + " Partition(s).");
                    Console.WriteLine("Disk Disk Size : " + string.Format("{0:#,###0}",physical.Disk_Size) + " bytes.");

                Console.Write(Environment.NewLine);

                Console.WriteLine(new string('-', 50));
            }

            Console.Write(Environment.NewLine);
            
      
                Console.WriteLine(new string('*', 70));

            Console.Write(Environment.NewLine);



        


            // Print Logical Drive(s) Info

            Console.WriteLine(new string('*', 20) + " Logical Drive(s) Info " + new string('*', 20) + Environment.NewLine + Environment.NewLine +
                   "Logical Drive(s) Count : " + new LogicalDrives().Logical_drives.Count.ToString() + Environment.NewLine);


            // Loop over each logical disk
            Console.Write(Environment.NewLine);


            foreach (var logical in new LogicalDrives().Logical_drives)
            {
                Console.WriteLine(new string('-', 50));

                Console.Write(Environment.NewLine);

                Console.WriteLine("Logical Drive Name : " + logical.Drive_Name);
                Console.WriteLine("Logical Volume Name : " + logical.Volume_Name);
                Console.WriteLine("Logical Drive File System : " + logical.Drive_File_System);
                Console.WriteLine("Logical Drive Serial Number : " + logical.Drive_Serial_Number);

                Console.WriteLine("Logical Drive Size : " + string.Format("{0:#,###0}", logical.Drive_Size));
                Console.WriteLine("Logical Drive Type : " + logical.Drive_Type.ToString());
                Console.WriteLine("Logical Drive Free Space : " + string.Format("{0:#,###0}",logical.Free_Space) + " byte(s)");
                Console.WriteLine("Logical Drive Used Space : " + string.Format("{0:#,###0}", logical.Used_Space) + " byte(s)");
                Console.WriteLine("Logical Drive Is Compressed : " + logical.Is_Drive_Compressed.ToString());
                

                Console.Write(Environment.NewLine);

                Console.WriteLine(new string('-', 50));
            }

            Console.Write(Environment.NewLine);


            Console.WriteLine(new string('*', 70));

            Console.Write(Environment.NewLine);





            Console.WriteLine("Press any key to exit...");

            Console.Read();
            
        }
    }
}
