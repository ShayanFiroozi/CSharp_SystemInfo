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
        /// Class about getting Main Board information
        /// </summary>
#if (!SYSINFO_INT)
        public class Main_Board

#else
            internal  class Main_Board
#endif

        {

            private ManagementObject mb;

            /// <summary>
            /// Main Board class constructor
            /// </summary>
            public Main_Board()
            {
                try
                {

                    mb = new ManagementObjectSearcher("select * from Win32_BaseBoard").Get().Cast<ManagementObject>().First();

                    if (((string)mb["Manufacturer"]).Length == 0)
                    {
                        throw new Exception("Could not get Main Board information.");
                    }

                }

                catch (Exception ex)
                {
                    throw ex;
                }

            }



            /// <summary>
            /// returns Mother Board Version
            /// </summary>
            public string MB_Manufacturer
            {
                get
                {
                    return (string)mb["Manufacturer"];
                }
            }


            /// <summary>
            /// returns Mother Board Product
            /// </summary>
            public string MB_Product
            {
                get
                {
                    return (string)mb["Product"];
                }
            }

        }

    }
}