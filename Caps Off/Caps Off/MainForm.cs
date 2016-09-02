using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Caps_Off
{
    public partial class MainForm : Form
    {
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        public MainForm()
        {
            InitializeComponent();
            Microsoft.Win32.SystemEvents.SessionSwitch += new Microsoft.Win32.SessionSwitchEventHandler(SessionSwitched);
        }

        void SessionSwitched(object sender, Microsoft.Win32.SessionSwitchEventArgs e)
        {
            if (e.Reason == Microsoft.Win32.SessionSwitchReason.SessionLock)
            {
                if (Control.IsKeyLocked(Keys.CapsLock))
                {
                    keybd_event(0x14, 0x45, 0x1, (UIntPtr)0);
                    keybd_event(0x14, 0x45, 0x1 | 0x2, (UIntPtr)0);
                }
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {   //This must be done in _Shown instead of Main because Main runs before the form actually appears on screen
            WindowState = FormWindowState.Minimized;
            Hide();
        }
    }
}
