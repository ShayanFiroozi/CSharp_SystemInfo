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
        /// Processes class
        /// </summary>
        /// 
#if (!SYSINFO_INT)

        public class Processes : IEnumerable
#else
        internal class Processes : IEnumerable
#endif
        {

            /// <summary>
            /// Process class
            /// </summary>
            public class Process
            {
                /// <summary>
                /// Process name
                /// </summary>
                public string Process_Name { get; internal set; } = "N/A";
                /// <summary>
                /// Process executable file path
                /// </summary>
                public string Process_Path { get; internal set; }  = "N/A";

                /// <summary>
                /// Process Start Date Time
                /// </summary>
                public DateTime Process_Start_Date_Time { get; internal set; }

                /// <summary>
                /// Process Start Time 
                /// </summary>
                public string Process_Start_Date_Time_Persian { get; internal set; } = "N/A";
                /// <summary>
                /// Process running time in seconds
                /// </summary>
                public double Process_Runtime_seconds { get; internal set; } = 0;
                /// <summary>
                /// Process system handle
                /// </summary>
                public string Process_Handle { get; internal set; } = "N/A";
                /// <summary>
                /// Process unique identification
                /// </summary>
                public uint Process_ID { get; internal set; } = 0;
                /// <summary>
                /// Process priority in OS jobs
                /// </summary>
                public uint Process_Priority { get; internal set; } = 0;
                /// <summary>
                /// Process IO read in bytes
                /// </summary>
                public ulong Process_IO_Read_Count { get; internal set; } = 0;
                /// <summary>
                /// Process write in bytes
                /// </summary>
                public ulong Process_IO_Write_Count { get; internal set; } = 0;
                /// <summary>
                /// Process Thread count(sub process)
                /// </summary>
                public uint Process_Thread_Count { get; internal set; } = 0;

            }


            /// <summary>
            /// Local process list
            /// </summary>
            public List<Process> Local_Processes = new List<Process>();

            /// <summary>
            /// Enbale iteration over list
            /// </summary>
            /// <returns></returns>
            public IEnumerator GetEnumerator()
            {
                return Local_Processes.GetEnumerator();
            }


            /// <summary>
            /// Class constructor
            /// </summary>
            public Processes()
            {
                try
                {

                    ManagementObjectSearcher _processes = new ManagementObjectSearcher("select * from Win32_Process");

                    
                    if (_processes.Get().Count == -1)
                    {
                        throw new Exception("Could not get Local Processes information.");
                    }

                    // Iterate over processes
                    foreach(ManagementObject proc in _processes.Get())
                    {

                        Process _process = new Process();

                        // Iterate over process settings
                        foreach (PropertyData PC in proc.Properties)
                        {
                            if (PC.Value != null)
                            {
                                 if (PC.Name == "Handle") _process.Process_Handle = (string)proc[PC.Name];
                              
                                if (PC.Name == "ProcessId") _process.Process_ID = (uint)proc[PC.Name];
                                if (PC.Name == "ReadOperationCount") _process.Process_IO_Read_Count = (ulong)proc[PC.Name];
                                if (PC.Name == "WriteOperationCount") _process.Process_IO_Write_Count = (ulong)proc[PC.Name];
                                if (PC.Name == "Name") _process.Process_Name = (string)proc[PC.Name];
                                if (PC.Name == "ExecutablePath") _process.Process_Path = (string)proc[PC.Name];
                                if (PC.Name == "Priority") _process.Process_Priority = (uint)proc[PC.Name];
                                if (PC.Name == "ThreadCount") _process.Process_Thread_Count = (uint)proc[PC.Name];

                                // claculate process start time(in gregorial and persian claendar)
                                if (PC.Name == "CreationDate")
                                {
                                    // Remove uneccessary characters from string
                                    string _temp_proc_date = proc[PC.Name].ToString().Remove(proc[PC.Name].ToString().LastIndexOf('.'));

                                   
                                    int year =  Convert.ToInt32(_temp_proc_date.Substring(0, 4));
                                    int month = Convert.ToInt32(_temp_proc_date.Substring(4, 2));
                                    int day = Convert.ToInt32(_temp_proc_date.Substring(6, 2));

                                    int hour = Convert.ToInt32(_temp_proc_date.Substring(8, 2));
                                    int minute = Convert.ToInt32(_temp_proc_date.Substring(10, 2));
                                    int second = Convert.ToInt32(_temp_proc_date.Substring(12, 2));

                                    DateTime proc_time = new DateTime(year, month, day, hour, minute, second);

                                    // Gergorian calendar
                                    _process.Process_Start_Date_Time = proc_time;

                                    //Persian calendar
                                  //  _process.Process_Start_Date_Time_Persian = CoreDateTime.Gregorian_To_Persian(_process.Process_Start_Date_Time);

                                    // Calculate process runtime ( Current Date time - process start date time)
                                    TimeSpan span = (DateTime.Now - _process.Process_Start_Date_Time);
                                    _process.Process_Runtime_seconds  = Convert.ToDouble(span.TotalSeconds);
                                }

                            }
                        }

                        // Add process to the processes list
                        Local_Processes.Add(_process);
                    }

                }
                catch (Exception ex)
                {
                    throw ex;

                }
            }


            /// <summary>
            /// Is process running or not ?
            /// </summary>
            /// <param name="process_name_or_file_path">Process name (for ex : smss.exe) or process executable file full path</param>
            /// <returns>Boolean ( True or False)</returns>
            public bool Is_process_running(string process_name_or_file_path)
            {
               
                foreach (Process _prc in Local_Processes)
                {
                    if( _prc.Process_Name==process_name_or_file_path || _prc.Process_Path==process_name_or_file_path )
                    {
                        return true;
                        
                    }
                }
                return false;
            }

          


        }
    }
}