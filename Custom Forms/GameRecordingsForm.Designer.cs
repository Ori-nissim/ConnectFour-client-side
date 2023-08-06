namespace BallsGame_315903518
{
    partial class GameRecordingsForm
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
            this.GamescomboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.OkBtnRecordingForm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // GamescomboBox
            // 
            this.GamescomboBox.FormattingEnabled = true;
            this.GamescomboBox.Location = new System.Drawing.Point(175, 196);
            this.GamescomboBox.Name = "GamescomboBox";
            this.GamescomboBox.Size = new System.Drawing.Size(413, 21);
            this.GamescomboBox.TabIndex = 0;
            this.GamescomboBox.SelectedIndexChanged += new System.EventHandler(this.GamescomboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(170, 159);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(214, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select a game to watch";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // OkBtnRecordingForm
            // 
            this.OkBtnRecordingForm.Location = new System.Drawing.Point(356, 365);
            this.OkBtnRecordingForm.Name = "OkBtnRecordingForm";
            this.OkBtnRecordingForm.Size = new System.Drawing.Size(117, 53);
            this.OkBtnRecordingForm.TabIndex = 2;
            this.OkBtnRecordingForm.Text = "OK";
            this.OkBtnRecordingForm.UseVisualStyleBackColor = true;
            this.OkBtnRecordingForm.Click += new System.EventHandler(this.OkBtnRecordingForm_Click);
            // 
            // GameRecordingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.OkBtnRecordingForm);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.GamescomboBox);
            this.Name = "GameRecordingsForm";
            this.Text = "GameRecordinsForm";
            this.Load += new System.EventHandler(this.GameRecordingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox GamescomboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button OkBtnRecordingForm;
    }
}