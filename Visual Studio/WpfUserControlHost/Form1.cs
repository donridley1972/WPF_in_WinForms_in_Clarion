using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace WpfUserControlHost
{
    [ProgId("WpfUserControlHost")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public partial class Form1 : UserControl
    {
        public Form1()
        {
            InitializeComponent();
        }

        //https://pastebin.com/U0MSNY1p
        [ComRegisterFunction]
        private static void ComRegister(Type t)
        {
            string keyName = @"CLSID\" + t.GUID.ToString("B");

            using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(keyName, true))
            {
                if (key == null)
                    return;


                if (key.CreateSubKey("Control") != null) key.CreateSubKey("Control").Close();

                using (RegistryKey subkey = key.CreateSubKey("MiscStatus"))
                {
                    if (subkey != null) subkey.SetValue("", "131457");
                }
                using (RegistryKey subkey = key.CreateSubKey("TypeLib"))
                {
                    Guid libid = Marshal.GetTypeLibGuidForAssembly(t.Assembly);
                    if (subkey != null) subkey.SetValue("", libid.ToString("B"));
                }
                using (RegistryKey subkey = key.CreateSubKey("Version"))
                {
                    Version ver = t.Assembly.GetName().Version;
                    string version = string.Format("{0}.{1}", ver.Major, ver.Minor);

                    if (String.Compare(version, "0.0", false) == 0)

                        version = "1.0";
                    if (subkey != null) subkey.SetValue("", version);
                }
            }
        }

        [ComUnregisterFunction]
        private static void ComUnregister(Type t)
        {
            Registry.ClassesRoot.DeleteSubKeyTree(@"CLSID\" + t.GUID.ToString("B"));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Create the ElementHost control for hosting the
            // WPF UserControl.
            ElementHost host = new ElementHost();
            host.Dock = DockStyle.Fill;

            // Create the WPF UserControl.
            HostingWpfUserControlInWf.UserControl1 uc = new HostingWpfUserControlInWf.UserControl1();

            // Assign the WPF UserControl to the ElementHost control's
            // Child property.
            host.Child = uc;

            // Add the ElementHost control to the form's
            // collection of child controls.
            this.Controls.Add(host);
        }
    }
}
