using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Management;
using NetFwTypeLib;
using System.Reflection;

namespace NetworkTool
{
    /// A Helper class which provides convenient methods to set/get network configuration
    /// The original code that was a foundation for my code came from some open source place:
    /// https://www.codeproject.com/Articles/6975/SwitchNetConfig-Laptop-users-quickly-switch-networ
    /// Since the original code was missing a method to just set DNS, I had to add that. Changed the code over to using blocks and cleaned it up.
    /// Also I modified the class that aquires the information. It will now return a helper class. This should help should I ever need to do storing of this data.
    /// Several of these methods I do not use and might never use. Just will keep them here...never know.

    /// NIC Name/GUID
    public class NIC
    {
        public string NicName = "";
        public string Guid = "";
    }

    /// Networking Data Class
    public class NetworkData
    {
        public List<string> IPaddresses = new List<string>();
        public List<string> Subnets = new List<string>();
        public List<string> Gateways = new List<string>();
        public List<string> DNSes = new List<string>();
        public List<string> DNSSuffixes = new List<string>();
        public bool DHCP = false;
        public bool StaticIP = false;
        public bool StaticDNS = false;

        public BindingData Bindings = new BindingData();
    }

    /// Binding Data Class
    public class BindingData
    {
        public BindingStatus CfMN = BindingStatus.NotInstalled;
        public BindingStatus FaPSfMN = BindingStatus.NotInstalled;
        public BindingStatus LLDP = BindingStatus.NotInstalled;
    }

    public enum BindingStatus
    {
        InstalledEnabled,
        InstalledDisabled,
        NotInstalled,
    }

    public class WMINetworkHelper
    {
        // This code should be used if not loading DLLs on application load.
        
        //public WMINetworkHelper()
        //{
        //    try
        //    {
        //        AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
        //        {
        //            string resourceName = new AssemblyName(args.Name).Name + ".dll";
        //            string resource = Array.Find(this.GetType().Assembly.GetManifestResourceNames(), element => element.EndsWith(resourceName));

        //            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
        //            {
        //                Byte[] assemblyData = new Byte[stream.Length];
        //                stream.Read(assemblyData, 0, assemblyData.Length);
        //                return Assembly.Load(assemblyData);
        //            }
        //        };
        //    }
        //    catch (Exception Ex)
        //    {
        //        LogWriter.Exception("Error loading embedded assembly resource. Application is about to crash.", Ex, true);
        //    }
        //}

