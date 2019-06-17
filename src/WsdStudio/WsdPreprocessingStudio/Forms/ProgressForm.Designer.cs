namespace WsdPreprocessingStudio.Forms
{
    partial class ProgressForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.ProgressLabel = new System.Windows.Forms.Label();
            this.CancelOperationButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(12, 25);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(386, 23);
            this.ProgressBar.TabIndex = 0;
            // 
            // ProgressLabel
            // 
            this.ProgressLabel.AutoSize = true;
            this.ProgressLabel.Location = new System.Drawing.Point(12, 9);
            this.ProgressLabel.Name = "ProgressLabel";
            this.ProgressLabel.Size = new System.Drawing.Size(57, 13);
            this.ProgressLabel.TabIndex = 1;
            this.ProgressLabel.Text = "Progress...";
            // 
            // CancelOperationButton
            // 
            this.CancelOperationButton.Location = new System.Drawing.Point(404, 24);
            this.CancelOperationButton.Name = "CancelOperationButton";
            this.CancelOperationButton.Size = new System.Drawing.Size(71, 25);
            this.CancelOperationButton.TabIndex = 2;
            this.CancelOperationButton.Text = "Cancel";
            this.CancelOperationButton.UseVisualStyleBackColor = true;
            // 
            // ProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 61);
            this.ControlBox = false;
            this.Controls.Add(this.CancelOperationButton);
            this.Controls.Add(this.ProgressLabel);
            this.Controls.Add(this.ProgressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(503, 100);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(503, 100);
            this.Name = "ProgressForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Progress...";
            this.Load += new System.EventHandler(this.ProgressForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ProgressBar ProgressBar;
        internal System.Windows.Forms.Label ProgressLabel;
        internal System.Windows.Forms.Button CancelOperationButton;
    }
}