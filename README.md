# 

A Textbox based control to set, display and save custom hotkeys written in C#.

Available as dll and seperate class

It is easy to integrate and use in other C # projects.


If you have questions, be free to ask me.
If you find Bugs, please report them in the Comments or DM me.


# Usage & Example

You can use it like a text box and enter the Hotkey you wish. He will be displayed in the control, registered as a Hotkey and returned as seperate variable to save it for usage after restart. 

To use the CustomHotkeyControl.dll, simply include it as a reference in your project.
If its not shown in the MS Visual Studio Toolbox, simply drag from the  DLL from Windows Explorer straight to the Toolbox and if needed compile (F5).

To integrate as a Class in your Project in MS Visual Studio paste the CostumHotKeyControlClass project in your Project folder.

You can drag and drop the CostumHotKeyControl class in your Project.

Then its usable like the DLL.
If its not shown in the MS Visual Studio Toolbox, simply drag from the  DLL from Windows Explorer straight to the Toolbox and if needed compile (F5). (respective DLL)

For DLL: First reference TzigoSoft in the Class you would like to use it. 

The following must be integrated into the classes constructor:

        enum KeyModifier
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,

        }

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

Methods and variables:

You can call it with standart KeyUp events (Key EventArgs)

        UnregisterHotKey(this.Handle, id);
        RegisterHotKey(this.Handle, id, CustomHotkeyControl.A + CustomHotkeyControl.C + CustomHotkeyControl.S, CustomHotkeyControl.K.GetHashCode());
        string saveitwhereeveryouwant = CostumHotkeyControl.SaveHotKey;
        
   Replace id with the Hotkey id you would like to use starting with 0 - ...
   CustomHotkeyControl.A + B + C     return the fsModifiers as int
   CustomHotkeyControl.K             returns the in control pressed Keys
   CostumHotkeyControl.SaveHotKey is used to save the Hotkey for usage after restart.
   
To reset and set on default Hotkey:

        UnregisterHotKey(this.Handle, id);
        RegisterHotKey(this.Handle, id, int fsModifiers, int vk);
        CustomHotkeyControl.Reset();

  Replace the variables after RegisterHotKey with your default Hotkey variables.
  
   e.g.
  
        RegisterHotKey(this.Handle, 0, 1, Keys.F1.GetHashCode());

To load Hotkey after restart use the previously saved variable from string SaveHotKey you can use the following:

            CustomHotkeyControl.LoadHotKey(hotkeyload);
            RegisterHotKey(this.Handle, 0, CustomHotkeyControl.A + CustomHotkeyControl.C + CustomHotkeyControl.S, CustomHotkeyControl.K.GetHashCode());
            
# License

GNU General Public License v3.0

