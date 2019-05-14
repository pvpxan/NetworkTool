namespace NetworkTool
{
    partial class Networking
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.nic_nicknames_combobox = new System.Windows.Forms.ComboBox();
            this.network_groupbox = new System.Windows.Forms.GroupBox();
            this.current_workgroup_label = new System.Windows.Forms.Label();
            this.workgroup_label = new System.Windows.Forms.Label();
            this.current_domain_label = new System.Windows.Forms.Label();
            this.domain_label = new System.Windows.Forms.Label();
            this.suffix_label = new System.Windows.Forms.Label();
            this.current_suffix_label = new System.Windows.Forms.Label();
            this.current_static_ip_label = new System.Windows.Forms.Label();
            this.static_ip_label = new System.Windows.Forms.Label();
            this.flush_dns = new System.Windows.Forms.Button();
            this.current_ipv6_label = new System.Windows.Forms.Label();
            this.set_dns = new System.Windows.Forms.Button();
            this.ipv6_label = new System.Windows.Forms.Label();
            this.dns_label = new System.Windows.Forms.Label();
            this.gateway_label = new System.Windows.Forms.Label();
            this.subnet_label = new System.Windows.Forms.Label();
            this.ipv4_label = new System.Windows.Forms.Label();
            this.current_dns_label = new System.Windows.Forms.Label();
            this.current_gateway_label = new System.Windows.Forms.Label();
            this.current_subnet_label = new System.Windows.Forms.Label();
            this.current_ipv4_label = new System.Windows.Forms.Label();
            this.proxy_label = new System.Windows.Forms.Label();
            this.current_proxy_label = new System.Windows.Forms.Label();
            this.refresh_config = new System.Windows.Forms.Button();
            this.security_products_groupbox = new System.Windows.Forms.GroupBox();
            this.product_flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.os_version_label = new System.Windows.Forms.Label();
            this.fqdn_label = new System.Windows.Forms.Label();
            this.copy_button = new System.Windows.Forms.Button();
            this.network_groupbox.SuspendLayout();
            this.security_products_groupbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // nic_nicknames_combobox
            // 
            this.nic_nicknames_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.nic_nicknames_combobox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nic_nicknames_combobox.FormattingEnabled = true;
            this.nic_nicknames_combobox.Location = new System.Drawing.Point(9, 24);
            this.nic_nicknames_combobox.Name = "nic_nicknames_combobox";
            this.nic_nicknames_combobox.Size = new System.Drawing.Size(332, 24);
            this.nic_nicknames_combobox.TabIndex = 0;
            this.nic_nicknames_combobox.SelectedIndexChanged += new System.EventHandler(this.nic_nicknames_combobox_SelectedIndexChanged);
            // 
            // network_groupbox
            // 
            this.network_groupbox.Controls.Add(this.current_workgroup_label);
            this.network_groupbox.Controls.Add(this.workgroup_label);
            this.network_groupbox.Controls.Add(this.current_domain_label);
            this.network_groupbox.Controls.Add(this.domain_label);
            this.network_groupbox.Controls.Add(this.suffix_label);
            this.network_groupbox.Controls.Add(this.current_suffix_label);
            this.network_groupbox.Controls.Add(this.current_static_ip_label);
            this.network_groupbox.Controls.Add(this.static_ip_label);
            this.network_groupbox.Controls.Add(this.flush_dns);
            this.network_groupbox.Controls.Add(this.current_ipv6_label);
            this.network_groupbox.Controls.Add(this.set_dns);
            this.network_groupbox.Controls.Add(this.ipv6_label);
            this.network_groupbox.Controls.Add(this.dns_label);
            this.network_groupbox.Controls.Add(this.nic_nicknames_combobox);
            this.network_groupbox.Controls.Add(this.gateway_label);
            this.network_groupbox.Controls.Add(this.subnet_label);
            this.network_groupbox.Controls.Add(this.ipv4_label);
            this.network_groupbox.Controls.Add(this.current_dns_label);
            this.network_groupbox.Controls.Add(this.current_gateway_label);
            this.network_groupbox.Controls.Add(this.current_subnet_label);
            this.network_groupbox.Controls.Add(this.current_ipv4_label);
            this.network_groupbox.Controls.Add(this.proxy_label);
            this.network_groupbox.Controls.Add(this.current_proxy_label);
            this.network_groupbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.network_groupbox.Location = new System.Drawing.Point(12, 29);
            this.network_groupbox.Name = "network_groupbox";
            this.network_groupbox.Size = new System.Drawing.Size(350, 370);
            this.network_groupbox.TabIndex = 40;
            this.network_groupbox.TabStop = false;
            this.network_groupbox.Text = "Network Configuration";
            // 
            // current_workgroup_label
            // 
            this.current_workgroup_label.AutoSize = true;
            this.current_workgroup_label.Location = new System.Drawing.Point(151, 280);
            this.current_workgroup_label.Name = "current_workgroup_label";
            this.current_workgroup_label.Size = new System.Drawing.Size(17, 16);
            this.current_workgroup_label.TabIndex = 46;
            this.current_workgroup_label.Text = "X";
            // 
            // workgroup_label
            // 
            this.workgroup_label.AutoSize = true;
            this.workgroup_label.Location = new System.Drawing.Point(7, 280);
            this.workgroup_label.Name = "workgroup_label";
            this.workgroup_label.Size = new System.Drawing.Size(88, 16);
            this.workgroup_label.TabIndex = 45;
            this.workgroup_label.Text = "Workgroup:";
            // 
            // current_domain_label
            // 
            this.current_domain_label.AutoSize = true;
            this.current_domain_label.Location = new System.Drawing.Point(151, 304);
            this.current_domain_label.Name = "current_domain_label";
            this.current_domain_label.Size = new System.Drawing.Size(17, 16);
            this.current_domain_label.TabIndex = 44;
            this.current_domain_label.Text = "X";
            // 
            // domain_label
            // 
            this.domain_label.AutoSize = true;
            this.domain_label.Location = new System.Drawing.Point(7, 304);
            this.domain_label.Name = "domain_label";
            this.domain_label.Size = new System.Drawing.Size(65, 16);
            this.domain_label.TabIndex = 43;
            this.domain_label.Text = "Domain:";
            // 
            // suffix_label
            // 
            this.suffix_label.AutoSize = true;
            this.suffix_label.Location = new System.Drawing.Point(7, 208);
            this.suffix_label.Name = "suffix_label";
            this.suffix_label.Size = new System.Drawing.Size(49, 16);
            this.suffix_label.TabIndex = 41;
            this.suffix_label.Text = "Suffix:";
            // 
            // current_suffix_label
            // 
            this.current_suffix_label.AutoSize = true;
            this.current_suffix_label.Location = new System.Drawing.Point(151, 208);
            this.current_suffix_label.Name = "current_suffix_label";
            this.current_suffix_label.Size = new System.Drawing.Size(128, 16);
            this.current_suffix_label.TabIndex = 42;
            this.current_suffix_label.Text = "XXX.XXX.XXX.XXX";
            // 
            // current_static_ip_label
            // 
            this.current_static_ip_label.AutoSize = true;
            this.current_static_ip_label.Location = new System.Drawing.Point(151, 256);
            this.current_static_ip_label.Name = "current_static_ip_label";
            this.current_static_ip_label.Size = new System.Drawing.Size(17, 16);
            this.current_static_ip_label.TabIndex = 40;
            this.current_static_ip_label.Text = "X";
            // 
            // static_ip_label
            // 
            this.static_ip_label.AutoSize = true;
            this.static_ip_label.Location = new System.Drawing.Point(7, 256);
            this.static_ip_label.Name = "static_ip_label";
            this.static_ip_label.Size = new System.Drawing.Size(69, 16);
            this.static_ip_label.TabIndex = 40;
            this.static_ip_label.Text = "Static IP:";
            // 
            // flush_dns
            // 
            this.flush_dns.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.flush_dns.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flush_dns.Location = new System.Drawing.Point(154, 55);
            this.flush_dns.Name = "flush_dns";
            this.flush_dns.Size = new System.Drawing.Size(187, 24);
            this.flush_dns.TabIndex = 3;
            this.flush_dns.TabStop = false;
            this.flush_dns.Text = "Flush DNS Cache";
            this.flush_dns.UseVisualStyleBackColor = true;
            this.flush_dns.Click += new System.EventHandler(this.flush_dns_Click);
            // 
            // current_ipv6_label
            // 
            this.current_ipv6_label.AutoSize = true;
            this.current_ipv6_label.Location = new System.Drawing.Point(151, 110);
            this.current_ipv6_label.Name = "current_ipv6_label";
            this.current_ipv6_label.Size = new System.Drawing.Size(128, 16);
            this.current_ipv6_label.TabIndex = 40;
            this.current_ipv6_label.Text = "XXX.XXX.XXX.XXX";
            // 
            // set_dns
            // 
            this.set_dns.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.set_dns.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.set_dns.Location = new System.Drawing.Point(9, 55);
            this.set_dns.Name = "set_dns";
            this.set_dns.Size = new System.Drawing.Size(139, 24);
            this.set_dns.TabIndex = 1;
            this.set_dns.TabStop = false;
            this.set_dns.Text = "Set Google DNS";
            this.set_dns.UseVisualStyleBackColor = true;
            this.set_dns.Click += new System.EventHandler(this.set_dns_Click);
            // 
            // ipv6_label
            // 
            this.ipv6_label.AutoSize = true;
            this.ipv6_label.Location = new System.Drawing.Point(7, 110);
            this.ipv6_label.Name = "ipv6_label";
            this.ipv6_label.Size = new System.Drawing.Size(42, 16);
            this.ipv6_label.TabIndex = 40;
            this.ipv6_label.Text = "IPv6:";
            // 
            // dns_label
            // 
            this.dns_label.AutoSize = true;
            this.dns_label.Location = new System.Drawing.Point(7, 182);
            this.dns_label.Name = "dns_label";
            this.dns_label.Size = new System.Drawing.Size(44, 16);
            this.dns_label.TabIndex = 40;
            this.dns_label.Text = "DNS:";
            // 
            // gateway_label
            // 
            this.gateway_label.AutoSize = true;
            this.gateway_label.Location = new System.Drawing.Point(7, 158);
            this.gateway_label.Name = "gateway_label";
            this.gateway_label.Size = new System.Drawing.Size(72, 16);
            this.gateway_label.TabIndex = 40;
            this.gateway_label.Text = "Gateway:";
            // 
            // subnet_label
            // 
            this.subnet_label.AutoSize = true;
            this.subnet_label.Location = new System.Drawing.Point(7, 134);
            this.subnet_label.Name = "subnet_label";
            this.subnet_label.Size = new System.Drawing.Size(60, 16);
            this.subnet_label.TabIndex = 40;
            this.subnet_label.Text = "Subnet:";
            // 
            // ipv4_label
            // 
            this.ipv4_label.AutoSize = true;
            this.ipv4_label.Location = new System.Drawing.Point(7, 86);
            this.ipv4_label.Name = "ipv4_label";
            this.ipv4_label.Size = new System.Drawing.Size(42, 16);
            this.ipv4_label.TabIndex = 40;
            this.ipv4_label.Text = "IPv4:";
            // 
            // current_dns_label
            // 
            this.current_dns_label.AutoSize = true;
            this.current_dns_label.Location = new System.Drawing.Point(151, 182);
            this.current_dns_label.Name = "current_dns_label";
            this.current_dns_label.Size = new System.Drawing.Size(128, 16);
            this.current_dns_label.TabIndex = 40;
            this.current_dns_label.Text = "XXX.XXX.XXX.XXX";
            // 
            // current_gateway_label
            // 
            this.current_gateway_label.AutoSize = true;
            this.current_gateway_label.Location = new System.Drawing.Point(151, 158);
            this.current_gateway_label.Name = "current_gateway_label";
            this.current_gateway_label.Size = new System.Drawing.Size(128, 16);
            this.current_gateway_label.TabIndex = 40;
            this.current_gateway_label.Text = "XXX.XXX.XXX.XXX";
            // 
            // current_subnet_label
            // 
            this.current_subnet_label.AutoSize = true;
            this.current_subnet_label.Location = new System.Drawing.Point(151, 134);
            this.current_subnet_label.Name = "current_subnet_label";
            this.current_subnet_label.Size = new System.Drawing.Size(128, 16);
            this.current_subnet_label.TabIndex = 40;
            this.current_subnet_label.Text = "XXX.XXX.XXX.XXX";
            // 
            // current_ipv4_label
            // 
            this.current_ipv4_label.AutoSize = true;
            this.current_ipv4_label.Location = new System.Drawing.Point(151, 86);
            this.current_ipv4_label.Name = "current_ipv4_label";
            this.current_ipv4_label.Size = new System.Drawing.Size(128, 16);
            this.current_ipv4_label.TabIndex = 40;
            this.current_ipv4_label.Text = "XXX.XXX.XXX.XXX";
            // 
            // proxy_label
            // 
            this.proxy_label.AutoSize = true;
            this.proxy_label.Location = new System.Drawing.Point(7, 232);
            this.proxy_label.Name = "proxy_label";
            this.proxy_label.Size = new System.Drawing.Size(51, 16);
            this.proxy_label.TabIndex = 40;
            this.proxy_label.Text = "Proxy:";
            // 
            // current_proxy_label
            // 
            this.current_proxy_label.AutoSize = true;
            this.current_proxy_label.Location = new System.Drawing.Point(151, 232);
            this.current_proxy_label.Name = "current_proxy_label";
            this.current_proxy_label.Size = new System.Drawing.Size(128, 16);
            this.current_proxy_label.TabIndex = 40;
            this.current_proxy_label.Text = "XXX.XXX.XXX.XXX";
            // 
            // refresh_config
            // 
            this.refresh_config.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.refresh_config.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refresh_config.Location = new System.Drawing.Point(628, 5);
            this.refresh_config.Name = "refresh_config";
            this.refresh_config.Size = new System.Drawing.Size(88, 24);
            this.refresh_config.TabIndex = 2;
            this.refresh_config.Text = "Refresh";
            this.refresh_config.UseVisualStyleBackColor = true;
            this.refresh_config.Click += new System.EventHandler(this.refresh_config_Click);
            // 
            // security_products_groupbox
            // 
            this.security_products_groupbox.Controls.Add(this.product_flowLayoutPanel);
            this.security_products_groupbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.security_products_groupbox.Location = new System.Drawing.Point(368, 29);
            this.security_products_groupbox.Name = "security_products_groupbox";
            this.security_products_groupbox.Size = new System.Drawing.Size(435, 370);
            this.security_products_groupbox.TabIndex = 41;
            this.security_products_groupbox.TabStop = false;
            this.security_products_groupbox.Text = "Security Products";
            // 
            // product_flowLayoutPanel
            // 
            this.product_flowLayoutPanel.AutoScroll = true;
            this.product_flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.product_flowLayoutPanel.Location = new System.Drawing.Point(2, 24);
            this.product_flowLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.product_flowLayoutPanel.Name = "product_flowLayoutPanel";
            this.product_flowLayoutPanel.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.product_flowLayoutPanel.Size = new System.Drawing.Size(431, 340);
            this.product_flowLayoutPanel.TabIndex = 1;
            this.product_flowLayoutPanel.WrapContents = false;
            // 
            // os_version_label
            // 
            this.os_version_label.AutoSize = true;
            this.os_version_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.os_version_label.Location = new System.Drawing.Point(9, 9);
            this.os_version_label.Name = "os_version_label";
            this.os_version_label.Size = new System.Drawing.Size(137, 16);
            this.os_version_label.TabIndex = 42;
            this.os_version_label.Text = "Microsoft Windows";
            // 
            // fqdn_label
            // 
            this.fqdn_label.AutoSize = true;
            this.fqdn_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.fqdn_label.Location = new System.Drawing.Point(367, 9);
            this.fqdn_label.Name = "fqdn_label";
            this.fqdn_label.Size = new System.Drawing.Size(54, 16);
            this.fqdn_label.TabIndex = 43;
            this.fqdn_label.Text = "FQDN:";
            // 
            // copy_button
            // 
            this.copy_button.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.copy_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.copy_button.Location = new System.Drawing.Point(722, 5);
            this.copy_button.Name = "copy_button";
            this.copy_button.Size = new System.Drawing.Size(80, 24);
            this.copy_button.TabIndex = 44;
            this.copy_button.TabStop = false;
            this.copy_button.Text = "Copy Info";
            this.copy_button.UseVisualStyleBackColor = true;
            this.copy_button.Click += new System.EventHandler(this.copy_button_Click);
            // 
            // Networking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateGray;
            this.ClientSize = new System.Drawing.Size(814, 406);
            this.Controls.Add(this.copy_button);
            this.Controls.Add(this.fqdn_label);
            this.Controls.Add(this.os_version_label);
            this.Controls.Add(this.security_products_groupbox);
            this.Controls.Add(this.network_groupbox);
            this.Controls.Add(this.refresh_config);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Networking";
            this.Text = "Network Tool";
            this.network_groupbox.ResumeLayout(false);
            this.network_groupbox.PerformLayout();
            this.security_products_groupbox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox nic_nicknames_combobox;
        private System.Windows.Forms.GroupBox network_groupbox;
        private System.Windows.Forms.Label dns_label;
        private System.Windows.Forms.Label gateway_label;
        private System.Windows.Forms.Label subnet_label;
        private System.Windows.Forms.Label ipv4_label;
        private System.Windows.Forms.Label current_dns_label;
        private System.Windows.Forms.Label current_gateway_label;
        private System.Windows.Forms.Label current_subnet_label;
        private System.Windows.Forms.Label current_ipv4_label;
        private System.Windows.Forms.Label proxy_label;
        private System.Windows.Forms.Label current_proxy_label;
        private System.Windows.Forms.Button set_dns;
        private System.Windows.Forms.Button flush_dns;
        private System.Windows.Forms.Label ipv6_label;
        private System.Windows.Forms.Label current_static_ip_label;
        private System.Windows.Forms.Label static_ip_label;
        private System.Windows.Forms.Button refresh_config;
        private System.Windows.Forms.GroupBox security_products_groupbox;
        private System.Windows.Forms.FlowLayoutPanel product_flowLayoutPanel;
        private System.Windows.Forms.Label os_version_label;
        private System.Windows.Forms.Label suffix_label;
        private System.Windows.Forms.Label current_suffix_label;
        private System.Windows.Forms.Label current_domain_label;
        private System.Windows.Forms.Label domain_label;
        private System.Windows.Forms.Label workgroup_label;
        private System.Windows.Forms.Label current_workgroup_label;
        private System.Windows.Forms.Label current_ipv6_label;
        private System.Windows.Forms.Label fqdn_label;
        private System.Windows.Forms.Button copy_button;
    }
}