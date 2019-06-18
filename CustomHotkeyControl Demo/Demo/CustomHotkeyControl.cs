using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections;

namespace TzigoSoft
{
    public partial class CustomHotkeyControl : TextBox
    {
        #region Variables

        //required variables
        private Keys hotkey = Keys.None;
        private Keys modifiers = Keys.None;
        private string processed;
        private static string DisplayKey;
        private static string DisplayMods;
        public static string Display;
        public static Keys K;
        public static int A;
        public static int C;
        public static int S;
        public static string SaveHotKey;
        public static bool register;
        public bool locked;

        #endregion

        //ArrayLists, which contain not allowed shortcuts

        private ArrayList NoShiftAllowed;
        private ArrayList NoAltGrAllowed;
        private ArrayList NoControlAllowed;

        public CustomHotkeyControl()
        {
            InitializeComponent();

            //the required events
            this.KeyUp += new KeyEventHandler(CustomHotkeyControl_KeyUp);
            this.KeyDown += new KeyEventHandler(CustomHotkeyControl_KeyDown);
            this.MouseDown += new MouseEventHandler(CustomHotkeyControl_MouseDown);

            //prevents the windows contextmenu
            this.ShortcutsEnabled = false;

            NoShiftAllowed = new ArrayList();
            NoAltGrAllowed = new ArrayList();
            NoControlAllowed = new ArrayList();

            ForbiddenLists();
        }

        //Reading forbidden shortcuts into the ArrayLists
        private void ForbiddenLists()
        {
            //Shift + 0 - 9, A - Z
            for (Keys i = Keys.D0; i <= Keys.Z; i++) NoShiftAllowed.Add((int)i);

            //Shift + Numblock Keys
            for (Keys i = Keys.NumPad0; i <= Keys.NumPad9; i++) NoShiftAllowed.Add((int)i);


            //Shift + miscellaneous eg ,;<.+#
            for (Keys i = Keys.Oem1; i <= Keys.OemBackslash; i++)
                NoShiftAllowed.Add((int)i);

            //Shift + Space, PgUp, PgDn, End, Home
            for (Keys i = Keys.Space; i <= Keys.Home; i++)
                NoShiftAllowed.Add((int)i);

            //Shift + function keys
            for (Keys i = Keys.F1; i <= Keys.F12; i++) NoShiftAllowed.Add((int)i);
            
            //Various Keys, that cant be used
            NoShiftAllowed.Add((int)Keys.Insert);
            NoShiftAllowed.Add((int)Keys.Help);
            NoShiftAllowed.Add((int)Keys.Multiply);
            NoShiftAllowed.Add((int)Keys.Add);
            NoShiftAllowed.Add((int)Keys.Subtract);
            NoShiftAllowed.Add((int)Keys.Divide);
            NoShiftAllowed.Add((int)Keys.Decimal);
            NoShiftAllowed.Add((int)Keys.Return);
            NoShiftAllowed.Add((int)Keys.Escape);
            NoShiftAllowed.Add((int)Keys.NumLock);
            NoShiftAllowed.Add((int)Keys.Scroll);
            NoShiftAllowed.Add((int)Keys.Pause);
            NoShiftAllowed.Add((int)Keys.OemBackslash);


            //Ctrl+Alt + 0 - 9
            for (Keys i = Keys.D0; i <= Keys.D9; i++) NoAltGrAllowed.Add((int)i);

            NoAltGrAllowed.Add((int)Keys.Oemplus);
            NoAltGrAllowed.Add((int)Keys.OemOpenBrackets);
            NoAltGrAllowed.Add((int)Keys.OemBackslash);

            //Ctrl + various standart windows hotkeys
            NoControlAllowed.Add((int)Keys.A);
            NoControlAllowed.Add((int)Keys.C);
            NoControlAllowed.Add((int)Keys.D);
            NoControlAllowed.Add((int)Keys.S);
            NoControlAllowed.Add((int)Keys.V);
            NoControlAllowed.Add((int)Keys.X);
            NoControlAllowed.Add((int)Keys.Y);
            NoControlAllowed.Add((int)Keys.Z);
        }

        //locks rightclick, while HotkeyControl is unlocked, otherwise it prevents the appearance of the window's own contextmenus
        //locks leftclick if CustomHotkeyControl is locked
        private void CustomHotkeyControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (locked == false) { }
            }

