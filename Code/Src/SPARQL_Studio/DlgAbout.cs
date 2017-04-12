// Author: Antonello Salvatucci as@modularsystems.dk
//
// This code is copyright (c) by Antonello Salvatucci and Modular Systems
// See the LICENSE file at the root of the repository for licensing details.
// 
// THIS CODE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED
// ----------------------------------------------------------------------------
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace dk.ModularSystems.Sparql
{
    public partial class DlgAbout : Form
    {
        public DlgAbout()
        {
            InitializeComponent();
        }

        public void FillInformation(Assembly assembly)
        {
            AssemblyProductAttribute productAttr = (AssemblyProductAttribute) assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), inherit: true).FirstOrDefault();
            String productName = productAttr.Product;

            AssemblyCompanyAttribute companyAttr = (AssemblyCompanyAttribute)assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), inherit: true).FirstOrDefault();
            String companyName = companyAttr.Company;

            Version version = assembly.GetName().Version;
            String productVersion = version.ToString();
            String productVersionMajor = String.Format("{0}.{1}", version.Major, version.Minor);
            lbProductName.Text = String.Format("{0} {1}", productName, productVersionMajor);
            lbLinkCompany.Text = companyName;
            lbLinkCompany.LinkClicked +=lbLinkCompany_LinkClicked;
            lbLinkCompany.Tag = "http://www.modularsystems.dk"; // TEST TEST TEST

            lvAttributes.Items.Clear();
            IList<CustomAttributeData> assemblyAttributes = assembly.GetCustomAttributesData();
            ListViewItem itemVersion = lvAttributes.Items.Add("Version");
            itemVersion.SubItems.Add(productVersion);

            foreach (CustomAttributeData attribute in assemblyAttributes)
            {
                String attrName = attribute.ToString().Remove(attribute.ToString().IndexOf("Attribute", System.StringComparison.Ordinal));
                attrName = attrName.Substring(attrName.LastIndexOf('.')+1);

                ListViewItem item = lvAttributes.Items.Add(attrName); 
                foreach (CustomAttributeTypedArgument arg in attribute.ConstructorArguments)
                {
                    item.SubItems.Add(arg.Value.ToString());
                }
                //foreach (CustomAttributeNamedArgument arg in attribute.NamedArguments)
                //{
                //    ListViewItem item = lvAttributes.Items.Add(attrName);
                //    item.SubItems.Add(String.Format("{0} = {1}", arg.MemberName, arg.TypedValue.ToString()));
                //}
            }
        }

        void lbLinkCompany_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            String linkSite = (String) ((LinkLabel) sender).Tag;
            System.Diagnostics.Process.Start(linkSite);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            DialogResult = DialogResult.Cancel;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
