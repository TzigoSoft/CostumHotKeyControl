# UserHotkey.dll

A small library to set, display and save custom hotkeys written in C#.

The Hotkeycontrol is based on A simple hotkey selection control by Telrunya on code project.

It is easy to integrate and use in other C # projects.


If you have Questions, be free to ask me.
If you find Bugs, please report them in the Comments or DM me.


# Usage & Example

You can use it like a text box and enter the Hotkey you wish. He will be displayed in the control, registered as a Hotkey and returned as seperate variable to save it for usage after restart. 

To use the UserHotkey.dll, simply include it as a reference in your project.

First reference UserHotkey in the Class you would like to use it. 

        using UserHotkey;

        this.HotkeyControl = new UserHotkey.UserHotkeyControl();

The following must be integrated into the classes constructor:

        enum KeyModifier
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,

        }

        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

Methods and variables:

You can call it with standart KeyUp events (Key EventArgs)

        UnregisterHotKey(this.Handle, id);
        RegisterHotKey(this.Handle, id, HotkeyRegister.A + HotkeyRegister.C + HotkeyRegister.S, HotkeyRegister.K.GetHashCode());
        string saveitwhereeveryouwant = UserHotkeyControl.SaveHotKey;
        
   Replace id with the Hotkey id you would like to use starting with 0 - ...
   HotkeyRegister.A + B + C     return the fsModifiers as int
   HotkeyRegister.K             returns the in control pressed Keys
   UserHotkeyControl.SaveHotKey is used to save the Hotkey for usage after restart.
   
To reset and set on default Hotkey:

        UnregisterHotKey(this.Handle, id);
        RegisterHotKey(this.Handle, id, int fsModifiers, int vk);
        this.HotkeyControl.Reset();

  Replace the variables after RegisterHotKey with your default Hotkey variables.
  
   e.g.
  
        RegisterHotKey(this.Handle, 0, 1, Keys.F1.GetHashCode());

To load Hotkey after restart use the previously saved variable from string SaveHotKey you can use the following:

            this.HotkeyControl.LoadHotKey(hotkeyload);
            RegisterHotKey(this.Handle, 0, HotkeyRegister.A + HotkeyRegister.C + HotkeyRegister.S, HotkeyRegister.K.GetHashCode());
            
# License

GNU General Public License v3.0

