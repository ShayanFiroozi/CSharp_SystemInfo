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
        /// Class about getting BIOS Information
        /// </summary>

#if (!SYSINFO_INT)
        public class BIOS
#else
        internal class BIOS
#endif

        {

            private ManagementObject bios;

            /// <summary>
            /// BIOS class constructor
            /// </summary>
            public BIOS()
            {
                try
                {

                    bios = new ManagementObjectSearcher("select * from Win32_BIOS").Get().Cast<ManagementObject>().First();

                    if (((string)bios["Version"]).Length == 0)
                    {
                        throw new Exception("Could not get BIOS information.");
                    }

                }

                catch (Exception ex)
                {
                    throw ex;
                }

            }

            /// <summary>
            /// returns BIOS Version
            /// </summary>
            public string BIOS_Version
            {
                get
                {
                    return (string)bios["Version"];
                }
            }


            /// <summary>
            /// returns BIOS Manufacturer
            /// </summary>
            public string BIOS_Manufacturer
            {
                get
                {
                    return (string)bios["Manufacturer"];
                }
            }


            /// <summary>
            /// returns BIOS Manufacturer
            /// </summary>
            public bool BIOS_is_primary_BIOS
            {
                get
                {
                    return (bool)bios["PrimaryBIOS"];
                }
            }


            /// <summary>
            /// returns BIOS Release Date
            /// </summary>
            public string BIOS_Release_Date
            {
                get
                {
                    return (string)bios["ReleaseDate"];
                }
            }


        }
    }
}