        /// <summary>
        /// Sets Just the DNS for the specified network card name
        /// This is MY own code and added it since the original code was missing a method to just set the DNS.
        /// </summary>
        /// <param name="nic">Name of the NIC struct</param>
        /// <param name="DnsSearchOrder">Comma delimited DNS IP</param>
        public bool SetDNS(NIC nic, string[] DnsSearchOrder)
        {
            bool success = false;

            using (ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration"))
            {
                using (ManagementObjectCollection moc = mc.GetInstances())
                {
                    foreach (ManagementObject mo in moc)
                    {
                        // Make sure this is a IP enabled device. Not something like memory card or VM Ware
                        if ((bool)mo["IPEnabled"])
                        {
                            if (mo["SettingID"].Equals(nic.Guid))
                            {
                                try
                                {
                                    using (ManagementBaseObject newDNS = mo.GetMethodParameters("SetDNSServerSearchOrder"))
                                    {
                                        if (newDNS != null)
                                        {
                                            newDNS["DNSServerSearchOrder"] = DnsSearchOrder; //.Split(',');
                                            mo.InvokeMethod("SetDNSServerSearchOrder", newDNS, null);

                                            success = true;
                                        }
                                    }
                                }
                                catch (Exception Ex)
                                {
                                    LogWriter.Exception("Error trying to set DNS.", Ex);
                                }
                            }
                        }
                    }
                }
            }

            return success;
        }

        public bool SetDynamicDNS(NIC nic)
        {
            bool success = true;

            using (ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration"))
            {
                using (ManagementObjectCollection moc = mc.GetInstances())
                {
                    foreach (ManagementObject mo in moc)
                    {
                        // Make sure this is a IP enabled device. Not something like memory card or VM Ware
                        if ((bool)mo["IPEnabled"])
                        {
                            if (mo["SettingID"].Equals(nic.Guid))
                            {
                                try
                                {
                                    using (ManagementBaseObject newDNS = mo.GetMethodParameters("SetDNSServerSearchOrder"))
                                    {
                                        newDNS["DNSServerSearchOrder"] = null;
                                        mo.InvokeMethod("SetDNSServerSearchOrder", newDNS, null);

                                        success = true;
                                    }
                                }
                                catch (Exception Ex)
                                {
                                    LogWriter.Exception("Error trying to set DNS.", Ex);
                                }


                            }
                        }
                    }
                }
            }

            return success;
        }

        /// <summary>
        /// Enable DHCP on the NIC
        /// </summary>
        /// <param name="nic">Name of the NIC struct</param>
        public bool SetDHCP(NIC nic)
        {
            bool success = false;

            using (ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration"))
            {
                using (ManagementObjectCollection moc = mc.GetInstances())
                {
                    foreach (ManagementObject mo in moc)
                    {
                        // Make sure this is a IP enabled device. Not something like memory card or VM Ware
                        if ((bool)mo["IPEnabled"])
                        {
                            if (mo["SettingID"].Equals(nic.Guid))
                            {
                                try
                                {
                                    using (ManagementBaseObject newDNS = mo.GetMethodParameters("SetDNSServerSearchOrder"))
                                    {
                                        newDNS["DNSServerSearchOrder"] = null;

                                        mo.InvokeMethod("EnableDHCP", null, null);
                                        mo.InvokeMethod("SetDNSServerSearchOrder", newDNS, null);

                                        success = true;
                                    }
                                }
                                catch (Exception Ex)
                                {
                                    LogWriter.Exception("Error trying to set DNS.", Ex);
                                }


                            }
                        }
                    }
                }
            }

            return success;
        }

        /// <summary>
        /// Set IP for the specified network card name
        /// </summary>
        /// <param name="nic">Name of the NIC struct</param>
        /// <param name="IpAddresses">Comma delimited string containing one or more IP</param>
        /// <param name="SubnetMask">Subnet mask</param>
        /// <param name="Gateway">Gateway IP</param>
        /// <param name="DnsSearchOrder">Comma delimited DNS IP</param>
        public bool SetIP(NIC nic, string IpAddresses, string SubnetMask, string Gateway, string[] DnsSearchOrder)
        {
            bool success = false;

            using (ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration"))
            {
                using (ManagementObjectCollection moc = mc.GetInstances())
                {
                    foreach (ManagementObject mo in moc)
                    {
                        // Make sure this is a IP enabled device. Not something like memory card or VM Ware
                        if ((bool)mo["IPEnabled"])
                        {
                            if (mo["SettingID"].Equals(nic.Guid))
                            {
                                try
                                {
                                    using (ManagementBaseObject newIP = mo.GetMethodParameters("EnableStatic"))
                                    {
                                        newIP["IPAddress"] = IpAddresses.Split(',');
                                        newIP["SubnetMask"] = new string[] { SubnetMask };

                                        mo.InvokeMethod("EnableStatic", newIP, null);
                                    }

                                    using (ManagementBaseObject newGate = mo.GetMethodParameters("SetGateways"))
                                    {
                                        newGate["DefaultIPGateway"] = new string[] { Gateway };
                                        newGate["GatewayCostMetric"] = new int[] { 1 };
                                        mo.InvokeMethod("SetGateways", newGate, null);
                                    }

                                    using (ManagementBaseObject newDNS = mo.GetMethodParameters("SetDNSServerSearchOrder"))
                                    {
                                        newDNS["DNSServerSearchOrder"] = DnsSearchOrder; //.Split(',');
                                        mo.InvokeMethod("SetDNSServerSearchOrder", newDNS, null);
                                    }

                                    success = true;
                                }
                                catch (Exception Ex)
                                {
                                    LogWriter.Exception("Error trying to set IP.", Ex);
                                }
                            }
                        }
                    }
                }
            }

            return success;
        }

        /// <summary>
        /// Returns the network card configuration of the specified NIC
        /// </summary>
        /// <param name="nic">Name of the NIC struct</param>
        /// <param name="ipAdresses">Array of IP</param>
        /// <param name="subnets">Array of subnet masks</param>
        /// <param name="gateways">Array of gateways</param>
        /// <param name="dnses">Array of DNS IP</param>
        public NetworkData GetNetworkData(NIC nic)
        {
            NetworkData wmidata = new NetworkData();

            wmidata.IPaddresses = new List<string>();
            wmidata.Subnets = new List<string>();
            wmidata.Gateways = new List<string>();
            wmidata.DNSes = new List<string>();

            using (ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration"))
            {
                using (ManagementObjectCollection moc = mc.GetInstances())
                {
                    foreach (ManagementObject mo in moc)
                    {
                        // Make sure this is a IP enabled device. Not something like memory card or VM Ware
                        if ((bool)mo["ipEnabled"])
                        {
                            if (mo["SettingID"].Equals(nic.Guid))
                            {
                                if (mo["IPAddress"] != null)
                                {
                                    wmidata.IPaddresses = ((string[])mo["IPAddress"]).ToList();
                                }

                                if (mo["IPSubnet"] != null)
                                {
                                    wmidata.Subnets = ((string[])mo["IPSubnet"]).ToList();
                                }

                                if (mo["DefaultIPGateway"] != null)
                                {
                                    wmidata.Gateways = ((string[])mo["DefaultIPGateway"]).ToList();
                                }

                                if (mo["DNSServerSearchOrder"] != null)
                                {
                                    wmidata.DNSes = ((string[])mo["DNSServerSearchOrder"]).ToList();
                                }

                                if (mo["DNSDomainSuffixSearchOrder"] != null)
                                {
                                    wmidata.DNSSuffixes = ((string[])mo["DNSDomainSuffixSearchOrder"]).ToList();
                                }

                                if (mo["DHCPEnabled"] != null)
                                {
                                    wmidata.DHCP = ((bool)mo["DHCPEnabled"]);
                                }
                            }
                        }
                    }
                }
            }

            return wmidata;
        }

        /// <summary>
        /// Returns the list of Network Interfaces installed and the associated GUID
        /// </summary>
        /// <returns>Array list of string</returns>
        public List<NIC> GetNICList()
        {
            List<NIC> nicnames = new List<NIC>();

            using (ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration"))
            {
                using (ManagementObjectCollection moc = mc.GetInstances())
                {
                    foreach (ManagementObject mo in moc)
                    {
                        if ((bool)mo["ipEnabled"])
                        {
                            nicnames.Add(new NIC()
                            {
                                NicName = (string)mo["Caption"],
                                Guid = (string)mo["SettingID"],
                            });
                        }
                    }
                }
            }

            return nicnames;
        }

        // Get Workgroup
        public string GetCurrentWorkgroup()
        {
            string workgroup = "";

            SelectQuery query = new SelectQuery("Win32_ComputerSystem");
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            {
                foreach (ManagementObject mo in searcher.Get())
                {
                    if ((string)mo["domain"] != null)
                    {
                        workgroup = (string)mo["domain"];
                    }
                }
            }

            return workgroup;
        }

        public BindingData GetBindingData(string interfaceId)
        {
            BindingData bindings_data = new BindingData()
            {
                CfMN = BindingStatus.NotInstalled,
                FaPSfMN = BindingStatus.NotInstalled,
                LLDP = BindingStatus.NotInstalled,
            };

            var scope = new ManagementScope("\\\\.\\ROOT\\StandardCimv2");
            var query = new ObjectQuery("SELECT * FROM MSFT_NetAdapterBindingSettingData");
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query))
            {
                using (ManagementObjectCollection bindings = searcher.Get())
                {
                    foreach (ManagementObject item in bindings)
                    {
                        string InstanceID = (string)item["InstanceID"];
                        string ComponentID = (string)item["ComponentID"];

                        // Client for Microsoft Networks
                        if (InstanceID.ToLower().Contains(interfaceId.ToLower()) && InstanceID.ToLower() == "ms_msclient")
                        {
                            bool Enabled = (bool)item["Enabled"];

                            if (Enabled)
                            {
                                bindings_data.CfMN = BindingStatus.InstalledEnabled;
                            }
                            else
                            {
                                bindings_data.CfMN = BindingStatus.NotInstalled;
                            }
                        }

                        // File and Print Sharing for Microsoft Networks
                        if (InstanceID.ToLower().Contains(interfaceId.ToLower()) && InstanceID.ToLower() == "ms_server")
                        {
                            bool Enabled = (bool)item["Enabled"];

                            if (Enabled)
                            {
                                bindings_data.FaPSfMN = BindingStatus.InstalledEnabled;
                            }
                            else
                            {
                                bindings_data.FaPSfMN = BindingStatus.NotInstalled;
                            }
                        }

                        // Microsoft LLDP Protocol Driver
                        if (InstanceID.ToLower().Contains(interfaceId.ToLower()) && InstanceID.ToLower() == "ms_lldp")
                        {
                            bool Enabled = (bool)item["Enabled"];

                            if (Enabled)
                            {
                                bindings_data.LLDP = BindingStatus.InstalledEnabled;
                            }
                            else
                            {
                                bindings_data.LLDP = BindingStatus.NotInstalled;
                            }
                        }
                    }

                }
            }

            return bindings_data;
        }
    }

    /// This section will help gather data on local AV/FW/AS Products on the pc.

    public class SecurityProductData
    {
        public string name { get; set; }
        public string path { get; set; }

        public UInt32 state32 { get; set; }
        public bool enabled { get; set; }

        public bool updated { get; set; }

        public string version { get; set; }
        public byte state8 { get; set; }
        public bool scanning { get; set; }
    }

    public class InstalledSecurityProducts
    {
        public List<SecurityProductData> AVProducts = new List<SecurityProductData>();
        public List<SecurityProductData> FWProducts = new List<SecurityProductData>();
        public List<SecurityProductData> ASProducts = new List<SecurityProductData>();
    }

    public class WMISecurityHelper
    {
        public List<SecurityProductData> GetAVProducts()
        {
            List<SecurityProductData> List_Data = new List<SecurityProductData>();

            try
            {
                int m_version = System.Environment.OSVersion.Version.Major;

                string security_center = "";
                //string system_name = @"\\" + Environment.MachineName + @"\";

                if (m_version > 5)
                {
                    security_center = @"root\SecurityCenter2";
                }
                else
                {
                    security_center = @"root\SecurityCenter";
                }

                using (ManagementObjectSearcher av_searcher = new ManagementObjectSearcher(security_center, "SELECT * FROM AntivirusProduct"))
                {
                    using (ManagementObjectCollection data = av_searcher.Get())
                    {
                        foreach (ManagementObject managementobject in data)
                        {
                            SecurityProductData security_software_data = new SecurityProductData();

                            security_software_data.name = (string)managementobject["displayName"];
                            security_software_data.path = (string)managementobject["pathToSignedProductExe"];

                            if (m_version > 5)
                            {
                                security_software_data.state32 = (UInt32)managementobject["productState"];

                                string hex_value = Convert.ToInt32(security_software_data.state32).ToString("X6");

                                if (hex_value.Substring(2, 2) == "10" || hex_value.Substring(2, 2) == "11")
                                {
                                    security_software_data.enabled = true;
                                }
                                else if (hex_value.Substring(2, 2) == "00" || hex_value.Substring(2, 2) == "01")
                                {
                                    security_software_data.enabled = false;
                                }

                                if (hex_value.Substring(4, 2) == "00")
                                {
                                    security_software_data.updated = true;
                                }
                                else if (hex_value.Substring(4, 2) == "10")
                                {
                                    security_software_data.updated = false;
                                }
                            }
                            else
                            {
                                security_software_data.updated = (bool)managementobject["productUptoDate"];
                                security_software_data.scanning = (bool)managementobject["onAccessScanningEnabled"];
                                security_software_data.version = (string)(managementobject["versionNumber"]);
                                security_software_data.state8 = (byte)managementobject["productState"];
                            }

                            List_Data.Add(security_software_data);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                LogWriter.Exception("Something went wrong parsing WMI for security software.", Ex);
            }

            return List_Data;
        }

        public List<SecurityProductData> GetFWProducts()
        {
            List<SecurityProductData> List_Data = new List<SecurityProductData>();

            try
            {
                SecurityProductData fw_data = new SecurityProductData();
                fw_data.name = "Windows Firewall";
                fw_data.path = @"%SystemRoot%\System32\firewall.cpl";

                // REQUIRES using NetFwTypeLib;
                Type objectType = Type.GetTypeFromCLSID(new Guid("{304CE942-6E39-40D8-943A-B913C40C9CD4}"));
                INetFwMgr inetfwmgr = (INetFwMgr)Activator.CreateInstance(objectType);
                fw_data.enabled = inetfwmgr.LocalPolicy.CurrentProfile.FirewallEnabled;

                // REQUIRES .net 4.0;
                //Type FWManagerType = Type.GetTypeFromProgID("HNetCfg.FwMgr", false);
                //dynamic FWManager = Activator.CreateInstance(FWManagerType);
                //fw_data.enabled = FWManager.LocalPolicy.CurrentProfile.FirewallEnabled;

                List_Data.Add(fw_data);


                int m_version = System.Environment.OSVersion.Version.Major;

                string security_center = "";
                //string system_name = @"\\" + Environment.MachineName + @"\";

                if (m_version > 5)
                {
                    security_center = @"root\SecurityCenter2";
                }
                else
                {
                    security_center = @"root\SecurityCenter";
                }

                using (ManagementObjectSearcher fw_searcher = new ManagementObjectSearcher(security_center, "SELECT * FROM FirewallProduct"))
                {
                    using (ManagementObjectCollection data = fw_searcher.Get())
                    {
                        foreach (ManagementObject managementobject in data)
                        {
                            SecurityProductData security_software_data = new SecurityProductData();

                            security_software_data.name = (string)managementobject["displayName"];
                            security_software_data.path = (string)managementobject["pathToSignedProductExe"];

                            if (m_version > 5)
                            {
                                security_software_data.state32 = (UInt32)managementobject["productState"];

                                string hex_value = Convert.ToInt32(security_software_data.state32).ToString("X6");

                                if (hex_value.Substring(2, 2) == "10" || hex_value.Substring(2, 2) == "11")
                                {
                                    security_software_data.enabled = true;
                                }
                                else if (hex_value.Substring(2, 2) == "00" || hex_value.Substring(2, 2) == "01")
                                {
                                    security_software_data.enabled = false;
                                }
                            }
                            else
                            {
                                security_software_data.enabled = (bool)managementobject["enabled"];
                                security_software_data.version = (string)managementobject["versionNumber"];
                                security_software_data.state8 = (byte)managementobject["productState"];
                            }

                            List_Data.Add(security_software_data);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                LogWriter.Exception("Something went wrong parsing WMI for security software.", Ex);
            }

            return List_Data;
        }

        public List<SecurityProductData> GetASProducts()
        {
            List<SecurityProductData> List_Data = new List<SecurityProductData>();

            try
            {
                int m_version = System.Environment.OSVersion.Version.Major;

                string security_center = "";
                //string system_name = @"\\" + Environment.MachineName + @"\";

                if (m_version > 5)
                {
                    security_center = @"root\SecurityCenter2";
                }
                else
                {
                    security_center = @"root\SecurityCenter";
                }

                using (ManagementObjectSearcher as_searcher = new ManagementObjectSearcher(security_center, "SELECT * FROM AntiSpywareProduct"))
                {
                    using (ManagementObjectCollection data = as_searcher.Get())
                    {
                        foreach (ManagementObject managementobject in data)
                        {
                            SecurityProductData security_software_data = new SecurityProductData();

                            security_software_data.name = (string)managementobject["displayName"];
                            security_software_data.path = (string)managementobject["pathToSignedProductExe"];

                            if (m_version > 5)
                            {
                                security_software_data.state32 = (UInt32)managementobject["productState"];

                                string hex_value = Convert.ToInt32(security_software_data.state32).ToString("X6");

                                if (hex_value.Substring(2, 2) == "10" || hex_value.Substring(2, 2) == "11")
                                {
                                    security_software_data.enabled = true;
                                }
                                else if (hex_value.Substring(2, 2) == "00" || hex_value.Substring(2, 2) == "01")
                                {
                                    security_software_data.enabled = false;
                                }

                                if (hex_value.Substring(4, 2) == "00")
                                {
                                    security_software_data.updated = true;
                                }
                                else if (hex_value.Substring(4, 2) == "10")
                                {
                                    security_software_data.updated = false;
                                }
                            }
                            else
                            {
                                security_software_data.updated = (bool)managementobject["productUptoDate"];
                                security_software_data.enabled = (bool)managementobject["productEnabled"];
                                security_software_data.version = (string)(managementobject["versionNumber"]);
                                security_software_data.state8 = (byte)managementobject["productState"];
                            }

                            List_Data.Add(security_software_data);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                LogWriter.Exception("Something went wrong parsing WMI for security software.", Ex);
            }

            return List_Data;
        }
    }
}
