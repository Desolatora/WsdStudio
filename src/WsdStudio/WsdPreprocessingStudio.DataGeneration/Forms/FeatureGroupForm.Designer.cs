namespace WsdPreprocessingStudio.DataGeneration.Forms
{
    partial class FeatureGroupForm
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
            this.OkButton = new System.Windows.Forms.Button();
            this.SourceComboBox = new System.Windows.Forms.ComboBox();
            this.TypeComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ElementsListBox = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.AggregationComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CompressionComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ClearCompressionButton = new System.Windows.Forms.Button();
            this.ClearAggregationButton = new System.Windows.Forms.Button();
            this.ElementsGroupBox = new System.Windows.Forms.GroupBox();
            this.CancelButton = new System.Windows.Forms.Button();
            this.CompressionElementsGroupBox = new System.Windows.Forms.GroupBox();
            this.CompressionElementsListBox = new System.Windows.Forms.CheckedListBox();
            this.groupBox1.SuspendLayout();
            this.ElementsGroupBox.SuspendLayout();
            this.CompressionElementsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.Location = new System.Drawing.Point(545, 205);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(89, 32);
            this.OkButton.TabIndex = 0;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // SourceComboBox
            // 
            this.SourceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SourceComboBox.FormattingEnabled = true;
            this.SourceComboBox.Location = new System.Drawing.Point(9, 35);
            this.SourceComboBox.Name = "SourceComboBox";
            this.SourceComboBox.Size = new System.Drawing.Size(220, 21);
            this.SourceComboBox.TabIndex = 2;
            this.SourceComboBox.SelectedIndexChanged += new System.EventHandler(this.SourceComboBox_SelectedIndexChanged);
            // 
            // TypeComboBox
            // 
            this.TypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TypeComboBox.FormattingEnabled = true;
            this.TypeComboBox.Location = new System.Drawing.Point(9, 75);
            this.TypeComboBox.Name = "TypeComboBox";
            this.TypeComboBox.Size = new System.Drawing.Size(220, 21);
            this.TypeComboBox.TabIndex = 3;
            this.TypeComboBox.SelectedIndexChanged += new System.EventHandler(this.TypeComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Source:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Type:";
            // 
            // ElementsListBox
            // 
            this.ElementsListBox.CheckOnClick = true;
            this.ElementsListBox.FormattingEnabled = true;
            this.ElementsListBox.Location = new System.Drawing.Point(6, 19);
            this.ElementsListBox.Name = "ElementsListBox";
            this.ElementsListBox.Size = new System.Drawing.Size(223, 154);
            this.ElementsListBox.TabIndex = 6;
            this.ElementsListBox.SelectedIndexChanged += new System.EventHandler(this.ElementsListBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Aggregation:";
            // 
            // AggregationComboBox
            // 
            this.AggregationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AggregationComboBox.FormattingEnabled = true;
            this.AggregationComboBox.Location = new System.Drawing.Point(9, 115);
            this.AggregationComboBox.Name = "AggregationComboBox";
            this.AggregationComboBox.Size = new System.Drawing.Size(193, 21);
            this.AggregationComboBox.TabIndex = 7;
            this.AggregationComboBox.SelectedIndexChanged += new System.EventHandler(this.AggregationComboBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Compression:";
            // 
            // CompressionComboBox
            // 
            this.CompressionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CompressionComboBox.FormattingEnabled = true;
            this.CompressionComboBox.Location = new System.Drawing.Point(9, 155);
            this.CompressionComboBox.Name = "CompressionComboBox";
            this.CompressionComboBox.Size = new System.Drawing.Size(193, 21);
            this.CompressionComboBox.TabIndex = 9;
            this.CompressionComboBox.SelectedIndexChanged += new System.EventHandler(this.CompressionComboBox_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ClearCompressionButton);
            this.groupBox1.Controls.Add(this.ClearAggregationButton);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.SourceComboBox);
            this.groupBox1.Controls.Add(this.TypeComboBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.CompressionComboBox);
            this.groupBox1.Controls.Add(this.AggregationComboBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(235, 187);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parameters";
            // 
            // ClearCompressionButton
            // 
            this.ClearCompressionButton.Location = new System.Drawing.Point(208, 155);
            this.ClearCompressionButton.Name = "ClearCompressionButton";
            this.ClearCompressionButton.Size = new System.Drawing.Size(21, 21);
            this.ClearCompressionButton.TabIndex = 16;
            this.ClearCompressionButton.Text = "X";
            this.ClearCompressionButton.UseVisualStyleBackColor = true;
            this.ClearCompressionButton.Click += new System.EventHandler(this.ClearCompressionButton_Click);
            // 
            // ClearAggregationButton
            // 
            this.ClearAggregationButton.Location = new System.Drawing.Point(208, 115);
            this.ClearAggregationButton.Name = "ClearAggregationButton";
            this.ClearAggregationButton.Size = new System.Drawing.Size(21, 21);
            this.ClearAggregationButton.TabIndex = 15;
            this.ClearAggregationButton.Text = "X";
            this.ClearAggregationButton.UseVisualStyleBackColor = true;
            this.ClearAggregationButton.Click += new System.EventHandler(this.ClearAggregationButton_Click);
            // 
            // ElementsGroupBox
            // 
            this.ElementsGroupBox.Controls.Add(this.ElementsListBox);
            this.ElementsGroupBox.Location = new System.Drawing.Point(253, 12);
            this.ElementsGroupBox.Name = "ElementsGroupBox";
            this.ElementsGroupBox.Size = new System.Drawing.Size(235, 187);
            this.ElementsGroupBox.TabIndex = 13;
            this.ElementsGroupBox.TabStop = false;
            this.ElementsGroupBox.Text = "Elements";
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.Location = new System.Drawing.Point(640, 205);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(89, 32);
            this.CancelButton.TabIndex = 14;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // CompressionElementsGroupBox
            // 
            this.CompressionElementsGroupBox.Controls.Add(this.CompressionElementsListBox);
            this.CompressionElementsGroupBox.Location = new System.Drawing.Point(494, 12);
            this.CompressionElementsGroupBox.Name = "CompressionElementsGroupBox";
            this.CompressionElementsGroupBox.Size = new System.Drawing.Size(235, 187);
            this.CompressionElementsGroupBox.TabIndex = 14;
            this.CompressionElementsGroupBox.TabStop = false;
            this.CompressionElementsGroupBox.Text = "Compression elements";
            // 
            // CompressionElementsListBox
            // 
            this.CompressionElementsListBox.CheckOnClick = true;
            this.CompressionElementsListBox.FormattingEnabled = true;
            this.CompressionElementsListBox.Location = new System.Drawing.Point(6, 19);
            this.CompressionElementsListBox.Name = "CompressionElementsListBox";
            this.CompressionElementsListBox.Size = new System.Drawing.Size(223, 154);
            this.CompressionElementsListBox.TabIndex = 6;
            this.CompressionElementsListBox.SelectedIndexChanged += new System.EventHandler(this.CompressionElementsListBox_SelectedIndexChanged);
            // 
            // FeatureGroupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 249);
            this.Controls.Add(this.CompressionElementsGroupBox);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.ElementsGroupBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.OkButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(757, 288);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(516, 288);
            this.Name = "FeatureGroupForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Shown += new System.EventHandler(this.FeatureGroupForm_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ElementsGroupBox.ResumeLayout(false);
            this.CompressionElementsGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.ComboBox SourceComboBox;
        private System.Windows.Forms.ComboBox TypeComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox ElementsListBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox AggregationComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox CompressionComboBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox ElementsGroupBox;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button ClearCompressionButton;
        private System.Windows.Forms.Button ClearAggregationButton;
        private System.Windows.Forms.GroupBox CompressionElementsGroupBox;
        private System.Windows.Forms.CheckedListBox CompressionElementsListBox;
    }
}