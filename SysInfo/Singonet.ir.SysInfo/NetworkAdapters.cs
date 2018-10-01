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
        /// Network Adapters class
        /// </summary>
       
#if (!SYSINFO_INT)
        public class NetworkAdapters : IEnumerable
#else

        internal class NetworkAdapters : IEnumerable
#endif

        {

            /// <summary>
            /// Network adapter information class
            /// </summary>
            public class NetworkAdapter
            {
                           
                  /// <summary>
                  /// Adapter name
                  /// </summary>
                    public string Adapter_Name { get; internal set; } = "N/A";
                /// <summary>
                /// adapter MAC address
                /// </summary>
                    public string Adapter_MAC_Address { get; internal set; } = "N/A";
                /// <summary>
                /// Adapter Manufacturer
                /// </summary>
                    public string Adapter_Manufacturer { get; internal set; } = "N/A";
                /// <summary>
                /// Adapter Display name (in control panel)
                /// </summary>
                    public string Adapter_Display_Name { get; internal set; } = "N/A";
                /// <summary>
                /// Is adapter a physical adapter or not ?
                /// </summary>
                    public bool Is_A_Physical_Adapter { get; internal set; } = false;
                /// <summary>
                /// Has apater a enabled network operation ?
                /// </summary>
                    public bool Adapter_Network_Enabled { get; internal set; } = false;
                /// <summary>
                /// Is DHCP service is enbaled for adapter or not ?
                /// </summary>
                    public bool Adapter_DHCP_Enabled { get; internal set; } = false;
                /// <summary>
                /// Is IP filtering enabled for adapter
                /// </summary>
                    public bool Adapter_IP_Filter_Enabled{ get; internal set; } = false;
                /// <summary>
                /// Adapter index in adapers lists
                /// </summary>
                    public uint Adapter_Index { get; internal set; } = 0;
                /// <summary>
                /// Adapter Interface index in adapters index
                /// </summary>
                    public uint Adapter_Interface_Index { get; internal set; } = 0;

                /// <summary>
                /// Adaapter IP address
                /// </summary>
                     public string Adapter_IP_Address { get; internal set; } = "N/A";

                /// <summary>
                /// Adapter subnet mask
                /// </summary>
                    public string Adapter_Subnet_IP { get; internal set; } = "N/A";
  
            }


            /// <summary>
            /// List of local network adapters
            /// </summary>
            public List<NetworkAdapter> Network_Adapters = new List<NetworkAdapter>();

            /// <summary>
            /// Enable itreation for the list(foreach ...)
            /// </summary>
            /// <returns></returns>
            public IEnumerator GetEnumerator()
            {
                return Network_Adapters.GetEnumerator();
            }


            /// <summary>
            /// class constructor
            /// </summary>
            public NetworkAdapters()
            {
                try
                {

                    ManagementObjectSearcher _adapters = new ManagementObjectSearcher("select * from Win32_NetworkAdapter");

                    if (_adapters.Get().Count == -1)
                    {
                        throw new Exception("Could not get NetworkAdapter(s) information.");
                    }

                    // Iterate over network adapters
                    foreach (ManagementObject _adapter in _adapters.Get())
                    {


                        NetworkAdapter _adapter_info = new NetworkAdapter();

                        // iterate over network adapter settings
                        foreach (PropertyData PC in _adapter.Properties)
                        {
                            if (PC.Value != null)
                            {
                               
                                if (PC.Name == "NetConnectionID") _adapter_info.Adapter_Display_Name = (string)PC.Value;
                                if (PC.Name == "MACAddress") _adapter_info.Adapter_MAC_Address = (string)PC.Value;
                                if (PC.Name == "Manufacturer") _adapter_info.Adapter_Manufacturer = (string)PC.Value;
                                if (PC.Name == "Name") _adapter_info.Adapter_Name = (string)PC.Value;
                                if (PC.Name == "PhysicalAdapter") _adapter_info.Is_A_Physical_Adapter = (bool)PC.Value;
                                if (PC.Name == "NetEnabled") _adapter_info.Is_A_Physical_Adapter = (bool)PC.Value;
                                if (PC.Name == "Index") _adapter_info.Adapter_Index = (uint)PC.Value;
                                if (PC.Name == "InterfaceIndex") _adapter_info.Adapter_Interface_Index = (uint)PC.Value;

                            }
                        }

                        // Get configuration settings for each network adapter
                        ManagementObject _adapters_settings = new ManagementObjectSearcher("select * from Win32_NetworkAdapterConfiguration where Index=" + 
                            _adapter_info.Adapter_Index +  " And InterfaceIndex=" + _adapter_info.Adapter_Interface_Index).Get().Cast<ManagementObject>().First();

                        // Iterate over network adapter configuration settings
                        foreach (PropertyData PC in _adapters_settings.Properties)
                        {
                            if (PC.Value != null)
                            {
                                if (PC.Name == "DHCPEnabled") _adapter_info.Adapter_DHCP_Enabled = (bool)PC.Value;
                                if (PC.Name == "IPFilterSecurityEnabled") _adapter_info.Adapter_IP_Filter_Enabled = (bool)PC.Value;
                                if (PC.Name == "IPAddress") _adapter_info.Adapter_IP_Address = ((string[]) PC.Value).ElementAt(0);
                                if (PC.Name == "IPSubnet") _adapter_info.Adapter_Subnet_IP = ((string[])PC.Value).ElementAt(0);

                            }
                        }

                        // Add current network adapter to the network adapters list
                        Network_Adapters.Add(_adapter_info);

                   
                    }

                }

                catch (Exception ex)
                {
                    throw ex;
                }

            }

            /// <summary>
            /// Indicates is an adapter exists on local system or not
            /// </summary>
            /// <param name="adapter_name">Adapter name , can be adapter's real name or adapter's display name</param>
            /// <returns>true or false</returns>
            public bool Is_Adapter_Exists(string adapter_name)
            {
                foreach (NetworkAdapter _adapter in Network_Adapters)

                {
                    if ((_adapter.Adapter_Name == adapter_name) || (_adapter.Adapter_Display_Name == adapter_name))
                    {
                        return true;
                    }
                }
                return false;
            }




        }


    }

}
