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
        /// Printers class
        /// </summary>
#if (!SYSINFO_INT)
        public class Printers : IEnumerable
#else

        internal class Printers : IEnumerable
#endif
        {
            /// <summary>
            /// Printer class
            /// </summary>
            public class Printer
            {
       
                /// <summary>
                /// Printer Name
                /// </summary>
                public string Printer_Name { get; internal set; }

                /// <summary>
                /// Is printer is the default printer
                /// </summary>
                public bool Is_Printer_Default { get; internal set; }

                /// <summary>
                /// Is printer is a local printer or not(shared or remote) ?
                /// </summary>
                public bool Is_Prnter_On_Local { get; internal set; }

                /// <summary>
                /// Is printer shared with others on network ?
                /// </summary>
                public bool Is_Printer_Shared { get; internal set; }



                /// <summary>
                /// Printer settings class
                /// </summary>
                public class Printer_Setting
                {

                    public uint Copies { get; internal set; } = 0;

                    public uint Color { get; internal set; } = 0;

                    public bool Duplex { get; internal set; } = false;

                    public string Form_Name { get; internal set; } = "N/A";

                    public uint Horizontal_Resolution { get; internal set; } = 0;

                    public uint Orientation { get; internal set; } = 0;

                    public uint Paper_Length { get; internal set; } = 0;

                    public string Paper_Szie { get; internal set; } = "N/A";

                    public uint Paper_Width { get; internal set; } = 0;

                    public uint Print_Quality { get; internal set; } = 0;

                }


                /// <summary>
                /// Printer settings
                /// </summary>
                public Printer_Setting Printer_Settings = new Printer_Setting();

             
            }


           /// <summary>
           /// List of printers
           /// </summary>
          public List<Printer> Printers_List = new  List<Printer>();

            /// <summary>
            /// Iterate over list
            /// </summary>
            /// <returns></returns>
            public IEnumerator GetEnumerator()
            {
                return Printers_List.GetEnumerator();
            }


           
            /// <summary>
            /// classs conscturtor
            /// </summary>
            public Printers()
            {
                try
                {
                   
                    ManagementObjectSearcher _printers = new ManagementObjectSearcher("select * from Win32_Printer");

                    if (_printers.Get().Count == -1)
                    {
                        throw new Exception("Could not get Printer(s) information.");
                    }

                    // Iterate over printers

                    foreach (ManagementObject _printer in _printers.Get())
                    {


                       Printer pInfo = new Printer();

                        // Iterate over printer settings

                        foreach (PropertyData PC in _printer.Properties)
                        {
                            if (PC.Value != null)
                            {
                                if (PC.Name == "Name") pInfo.Printer_Name = (string)PC.Value;
                                if (PC.Name == "Shared") pInfo.Is_Printer_Shared = (bool)PC.Value;
                                if (PC.Name == "Local") pInfo.Is_Prnter_On_Local = (bool)PC.Value;
                                if (PC.Name == "Default") pInfo.Is_Printer_Default = (bool)PC.Value;
                            }
                        }


                        // Get each printer configuration
                        ManagementObject pr_setting;
                            pr_setting = new ManagementObjectSearcher("select * from Win32_PrinterConfiguration where name='" 
                                                                        + pInfo.Printer_Name +"'").Get().Cast<ManagementObject>().First();


                        //Iterate over printer configuration settings
                        foreach (PropertyData PC in pr_setting.Properties)
                        {
                            if (PC.Value != null)
                            {

                                if (PC.Name == "Color") pInfo.Printer_Settings.Color = (uint)PC.Value;
                                if (PC.Name == "Copies") pInfo.Printer_Settings.Copies = (uint)PC.Value;
                                if (PC.Name == "Duplex") pInfo.Printer_Settings.Duplex = (bool)PC.Value;
                                if (PC.Name == "FormName") pInfo.Printer_Settings.Form_Name = (string)PC.Value;
                                if (PC.Name == "HorizontalResolution") pInfo.Printer_Settings.Horizontal_Resolution = (uint)pr_setting[PC.Name];
                                if (PC.Name == "Orientation") pInfo.Printer_Settings.Orientation = (uint)pr_setting[PC.Name];
                                if (PC.Name == "PaperLength") pInfo.Printer_Settings.Paper_Length = (uint)pr_setting[PC.Name];
                                if (PC.Name == "PaperSize") pInfo.Printer_Settings.Paper_Szie = (string)pr_setting[PC.Name];
                                if (PC.Name == "PaperWidth") pInfo.Printer_Settings.Paper_Width = (uint)pr_setting[PC.Name];
                                if (PC.Name == "PrintQuality") pInfo.Printer_Settings.Print_Quality = (uint)pr_setting[PC.Name];
                            }
                        }


                        // add current printer information to the list
                          Printers_List.Add(pInfo); 

                    }
  
                }

                catch (Exception ex)
                {
                    throw ex;
                }

            }

            /// <summary>
            /// Indicates is this printer installed or not
            /// </summary>
            /// <param name="printer_name">Printer name</param>
            /// <returns>Boolean (True or false)</returns>
            public bool Is_printer_exists(string printer_name)
            {
                foreach (Printer _pr in Printers_List)

                {
                    if(_pr.Printer_Name==printer_name)
                    {
                        return true;
                    }
                }
                return false;
            }


      

        }


    }
       
}
