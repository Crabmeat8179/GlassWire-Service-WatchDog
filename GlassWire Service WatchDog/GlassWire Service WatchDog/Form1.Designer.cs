namespace GlassWire_Service_WatchDog
{
    partial class Interval_Changer_Window
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
            User_Inputted_time = new TextBox();
            label1 = new Label();
            button1 = new Button();
            SuspendLayout();
            // 
            // User_Inputted_time
            // 
            User_Inputted_time.Location = new Point(24, 35);
            User_Inputted_time.Name = "User_Inputted_time";
            User_Inputted_time.Size = new Size(296, 23);
            User_Inputted_time.TabIndex = 0;
            User_Inputted_time.Text = "30";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(122, 17);
            label1.Name = "label1";
            label1.Size = new Size(93, 15);
            label1.TabIndex = 1;
            label1.Text = "Time in seconds";
            // 
            // button1
            // 
            button1.Location = new Point(122, 64);
            button1.Name = "button1";
            button1.Size = new Size(93, 23);
            button1.TabIndex = 2;
            button1.Text = "Change";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Interval_Changer_Window
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(344, 93);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(User_Inputted_time);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Interval_Changer_Window";
            Text = "Interval Changer";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox User_Inputted_time;
        private Label label1;
        private Button button1;
    }
}