namespace WsdPreprocessingStudio.StatisticsPlugin
{
    partial class StatisticsForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ExecutionPriority_UpDown = new System.Windows.Forms.NumericUpDown();
            this.EnablePlugin_Checkbox = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Statistics_GroupBox = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.MinimumExamples_UpDown = new System.Windows.Forms.NumericUpDown();
            this.RequireTestSet_CheckBox = new System.Windows.Forms.CheckBox();
            this.RequireTrainingSet_CheckBox = new System.Windows.Forms.CheckBox();
            this.AbortGeneration_CheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ExecutionPriority_UpDown)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.Statistics_GroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MinimumExamples_UpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ExecutionPriority_UpDown);
            this.groupBox1.Controls.Add(this.EnablePlugin_Checkbox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(374, 461);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Plugin";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Handler execution priority:";
            // 
            // ExecutionPriority_UpDown
            // 
            this.ExecutionPriority_UpDown.Location = new System.Drawing.Point(141, 41);
            this.ExecutionPriority_UpDown.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.ExecutionPriority_UpDown.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.ExecutionPriority_UpDown.Name = "ExecutionPriority_UpDown";
            this.ExecutionPriority_UpDown.Size = new System.Drawing.Size(120, 20);
            this.ExecutionPriority_UpDown.TabIndex = 1;
            this.ExecutionPriority_UpDown.ValueChanged += new System.EventHandler(this.ExecutionPriority_UpDown_ValueChanged);
            // 
            // EnablePlugin_Checkbox
            // 
            this.EnablePlugin_Checkbox.AutoSize = true;
            this.EnablePlugin_Checkbox.Location = new System.Drawing.Point(9, 19);
            this.EnablePlugin_Checkbox.Name = "EnablePlugin_Checkbox";
            this.EnablePlugin_Checkbox.Size = new System.Drawing.Size(90, 17);
            this.EnablePlugin_Checkbox.TabIndex = 0;
            this.EnablePlugin_Checkbox.Text = "Enable plugin";
            this.EnablePlugin_Checkbox.UseVisualStyleBackColor = true;
            this.EnablePlugin_Checkbox.CheckedChanged += new System.EventHandler(this.EnablePlugin_Checkbox_CheckedChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Statistics_GroupBox, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(760, 467);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // Statistics_GroupBox
            // 
            this.Statistics_GroupBox.Controls.Add(this.AbortGeneration_CheckBox);
            this.Statistics_GroupBox.Controls.Add(this.label2);
            this.Statistics_GroupBox.Controls.Add(this.MinimumExamples_UpDown);
            this.Statistics_GroupBox.Controls.Add(this.RequireTestSet_CheckBox);
            this.Statistics_GroupBox.Controls.Add(this.RequireTrainingSet_CheckBox);
            this.Statistics_GroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Statistics_GroupBox.Location = new System.Drawing.Point(383, 3);
            this.Statistics_GroupBox.Name = "Statistics_GroupBox";
            this.Statistics_GroupBox.Size = new System.Drawing.Size(374, 461);
            this.Statistics_GroupBox.TabIndex = 1;
            this.Statistics_GroupBox.TabStop = false;
            this.Statistics_GroupBox.Text = "Statistics";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(192, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Minimum (training+validation) examples:";
            // 
            // MinimumExamples_UpDown
            // 
            this.MinimumExamples_UpDown.Location = new System.Drawing.Point(206, 63);
            this.MinimumExamples_UpDown.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.MinimumExamples_UpDown.Name = "MinimumExamples_UpDown";
            this.MinimumExamples_UpDown.Size = new System.Drawing.Size(120, 20);
            this.MinimumExamples_UpDown.TabIndex = 3;
            this.MinimumExamples_UpDown.ValueChanged += new System.EventHandler(this.MinimumExamples_UpDown_ValueChanged);
            // 
            // RequireTestSet_CheckBox
            // 
            this.RequireTestSet_CheckBox.AutoSize = true;
            this.RequireTestSet_CheckBox.Location = new System.Drawing.Point(11, 42);
            this.RequireTestSet_CheckBox.Name = "RequireTestSet_CheckBox";
            this.RequireTestSet_CheckBox.Size = new System.Drawing.Size(100, 17);
            this.RequireTestSet_CheckBox.TabIndex = 2;
            this.RequireTestSet_CheckBox.Text = "Require test set";
            this.RequireTestSet_CheckBox.UseVisualStyleBackColor = true;
            this.RequireTestSet_CheckBox.CheckedChanged += new System.EventHandler(this.RequireTestSet_CheckBox_CheckedChanged);
            // 
            // RequireTrainingSet_CheckBox
            // 
            this.RequireTrainingSet_CheckBox.AutoSize = true;
            this.RequireTrainingSet_CheckBox.Location = new System.Drawing.Point(11, 19);
            this.RequireTrainingSet_CheckBox.Name = "RequireTrainingSet_CheckBox";
            this.RequireTrainingSet_CheckBox.Size = new System.Drawing.Size(117, 17);
            this.RequireTrainingSet_CheckBox.TabIndex = 0;
            this.RequireTrainingSet_CheckBox.Text = "Require training set";
            this.RequireTrainingSet_CheckBox.UseVisualStyleBackColor = true;
            this.RequireTrainingSet_CheckBox.CheckedChanged += new System.EventHandler(this.RequireTrainingSet_CheckBox_CheckedChanged);
            // 
            // AbortGeneration_CheckBox
            // 
            this.AbortGeneration_CheckBox.AutoSize = true;
            this.AbortGeneration_CheckBox.Location = new System.Drawing.Point(11, 86);
            this.AbortGeneration_CheckBox.Name = "AbortGeneration_CheckBox";
            this.AbortGeneration_CheckBox.Size = new System.Drawing.Size(239, 17);
            this.AbortGeneration_CheckBox.TabIndex = 5;
            this.AbortGeneration_CheckBox.Text = "Abort generation after statistics are computed";
            this.AbortGeneration_CheckBox.UseVisualStyleBackColor = true;
            this.AbortGeneration_CheckBox.CheckedChanged += new System.EventHandler(this.AbortGeneration_CheckBox_CheckedChanged);
            // 
            // StatisticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(760, 467);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "StatisticsForm";
            this.Text = "StatisticsForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ExecutionPriority_UpDown)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.Statistics_GroupBox.ResumeLayout(false);
            this.Statistics_GroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MinimumExamples_UpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox EnablePlugin_Checkbox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.NumericUpDown ExecutionPriority_UpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox Statistics_GroupBox;
        private System.Windows.Forms.CheckBox RequireTestSet_CheckBox;
        private System.Windows.Forms.CheckBox RequireTrainingSet_CheckBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown MinimumExamples_UpDown;
        private System.Windows.Forms.CheckBox AbortGeneration_CheckBox;
    }
}