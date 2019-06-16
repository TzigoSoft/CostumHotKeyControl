using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TzigoSoft
{
    public partial class Demo : Form
    {
        #region variables

        //required constants und importfunctions
        enum KeyModifier { None = 0, Alt = 1, Control = 2, Shift = 4, }
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private static string MainPath = Application.StartupPath;
        #endregion

        public Demo()
        {
            InitializeComponent();

            if (File.Exists(Path.Combine(MainPath, "userconfig.ini")))
            {
                //if the Hotkey was saved before exiting the Program, it can be loaded here
                try
                {
                    //transmission of the saved string to the register function of the UserHotKeyControls
                    customHotkeyControl1.LoadHotKey(File.ReadAllText(Path.Combine(MainPath, "userconfig.ini")));

                    //Setting the Hotkey with the returned variables from CustomHotkeyControl
                    RegisterHotKey(this.Handle, 0, CustomHotkeyControl.A + CustomHotkeyControl.C + CustomHotkeyControl.S, CustomHotkeyControl.K.GetHashCode());
                    label2.Text = CustomHotkeyControl.Display;
                    SavecheckBox.Checked = true;

                    //locks the CustomHotkeyControl against unintentional changes
                    customHotkeyControl1.locked = true;
                }

                //in an error occurs, the defaulthotkey is loaded
                catch
                {
                    RegisterHotKey(this.Handle, 0, 2, Keys.F1.GetHashCode());
                    customHotkeyControl1.Text = "Control + F1";
                    label2.Text = "Control" + " + " + "F1";

                    //locks the CustomHotkeyControl against unintentional changes
                    customHotkeyControl1.locked = true;
                }
            }

            //otherwise a defaulthotkey can be loaded
            else
            {
                RegisterHotKey(this.Handle, 0, 2, Keys.F1.GetHashCode());
                customHotkeyControl1.Text = "Control + F1";
                label2.Text = "Control" + " + " + "F1";

                //locks the CustomHotkeyControl against unintentional changes
                customHotkeyControl1.locked = true;
            }
        }

        private void Demo_Load(object sender, EventArgs e)
        {

        }

        //hotkey selection and registration
        private void CustomHotkeyControl1_KeyUp(object sender, KeyEventArgs e)
        {
            //Querying if the hotkey is valid
            if (CustomHotkeyControl.register)
            {
                UnregisterHotKey(this.Handle, 0);

                //Setting the Hotkey with the returned variables from CustomHotkeyControl
                RegisterHotKey(this.Handle, 0, CustomHotkeyControl.A + CustomHotkeyControl.C + CustomHotkeyControl.S, CustomHotkeyControl.K.GetHashCode());

                //CustomHotkeyControl.Display returns the hotkey as a string
                label2.Text = CustomHotkeyControl.Display;

                //locks the CustomHotkeyControl against unintentional changes
                customHotkeyControl1.locked = true;

                //here the hotkey canbe saved for usage after a programm restart
                if (SavecheckBox.Checked == true)
                {
                    //CustomHotkeyControl.SaveHotKey returns the hotkey as a string
                    File.WriteAllText(Path.Combine(MainPath, "userconfig.ini"), CustomHotkeyControl.SaveHotKey);
                }
            }
        }

        //resetting the hotkey per Buttonclick
        private void ResetButton_Click(object sender, EventArgs e)
        {
            //hotkey gets unregisterd
            UnregisterHotKey(this.Handle, 0);

            //reset of the CustomHotkeyControl 
            customHotkeyControl1.Reset();
            label2.Text = customHotkeyControl1.Text;

            //makes the saved varables invalid
            CustomHotkeyControl.register = false;
            if (SavecheckBox.Checked == true)
            {

                //Reset hotkey to default if it should be saved
                File.WriteAllText(Path.Combine(MainPath, "userconfig.ini"), CustomHotkeyControl.SaveHotKey);
            }

            MessageBox.Show("Hotkey is unregistered");
        }

        //or resetting the hotkey per rightclick
        private void RightClickReset(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //checks if the CustomHotkeyControl is locked
                //otherwise right clicks on the CustomHotkeyControl are ignored to block the contextmenu of windows
                if (customHotkeyControl1.locked == true)
                {
                    //Hotkey gets unregisterd
                    UnregisterHotKey(this.Handle, 0);

                    //resets the CustomHotkeyControl
                    customHotkeyControl1.Reset();
                    label2.Text = customHotkeyControl1.Text;

                    //makes the saved varables invalid
                    CustomHotkeyControl.register = false;
                    if (SavecheckBox.Checked == true)
                    {
                        //Reset hotkey to default if it should be saved
                        File.WriteAllText(Path.Combine(MainPath, "userconfig.ini"), "Control + F1");
                    }

                    MessageBox.Show("Hotkey is unregistered");
                }
            }
        }

        //Monitor the set hotkey for further use
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0x0312)
            {
                Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                KeyModifier modifier = (KeyModifier)((int)m.LParam & 0xFFFF);
                int id = m.WParam.ToInt32();
                if (id == 0)
                {
                    MessageBox.Show("Hotkey is registered and you can do any work with this.");
                }
            }
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.LinkVisited = true;
            System.Diagnostics.Process.Start("https://www.tzigosoft.de/");
        }
    }
}
