using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NetworkTool
{
    public partial class Networking : Form
    {
        public Networking()
        {
            InitializeComponent();

            Statics.MainWindow = this;

            current_ipv4_label.Text = "";
            current_ipv6_label.Text = "";
            current_subnet_label.Text = "";
            current_gateway_label.Text = "";
            current_dns_label.Text = "";
            current_proxy_label.Text = "";
            current_static_ip_label.Text = "";

            Load += (s, e) => Populate_Form();
        }

        private string av_data_string = "";
        private string fw_data_string = "";
        private string as_data_string = "";

        private void Populate_Form()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {

                }));
                return;
            }

            string edition = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ProductName", "").ToString();

            string releaseId = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", "").ToString();

            if (releaseId.Length <= 0)
            {
                releaseId = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CSDVersion", "").ToString();
            }

            if (releaseId.Length > 0)
            {
                releaseId = releaseId + " ";
            }

            string build1 =
                Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentMajorVersionNumber", "").ToString() + "." +
                Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentMinorVersionNumber", "").ToString();

            if (build1 == ".")
            {
                build1 = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentVersion", "").ToString();
            }

            string[] build_arr = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "BuildLabEx", "").ToString().Split('.');

            string build2 = build_arr[0] + "." + build_arr[1];

            os_version_label.Text = "Microsoft " + edition + " " + releaseId + "(" + build1 + "." + build2 + ")";

            fqdn_label.Text = "FQDN: " + System.Net.Dns.GetHostEntry(Environment.MachineName).HostName;

            List<SecurityProductData> av_data = new List<SecurityProductData>();
            av_data_string = "AV Products:";

            List<SecurityProductData> fw_data = new List<SecurityProductData>();
            fw_data_string = "FW Products:";

            List<SecurityProductData> as_data = new List<SecurityProductData>();
            as_data_string = "AS Products:";

            // Populate Network Section
            WMINetworkHelper wmi_network_helper = new WMINetworkHelper();

            List<NIC> nic_names = wmi_network_helper.GetNICList();
            nic_nicknames_combobox.Items.Clear();

            foreach (NIC nic in nic_names)
            {
                nic_nicknames_combobox.Items.Add(nic.NicName);
            }

            if (nic_nicknames_combobox.Items.Count > 0)
            {
                nic_nicknames_combobox.SelectedIndex = 0;
            }

            // Domain
            string domain = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
            if (domain.Length > 0)
            {
                current_domain_label.Text = domain;
            }
            else
            {
                domain = "N/A";
            }

            // Security Products
            WMISecurityHelper management_helper = new WMISecurityHelper();
            product_flowLayoutPanel.Controls.Clear();

            // AV Products
            // ----------------------------------------------------------------------------------------------------------
            av_data = management_helper.GetAVProducts();

            Panel av_label_panel = new Panel();
            av_label_panel.Height = 24;
            av_label_panel.Width = 420;
            av_label_panel.Padding = new System.Windows.Forms.Padding(0);
            av_label_panel.Margin = new System.Windows.Forms.Padding(0);

            Label av_label = new Label();
            av_label.Left = 8;
            av_label.Height = 17;
            av_label.Width = 148;
            av_label.Padding = new System.Windows.Forms.Padding(0);
            av_label.Margin = new System.Windows.Forms.Padding(0);
            av_label.Text = "Antivirus Products:";

            av_label_panel.Controls.Add(av_label);
            product_flowLayoutPanel.Controls.Add(av_label_panel);

            foreach (SecurityProductData item in av_data)
            {
                Panel av_product_panel = new Panel();
                av_product_panel.Height = 24;
                av_product_panel.Width = 420;
                av_product_panel.Padding = new System.Windows.Forms.Padding(0);
                av_product_panel.Margin = new System.Windows.Forms.Padding(0);

                Button av_product_button = new Button();
                av_product_button.Left = 8;
                av_product_button.Top = 0;
                av_product_button.Width = 256;
                av_product_button.Height = 24;
                av_product_button.Enabled = true;
                av_product_button.FlatStyle = System.Windows.Forms.FlatStyle.System;
                av_product_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                av_product_button.TabIndex = 5;
                av_product_button.TabStop = false;
                av_product_button.Text = item.name;
                av_product_button.UseVisualStyleBackColor = true;
                av_product_button.Padding = new System.Windows.Forms.Padding(0);
                av_product_button.Margin = new System.Windows.Forms.Padding(0);
                av_product_button.Click += new EventHandler(delegate (object sender, EventArgs e) { execute(item.path); });

                av_product_panel.Controls.Add(av_product_button);

                string label_text = "";

                if (item.enabled)
                {
                    label_text = "Enabled - ";
                }
                else
                {
                    label_text = "Disabled - ";
                }

                if (item.updated)
                {
                    label_text = label_text + "Updated";
                }
                else
                {
                    label_text = label_text + "Outdated";
                }

                Label av_product_label = new Label();
                av_product_label.Left = 272;
                av_product_label.Top = 3;
                av_product_label.Height = 17;
                av_product_label.Width = 140;
                av_product_label.Text = label_text;
                av_product_label.Padding = new System.Windows.Forms.Padding(0);
                av_product_label.Margin = new System.Windows.Forms.Padding(0);

                av_product_panel.Controls.Add(av_product_label);

                product_flowLayoutPanel.Controls.Add(av_product_panel);

                av_data_string = av_data_string + Environment.NewLine + item.name + " | " + label_text;
            }

            Panel spacer_panel1 = new Panel();
            spacer_panel1.Height = 12;
            spacer_panel1.Width = 420;
            spacer_panel1.Padding = new System.Windows.Forms.Padding(0);
            spacer_panel1.Margin = new System.Windows.Forms.Padding(0);
            product_flowLayoutPanel.Controls.Add(spacer_panel1);

            // FW Products
            // ----------------------------------------------------------------------------------------------------------
            fw_data = management_helper.GetFWProducts();

            Panel fw_label_panel = new Panel();
            fw_label_panel.Height = 24;
            fw_label_panel.Width = 420;
            fw_label_panel.Padding = new System.Windows.Forms.Padding(0);
            fw_label_panel.Margin = new System.Windows.Forms.Padding(0);

            Label fw_label = new Label();
            fw_label.Left = 8;
            fw_label.Height = 17;
            fw_label.Width = 148;
            fw_label.Padding = new System.Windows.Forms.Padding(0);
            fw_label.Margin = new System.Windows.Forms.Padding(0);
            fw_label.Text = "Firewall Products:";

            fw_label_panel.Controls.Add(fw_label);
            product_flowLayoutPanel.Controls.Add(fw_label_panel);

            foreach (SecurityProductData item in fw_data)
            {
                Panel fw_product_panel = new Panel();
                fw_product_panel.Height = 24;
                fw_product_panel.Width = 420;
                fw_product_panel.Padding = new System.Windows.Forms.Padding(0);
                fw_product_panel.Margin = new System.Windows.Forms.Padding(0);

                Button fw_product_button = new Button();
                fw_product_button.Left = 8;
                fw_product_button.Top = 0;
                fw_product_button.Width = 256;
                fw_product_button.Height = 24;
                fw_product_button.Enabled = true;
                fw_product_button.FlatStyle = System.Windows.Forms.FlatStyle.System;
                fw_product_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                fw_product_button.TabIndex = 5;
                fw_product_button.TabStop = false;
                fw_product_button.Text = item.name;
                fw_product_button.UseVisualStyleBackColor = true;
                fw_product_button.Padding = new System.Windows.Forms.Padding(0);
                fw_product_button.Margin = new System.Windows.Forms.Padding(0);
                fw_product_button.Click += new EventHandler(delegate (object sender, EventArgs e) { execute(item.path); });

                fw_product_panel.Controls.Add(fw_product_button);

                string label_text = "";

                if (item.enabled)
                {
                    label_text = "Enabled";
                }
                else
                {
                    label_text = "Disabled";
                }

                Label fw_product_label = new Label();
                fw_product_label.Left = 272;
                fw_product_label.Top = 3;
                fw_product_label.Height = 17;
                fw_product_label.Width = 140;
                fw_product_label.Text = label_text;
                fw_product_label.Padding = new System.Windows.Forms.Padding(0);
                fw_product_label.Margin = new System.Windows.Forms.Padding(0);

                fw_product_panel.Controls.Add(fw_product_label);

                product_flowLayoutPanel.Controls.Add(fw_product_panel);

                fw_data_string = fw_data_string + Environment.NewLine + item.name + " | " + label_text;
            }

            Panel spacer_panel2 = new Panel();
            spacer_panel2.Height = 12;
            spacer_panel2.Width = 420;
            spacer_panel2.Padding = new System.Windows.Forms.Padding(0);
            spacer_panel2.Margin = new System.Windows.Forms.Padding(0);
            product_flowLayoutPanel.Controls.Add(spacer_panel2);

            // AS Products
            // ----------------------------------------------------------------------------------------------------------
            as_data = management_helper.GetASProducts();

            Panel as_label_panel = new Panel();
            as_label_panel.Height = 24;
            as_label_panel.Width = 420;
            as_label_panel.Padding = new System.Windows.Forms.Padding(0);
            as_label_panel.Margin = new System.Windows.Forms.Padding(0);

            Label as_label = new Label();
            as_label.Left = 8;
            as_label.Height = 17;
            as_label.Width = 148;
            as_label.Padding = new System.Windows.Forms.Padding(0);
            as_label.Margin = new System.Windows.Forms.Padding(0);
            as_label.Text = "Antispyware Products:";

            as_label_panel.Controls.Add(as_label);
            product_flowLayoutPanel.Controls.Add(as_label_panel);

            foreach (SecurityProductData item in as_data)
            {
                Panel as_product_panel = new Panel();
                as_product_panel.Height = 24;
                as_product_panel.Width = 420;
                as_product_panel.Padding = new System.Windows.Forms.Padding(0);
                as_product_panel.Margin = new System.Windows.Forms.Padding(0);

                Button as_product_button = new Button();
                as_product_button.Left = 8;
                as_product_button.Top = 0;
                as_product_button.Width = 256;
                as_product_button.Height = 24;
                as_product_button.Enabled = true;
                as_product_button.FlatStyle = System.Windows.Forms.FlatStyle.System;
                as_product_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                as_product_button.TabIndex = 5;
                as_product_button.TabStop = false;
                as_product_button.Text = item.name;
                as_product_button.UseVisualStyleBackColor = true;
                as_product_button.Padding = new System.Windows.Forms.Padding(0);
                as_product_button.Margin = new System.Windows.Forms.Padding(0);
                as_product_button.Click += new EventHandler(delegate (object sender, EventArgs e) { execute(item.path); });

                as_product_panel.Controls.Add(as_product_button);

                string label_text = "";

                if (item.enabled)
                {
                    label_text = "Enabled - ";
                }
                else
                {
                    label_text = "Disabled - ";
                }

                if (item.updated)
                {
                    label_text = label_text + "Updated";
                }
                else
                {
                    label_text = label_text + "Outdated";
                }

                Label as_product_label = new Label();
                as_product_label.Left = 272;
                as_product_label.Top = 3;
                as_product_label.Height = 17;
                as_product_label.Width = 140;
                as_product_label.Text = label_text;
                as_product_label.Padding = new System.Windows.Forms.Padding(0);
                as_product_label.Margin = new System.Windows.Forms.Padding(0);

                as_product_panel.Controls.Add(as_product_label);

                product_flowLayoutPanel.Controls.Add(as_product_panel);

                as_data_string = as_data_string + Environment.NewLine + item.name + " | " + label_text;
            }
        }

        private void execute(string path)
        {
            try
            {
                if (path.ToLower().Contains("%"))
                {
                    path = Environment.ExpandEnvironmentVariables(path);
                }

                if (System.IO.File.Exists(path) || path.ToLower().Contains("windowsdefender:"))
                {
                    using (System.Diagnostics.Process s_product = new System.Diagnostics.Process())
                    {
                        s_product.StartInfo.FileName = path;
                        s_product.StartInfo.UseShellExecute = true;
                        s_product.Start();
                    }
                }
            }
            catch (Exception Ex)
            {
                LogWriter.Exception("There was a problem opening the software product at the following path: " + path, Ex, true);
            }
        }

        private void nic_nicknames_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            WMINetworkHelper wmi_network_helper = new WMINetworkHelper();
            NetworkData network_data = new NetworkData();

            NIC selected_nic = new NIC();
            List<NIC> nic_names = wmi_network_helper.GetNICList();
            foreach (NIC nic in nic_names)
            {
                if (nic.NicName.ToLower() == Convert.ToString(nic_nicknames_combobox.SelectedItem).ToLower())
                {
                    selected_nic.NicName = nic.NicName;
                    selected_nic.Guid = nic.Guid;
                }
            }

            network_data = wmi_network_helper.GetNetworkData(selected_nic);

            if (network_data.IPaddresses.Count > 0)
            {
                current_ipv4_label.Text = network_data.IPaddresses[0];

                if (network_data.IPaddresses.Count > 1)
                {
                    current_ipv6_label.Text = network_data.IPaddresses[1];
                }

                if (network_data.Subnets.Count > 0)
                {
                    current_subnet_label.Text = network_data.Subnets[0];
                }
                
                if (network_data.Gateways.Count > 0)
                {
                    current_gateway_label.Text = network_data.Gateways[0];
                }
                
                current_dns_label.Text = String.Join(" | ", network_data.DNSes.ToArray());
                current_suffix_label.Text = String.Join(" | ", network_data.DNSSuffixes.ToArray());
                current_proxy_label.Text = IEProxy.ProxyServer;

                if (network_data.DHCP)
                {
                    current_static_ip_label.Text = "No";
                }
                else
                {
                    current_static_ip_label.Text = "Yes";
                }

                if ((System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName).Length > 0)
                {
                    current_workgroup_label.Text = "";
                    current_domain_label.Text = wmi_network_helper.GetCurrentWorkgroup();
                }
                else
                {
                    current_workgroup_label.Text = wmi_network_helper.GetCurrentWorkgroup();
                    current_domain_label.Text = "";
                }


            }
        }

        private void set_dns_Click(object sender, EventArgs e)
        {
            string[] Google_DNS = { "8.8.8.8", "8.8.4.4" };

            WMINetworkHelper wmi_network_helper = new WMINetworkHelper();

            NIC selected_nic = new NIC();
            List<NIC> nic_names = wmi_network_helper.GetNICList();
            foreach (NIC nic in nic_names)
            {
                if (nic.NicName.ToLower() == Convert.ToString(nic_nicknames_combobox.SelectedItem).ToLower())
                {
                    selected_nic.NicName = nic.NicName;
                    selected_nic.Guid = nic.Guid;
                }
            }

            if (wmi_network_helper.SetDNS(selected_nic, Google_DNS))
            {
                MessageBox.Show("New DNS search order added. New DNS should now display.", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error trying to set DNS.", "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            nic_nicknames_combobox_SelectedIndexChanged(sender, e);
        }

        private void refresh_config_Click(object sender, EventArgs e)
        {
            //nic_nicknames_combobox_SelectedIndexChanged(sender, e);

            Populate_Form();
        }

        private void flush_dns_Click(object sender, EventArgs e)
        {
            try
            {
                string CMDPath = Environment.SystemDirectory;

                if (System.IO.File.Exists(CMDPath + @"\cmd.exe"))
                {
                    System.Diagnostics.Process run_cmd = new System.Diagnostics.Process();
                    run_cmd.StartInfo.FileName = CMDPath + @"\cmd.exe";
                    run_cmd.StartInfo.Arguments = @"/c ipconfig /flushdns && ipconfig /registerdns";
                    run_cmd.Start();
                    run_cmd.WaitForExit();
                }
            }
            catch
            {

            }
        }

        private string post_data = @"N/A";

        private void generate_post_data()
        {
            post_data =
                os_version_label.Text + Environment.NewLine +

                Environment.NewLine +

                "Computer Name: " + Environment.MachineName + Environment.NewLine +
                "FQDN: " + fqdn_label.Text + Environment.NewLine +

                Environment.NewLine +

                "NIC: " + nic_nicknames_combobox.Text + Environment.NewLine +
                "IPv4: " + current_ipv4_label.Text + Environment.NewLine +
                "IPv6: " + current_ipv6_label.Text + Environment.NewLine +
                "Subnet: " + current_subnet_label.Text + Environment.NewLine +
                "Gateway: " + current_gateway_label.Text + Environment.NewLine +
                "DNS: " + current_dns_label.Text + Environment.NewLine +
                "Suffix: " + current_suffix_label.Text + Environment.NewLine +
                "Proxy: " + current_proxy_label.Text + Environment.NewLine +
                "Static IP: " + current_static_ip_label.Text + Environment.NewLine +
                "Workgroup: " + current_workgroup_label.Text + Environment.NewLine +
                "Domain: " + current_domain_label.Text + Environment.NewLine +

                Environment.NewLine +

                av_data_string + Environment.NewLine + Environment.NewLine +
                fw_data_string + Environment.NewLine + Environment.NewLine +
                as_data_string;

            //MessageBox.Show(post_data); //debug
        }

        private void copy_button_Click(object sender, EventArgs e)
        {
            generate_post_data();

            System.Windows.Forms.Clipboard.SetText(post_data);
        }
    }
}
