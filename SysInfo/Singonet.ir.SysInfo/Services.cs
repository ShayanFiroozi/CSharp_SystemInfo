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
        /// Services class
        /// </summary>
        /// 
#if (!SYSINFO_INT)
        public class Services : IEnumerable
#else

       internal class Services : IEnumerable
#endif

        {

            /// <summary>
            /// Service class
            /// </summary>
            public class Service
            {
                public string Service_Name { get; internal set; } = "N/A";
                public string Display_Name { get; internal set; } = "N/A";
                public string Service_Description { get; internal set; } = "N/A";
                public string Service_Path { get; internal set; } = "N/A";
                public bool Service_Stop_Ability { get; internal set; } = false;
                public bool Service_Pause_Ability { get; internal set; } = false;
                public bool Is_Service_Started { get; internal set; } = false;
                public string Startup_Mode { get; internal set; } = "N/A";
                public string Service_Start_Account { get; internal set; } = "N/A";
                public string Service_State { get; internal set; } = "N/A";
                public uint Parent_Process_ID { get; internal set; } = 0;


            }


            /// <summary>
            /// Local services list
            /// </summary>
            public List<Service> Local_Services = new List<Service>();

            /// <summary>
            ///  Enable iteration over list
            /// </summary>
            /// <returns></returns>
            public IEnumerator GetEnumerator()
            {
                return Local_Services.GetEnumerator();
            }

            /// <summary>
            /// Class constructor
            /// </summary>
            public Services()
            {
                try
                {

                    ManagementObjectSearcher _services = new ManagementObjectSearcher("select * from Win32_Service");

                    if (_services.Get().Count == -1)
                    {
                        throw new Exception("Could not get Local Services information.");
                    }

                    // Iterate over local services
                    foreach(ManagementObject srv in _services.Get())
                    {


                        Service _service = new Service();

                        // Iterate over service settings
                        foreach (PropertyData PC in srv.Properties)
                        {
                            if (PC.Value != null)
                            {
                                if (PC.Name == "Started") _service.Is_Service_Started = (bool)srv[PC.Name];
                                if (PC.Name == "ProcessId") _service.Parent_Process_ID = (uint)srv[PC.Name];
                                if (PC.Name == "Name") _service.Service_Name = (string)srv[PC.Name];
                                if (PC.Name == "PathName") _service.Service_Path = (string)srv[PC.Name];
                                if (PC.Name == "AcceptPause") _service.Service_Pause_Ability = (bool)srv[PC.Name];
                                if (PC.Name == "StartName") _service.Service_Start_Account = (string)srv[PC.Name];
                                if (PC.Name == "State") _service.Service_State = (string)srv[PC.Name];
                                if (PC.Name == "AcceptStop") _service.Service_Stop_Ability = (bool)srv[PC.Name];
                                if (PC.Name == "StartMode") _service.Startup_Mode = (string)srv[PC.Name];
                                if (PC.Name == "DisplayName") _service.Display_Name = (string)srv[PC.Name];
                                if (PC.Name == "Description") _service.Service_Description = (string)srv[PC.Name];

                            }
                        }

                        // Add service to the Local services list
                        Local_Services.Add(_service);
                    }

                }
                catch (Exception ex)
                {
                    throw ex;

                }

            }


            /// <summary>
            /// Indicates service is running or not ?
            /// </summary>
            /// <param name="service_name">Service name or Service Display name</param>
            /// <returns>Boolean (True or False)</returns>
            public bool Is_Service_Running(string service_name)
            {
                foreach (Service _srv in Local_Services)
                {
                    if ((_srv.Service_Name==service_name || _srv.Display_Name==service_name) && _srv.Service_State=="Running")
                    {
                        return true;
                    }
                }
                return false;
            }


        }
    }
}