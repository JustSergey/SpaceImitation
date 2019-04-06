namespace SpaceImitation
{
    partial class Main_Form
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.button_play = new System.Windows.Forms.Button();
            this.draw_timer = new System.Windows.Forms.Timer(this.components);
            this.pattern_comboBox = new System.Windows.Forms.ComboBox();
            this.button_remove = new System.Windows.Forms.Button();
            this.velocity_label = new System.Windows.Forms.Label();
            this.tail_checkBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pictureBox.Location = new System.Drawing.Point(0, 43);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(1209, 736);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseDown);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseMove);
            this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseUp);
            // 
            // timer
            // 
            this.timer.Interval = 10;
            this.timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // button_play
            // 
            this.button_play.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_play.Location = new System.Drawing.Point(12, 12);
            this.button_play.Name = "button_play";
            this.button_play.Size = new System.Drawing.Size(75, 25);
            this.button_play.TabIndex = 1;
            this.button_play.Text = "Play";
            this.button_play.UseVisualStyleBackColor = true;
            this.button_play.Click += new System.EventHandler(this.Button_play_Click);
            // 
            // draw_timer
            // 
            this.draw_timer.Enabled = true;
            this.draw_timer.Interval = 10;
            this.draw_timer.Tick += new System.EventHandler(this.Draw_timer_Tick);
            // 
            // pattern_comboBox
            // 
            this.pattern_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pattern_comboBox.FormattingEnabled = true;
            this.pattern_comboBox.Items.AddRange(new object[] {
            "Earth",
            "Moon",
            "Jupiter"});
            this.pattern_comboBox.Location = new System.Drawing.Point(894, 15);
            this.pattern_comboBox.Name = "pattern_comboBox";
            this.pattern_comboBox.Size = new System.Drawing.Size(141, 21);
            this.pattern_comboBox.TabIndex = 2;
            this.pattern_comboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Pattern_comboBox_KeyPress);
            // 
            // button_remove
            // 
            this.button_remove.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_remove.Location = new System.Drawing.Point(1093, 12);
            this.button_remove.Name = "button_remove";
            this.button_remove.Size = new System.Drawing.Size(104, 25);
            this.button_remove.TabIndex = 3;
            this.button_remove.Text = "Remove Last";
            this.button_remove.UseVisualStyleBackColor = true;
            this.button_remove.Click += new System.EventHandler(this.Button_remove_Click);
            // 
            // velocity_label
            // 
            this.velocity_label.AutoSize = true;
            this.velocity_label.Location = new System.Drawing.Point(804, 18);
            this.velocity_label.Name = "velocity_label";
            this.velocity_label.Size = new System.Drawing.Size(71, 13);
            this.velocity_label.TabIndex = 6;
            this.velocity_label.Text = "Velocity: 0.00";
            // 
            // tail_checkBox
            // 
            this.tail_checkBox.AutoSize = true;
            this.tail_checkBox.Location = new System.Drawing.Point(93, 17);
            this.tail_checkBox.Name = "tail_checkBox";
            this.tail_checkBox.Size = new System.Drawing.Size(43, 17);
            this.tail_checkBox.TabIndex = 7;
            this.tail_checkBox.Text = "Tail";
            this.tail_checkBox.UseVisualStyleBackColor = true;
            // 
            // Main_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1209, 779);
            this.Controls.Add(this.tail_checkBox);
            this.Controls.Add(this.velocity_label);
            this.Controls.Add(this.button_remove);
            this.Controls.Add(this.pattern_comboBox);
            this.Controls.Add(this.button_play);
            this.Controls.Add(this.pictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Main_Form";
            this.Text = "Space";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Main_Form_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button button_play;
        private System.Windows.Forms.Timer draw_timer;
        private System.Windows.Forms.ComboBox pattern_comboBox;
        private System.Windows.Forms.Button button_remove;
        private System.Windows.Forms.Label velocity_label;
        private System.Windows.Forms.CheckBox tail_checkBox;
    }
}

