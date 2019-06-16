namespace TzigoSoft
{
    partial class Demo
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
            this.ResetButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SavecheckBox = new System.Windows.Forms.CheckBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.customHotkeyControl1 = new TzigoSoft.CustomHotkeyControl();
            this.SuspendLayout();
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(221, 66);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(75, 23);
            this.ResetButton.TabIndex = 1;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(42, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Current Hotkey is: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(195, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "label2";
            // 
            // SavecheckBox
            // 
            this.SavecheckBox.AutoSize = true;
            this.SavecheckBox.Location = new System.Drawing.Point(45, 112);
            this.SavecheckBox.Name = "SavecheckBox";
            this.SavecheckBox.Size = new System.Drawing.Size(213, 17);
            this.SavecheckBox.TabIndex = 4;
            this.SavecheckBox.Text = "Save UserHotkey for usage after restart";
            this.SavecheckBox.UseVisualStyleBackColor = true;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(125, 143);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(75, 16);
            this.linkLabel1.TabIndex = 13;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "(c) TzigoSoft";
            // 
            // customHotkeyControl1
            // 
            this.customHotkeyControl1.BackColor = System.Drawing.Color.White;
            this.customHotkeyControl1.Location = new System.Drawing.Point(45, 68);
            this.customHotkeyControl1.Name = "customHotkeyControl1";
            this.customHotkeyControl1.ShortcutsEnabled = false;
            this.customHotkeyControl1.Size = new System.Drawing.Size(140, 20);
            this.customHotkeyControl1.TabIndex = 0;
            this.customHotkeyControl1.Text = "---";
            this.customHotkeyControl1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.customHotkeyControl1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CustomHotkeyControl1_KeyUp);
            this.customHotkeyControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RightClickReset);
            // 
            // Demo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 182);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.SavecheckBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.customHotkeyControl1);
            this.Name = "Demo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UserHotkey Demo";
            this.Load += new System.EventHandler(this.Demo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomHotkeyControl customHotkeyControl1;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox SavecheckBox;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}