            if (e.Button == MouseButtons.Left)
            {
                if (locked) { }
            }
        }

        void CustomHotkeyControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (locked == false)
            {
                //shows if a modifiers is existing
                if (Control.ModifierKeys == Keys.None)
                {
                    this.BackColor = Color.Red;
                    this.Font = new Font(this.Font, FontStyle.Bold);
                    this.Text = "---";
                }

                //prohibit backspace and delete key
                if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                {
                    this.hotkey = Keys.None;
                    this.modifiers = Keys.None;
                    this.BackColor = Color.Red;
                    this.Font = new Font(this.Font, FontStyle.Bold);
                    this.Text = "---";
                    return;
                }

                //prohibit standalone modifiers
                if (e.KeyCode == (Keys.Alt | Keys.None) || e.KeyCode == (Keys.Control | Keys.None) || e.KeyCode == (Keys.Shift | Keys.None))
                {
                    this.hotkey = Keys.None;
                    this.modifiers = Keys.None;
                    this.BackColor = Color.Red;
                    this.Font = new Font(this.Font, FontStyle.Bold);
                    this.Text = "---";
                }

                else
                {
                    this.modifiers = e.Modifiers;
                    this.hotkey = e.KeyCode;

                    //prevents the faulty display when pressing the modifiers (eg when pressing Shift normally ShiftKey + Shift is displayed )
                    if (this.hotkey == Keys.Menu || this.hotkey == Keys.ShiftKey || this.hotkey == Keys.ControlKey || this.hotkey == Keys.Alt )
                    {
                        this.hotkey = Keys.None;
                        this.modifiers = Keys.None;
                        this.BackColor = Color.Red;
                        this.Font = new Font(this.Font, FontStyle.Bold);
                        this.Text = "---";
                        return;
                    }

                    //Hotkey is reconciled with the AltGR-List
                    if ((this.modifiers == (Keys.Alt | Keys.Control)) && this.NoAltGrAllowed.Contains((int)this.hotkey))
                    {
                        //if existing in list, reset
                        this.hotkey = Keys.None;
                        this.modifiers = Keys.None;
                        this.BackColor = Color.Red;
                        this.Font = new Font(this.Font, FontStyle.Bold);
                        this.Text = "---";
                        return;
                    }

                    //Hotkey is reconciled with the Control-List
                    if ((this.modifiers == (Keys.Control)) && this.NoControlAllowed.Contains((int)this.hotkey))
                    {
                        //if existing in list, reset
                        this.hotkey = Keys.None;
                        this.modifiers = Keys.None;
                        this.BackColor = Color.Red;
                        this.Font = new Font(this.Font, FontStyle.Bold);
                        this.Text = "---";
                        return;
                    }

                    //Hotkey is reconciled with the Shift-List
                    if ((this.modifiers == (Keys.Shift)) && this.NoShiftAllowed.Contains((int)this.hotkey))
                    {
                        //if existing in list, reset
                        this.hotkey = Keys.None;
                        this.modifiers = Keys.None;
                        this.BackColor = Color.White;
                        this.Font = new Font(this.Font, FontStyle.Regular);
                        this.Text = "---";
                        return;
                    }

                    else
                    {
                        //else allow
                        this.BackColor = Color.Green;
                        this.Font = new Font(this.Font, FontStyle.Bold);
                        this.Text = this.modifiers.ToString() + " + " + this.hotkey;
                    }
                }
            }
        }

        //Dispatched when all keys have been released. If the current hotkey is not valid, reset.
        void CustomHotkeyControl_KeyUp(object sender, KeyEventArgs e)
        {
            //checking if CustomHotkeyControl is locked
            if (locked == false)
            {
                //if Modifizierer oder Hotkey are empty, reset
                if (this.modifiers == (Keys.None) || this.hotkey == (Keys.None))
                {
                    this.hotkey = Keys.None;
                    this.modifiers = Keys.None;
                    this.BackColor = Color.Red;
                    this.Font = new Font(this.Font, FontStyle.Bold);
                    this.Text = "---";
                    return;
                }

                else
                {
                    //else transmission to the register function
                    processed = this.modifiers.ToString() + " + " + this.hotkey.ToString();
                    StringConversion(processed);
                    //hotkey is retruned as string for displaying
                    Display = DisplayMods + " + " + DisplayKey;

                    //MessageBox as query if the hotkey is correcct
                    string message = "Do you want to register " + Display + " as custom hotkey";
                    DialogResult result;
                    result = MessageBox.Show(message, "Confirmation", MessageBoxButtons.OKCancel);

                    //if yes, show hotkey, set Boolean register to true and lock CustonHotKey
                    if (result == DialogResult.OK)
                    {
                        SaveHotKey = processed;
                        this.Text = Display;
                        register = true;
                        this.SelectionLength = 0;
                        this.BackColor = Color.White;
                        this.Font = new Font(this.Font, FontStyle.Bold);
                        locked = true;
                    }

                    //if not, reset
                    if (result == DialogResult.Cancel)
                    {
                        register = false;
                        this.Text = "---";
                        this.Cursor = Cursors.Default;
                        this.BackColor = Color.White;
                        this.Font = new Font(this.Font, FontStyle.Regular);
                    }
                }
            }
        }

        //reset function
        public void Reset()
        {
            this.Text = "No hotkey registered";
            locked = false;
            this.hotkey = Keys.None;
            this.modifiers = Keys.None;
            this.BackColor = Color.White;
            this.Font = new Font(this.Font, FontStyle.Regular);
        }

        //if the hotkey was saved before exiting the program, it can be set here again
        public void LoadHotKey(string savedhotkey)
        {
            StringConversion(savedhotkey);
            Display = DisplayMods + " + " + DisplayKey;
            this.Text = Display;
            locked = true;
            this.BackColor = Color.White;
            this.Font = new Font(this.Font, FontStyle.Bold);
        }

        //Start converting the string from the CustomHotkeyControl back to registrable modifier and key
        public static void StringConversion(string ControlOutput)
        {
            //checking if the hotkey is empty or incomplete
            if (ControlOutput.Contains("---") || ControlOutput.Contains(" none"))
            {
                return;
            }

            //splitting in modifier und key
            string[] split = ControlOutput.Split('+');
            if (split.Length == 2)
            {
                string key = split[1].Trim();

                bool ALT = split[0].Contains("Alt");
                bool CTRL = split[0].Contains("Control");
                bool SHIFT = split[0].Contains("Shift");

                //Conversion of the Modifizierer to Int values
                if (ALT && CTRL && SHIFT) { A = 1; C = 2; S = 4; DisplayMods = "Alt, Control, Shift"; }
                else if (ALT && CTRL) { A = 1; C = 2; S = 0; DisplayMods = "Alt, Control"; }
                else if (ALT && SHIFT) { A = 1; C = 0; S = 4; DisplayMods = "Alt, Shift"; }
                else if (CTRL && SHIFT) { A = 0; C = 2; S = 4; DisplayMods = "Control, Shift"; }
                else if (ALT) { A = 1; C = 0; S = 0; DisplayMods = "Alt"; }
                else if (CTRL) { A = 0; C = 2; S = 0; DisplayMods = "Control"; }
                else if (SHIFT) { A = 0; C = 0; S = 0; DisplayMods = "Shift"; }

                //Key is transferred to the next function for further processing
                Inputs(key);
            }
        }

        //further processing of the Key Strings
        private static void Inputs(string key)
        {
            //checking if Key is a single Letter. If yes, conversion to corrosponding Key
            string[] Alpha = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            if (key.Length == 1 && Alpha.Contains(key))
            {
                TypeConverter conv = TypeDescriptor.GetConverter(typeof(Keys));
                K = (Keys)conv.ConvertFromString(key);
            }

            //checking if Key is a numeric Key and transferring to corrosponding funcion
            else if (key.Length == 2 && key.StartsWith("D")) Numbers(key);

            //checking if Key is a function key and transferring to corrosponding function
            else if (key.Length == 2 && key.StartsWith("F")) Functions(key);
            else if (key.Length == 3 && key.StartsWith("F")) Functions(key);

            //checking if Key is a Numblock key and transferring to corrosponding function
            else if (key.Length > 3 && key.Contains("NumPad")) Numpad(key);

            //checking if Key is a other Key and transferring to corrosponding function
            else if (key.Length > 3 && key.Contains("Oem")) Oem(key);

            //checking various Keys and creating corrosponding Key, as well as display
            else if (key.Equals("Left")) { K = Keys.Left; DisplayKey = "Left"; }
            else if (key.Equals("Up")) { K = Keys.Up; DisplayKey = "Up"; }
            else if (key.Equals("Down")) { K = Keys.Down; DisplayKey = "Down"; }
            else if (key.Equals("Right")) { K = Keys.Right; DisplayKey = "Right"; }
            else if (key.Equals("Divide")) { K = Keys.Divide; DisplayKey = "Divide"; }
            else if (key.Equals("Multiply")) { K = Keys.Multiply; DisplayKey = "Multiply"; }
            else if (key.Equals("Add")) { K = Keys.Add; DisplayKey = "Add"; }
            else if (key.Equals("Subtract")) { K = Keys.Subtract; DisplayKey = "Subtract"; }
            else if (key.Equals("Pause")) { K = Keys.Pause; DisplayKey = "Pause"; }
            else if (key.Equals("Scroll")) { K = Keys.Scroll; DisplayKey = "Scroll"; }
            else if (key.Equals("Tab")) { K = Keys.Tab; DisplayKey = "Tab"; }
            else if (key.Equals("Decimal")) { K = Keys.Decimal; DisplayKey = "Decimal"; }
            else if (key.Equals("Insert")) { K = Keys.Insert; DisplayKey = "Insert"; }
        }

        //creating respective numeric key, as well as display
        private static void Numbers(string key)
        {
            if (key.Equals("D0")) { K = Keys.D0; DisplayKey = "0"; }
            else if (key.Equals("D1")) { K = Keys.D1; DisplayKey = "1"; }
            else if (key.Equals("D2")) { K = Keys.D2; DisplayKey = "2"; }
            else if (key.Equals("D3")) { K = Keys.D3; DisplayKey = "3"; }
            else if (key.Equals("D4")) { K = Keys.D4; DisplayKey = "4"; }
            else if (key.Equals("D5")) { K = Keys.D5; DisplayKey = "5"; }
            else if (key.Equals("D6")) { K = Keys.D6; DisplayKey = "6"; }
            else if (key.Equals("D7")) { K = Keys.D7; DisplayKey = "7"; }
            else if (key.Equals("D8")) { K = Keys.D8; DisplayKey = "8"; }
            else if (key.Equals("D9")) { K = Keys.D9; DisplayKey = "9"; }
        }

        //creating respective function key, as well as display
        private static void Functions(string key)
        {
            if (key.Equals("F1")) { K = Keys.F1; DisplayKey = "F1"; }
            else if (key.Equals("F2")) { K = Keys.F2; DisplayKey = "F2"; }
            else if (key.Equals("F3")) { K = Keys.F3; DisplayKey = "F3"; }
            else if (key.Equals("F4")) { K = Keys.F4; DisplayKey = "F4"; }
            else if (key.Equals("F5")) { K = Keys.F5; DisplayKey = "F5"; }
            else if (key.Equals("F6")) { K = Keys.F6; DisplayKey = "F6"; }
            else if (key.Equals("F7")) { K = Keys.F7; DisplayKey = "F7"; }
            else if (key.Equals("F8")) { K = Keys.F8; DisplayKey = "F8"; }
            else if (key.Equals("F9")) { K = Keys.F9; DisplayKey = "F9"; }
            else if (key.Equals("F10")) { K = Keys.F10; DisplayKey = "F10"; }
            else if (key.Equals("F11")) { K = Keys.F11; DisplayKey = "F11"; }
            else if (key.Equals("F12")) { K = Keys.F12; DisplayKey = "F12"; }
        }

        //creating respective numblock key, as well as display
        private static void Numpad(string key)
        {
            if (key.Equals("NumPad0")) { K = Keys.NumPad0; DisplayKey = "NumPad0"; }
            else if (key.Equals("NumPad1")) { K = Keys.NumPad1; DisplayKey = "NumPad1"; }
            else if (key.Equals("NumPad2")) { K = Keys.NumPad2; DisplayKey = "NumPad2"; }
            else if (key.Equals("NumPad3")) { K = Keys.NumPad3; DisplayKey = "NumPad3"; }
            else if (key.Equals("NumPad4")) { K = Keys.NumPad4; DisplayKey = "NumPad4"; }
            else if (key.Equals("NumPad5")) { K = Keys.NumPad5; DisplayKey = "NumPad5"; }
            else if (key.Equals("NumPad6")) { K = Keys.NumPad6; DisplayKey = "NumPad6"; }
            else if (key.Equals("NumPad7")) { K = Keys.NumPad7; DisplayKey = "NumPad7"; }
            else if (key.Equals("NumPad8")) { K = Keys.NumPad8; DisplayKey = "NumPad8"; }
            else if (key.Equals("NumPad9")) { K = Keys.NumPad9; DisplayKey = "NumPad9"; }
        }

        //creating respective other key, as well as display
        private static void Oem(string key)
        {
            if (key.Equals("Oem5")) { K = Keys.Oem5; DisplayKey = "^"; }
            else if (key.Equals("OemQuestion")) { K = Keys.OemQuestion; DisplayKey = "#"; }
            else if (key.Equals("Oemplus")) { K = Keys.Oemplus; DisplayKey = "+/~"; }
            else if (key.Equals("Oem1")) { K = Keys.Oem1; DisplayKey = "Ü"; }
            else if (key.Equals("Oemtilde")) { K = Keys.Oemtilde; DisplayKey = "Ö"; }
            else if (key.Equals("Oem7")) { K = Keys.Oem7; DisplayKey = "Ä"; }
            else if (key.Equals("OemBackslash")) { K = Keys.OemBackslash; DisplayKey = "<"; }
            else if (key.Equals("Oemcomma")) { K = Keys.Oemcomma; DisplayKey = "Comma"; }
            else if (key.Equals("OemPeriod")) { K = Keys.OemPeriod; DisplayKey = "./:"; }
            else if (key.Equals("OemMinus")) { K = Keys.OemMinus; DisplayKey = "-"; }
        }
    }

}
