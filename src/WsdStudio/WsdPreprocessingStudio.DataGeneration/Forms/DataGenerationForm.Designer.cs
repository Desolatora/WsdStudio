

namespace WsdPreprocessingStudio.DataGeneration.Forms
{
    partial class DataGenerationForm
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
            this.GenerateData_GenerateDataButton = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.PosList_CheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.GenerateData_OrderMeaningsStrategyComboBox = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.GenerateData_OrderMeaningsComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.GenerateData_FeatureGroupsListView = new System.Windows.Forms.ListView();
            this.GenerateData_SourceHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GenerateData_ElementsHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GenerateData_TypeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GenerateData_CompressionHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GenerateData_CompressionElementsHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GenerateData_AggregationHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GenerateData_RemoveButton = new System.Windows.Forms.Button();
            this.GenerateData_AddButton = new System.Windows.Forms.Button();
            this.GenerateData_DownButton = new System.Windows.Forms.Button();
            this.GenerateData_UpButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.GenerateData_OverlapCheckBox = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.GenerateData_LeftContextNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.GenerateData_RightContextNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.GenerateData_OutputFormatComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.GenerateData_SavingStrategyComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.GenerateData_ExtractValidationSetCheckBox = new System.Windows.Forms.CheckBox();
            this.GenerateData_ValidationSetPercentageLabel = new System.Windows.Forms.Label();
            this.GenerateData_ValidationSetPercentageTrackBar = new System.Windows.Forms.TrackBar();
            this.GenerateData_ShuffleDataCheckbox = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.GenerateData_SaveGenerationInfoButton = new System.Windows.Forms.Button();
            this.GenerateData_LoadGenerationInfoButton = new System.Windows.Forms.Button();
            this.groupBox7.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GenerateData_LeftContextNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GenerateData_RightContextNumericUpDown)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GenerateData_ValidationSetPercentageTrackBar)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GenerateData_GenerateDataButton
            // 
            this.GenerateData_GenerateDataButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.GenerateData_GenerateDataButton.Location = new System.Drawing.Point(0, 420);
            this.GenerateData_GenerateDataButton.Name = "GenerateData_GenerateDataButton";
            this.GenerateData_GenerateDataButton.Size = new System.Drawing.Size(684, 41);
            this.GenerateData_GenerateDataButton.TabIndex = 25;
            this.GenerateData_GenerateDataButton.Text = "Generate data";
            this.GenerateData_GenerateDataButton.UseVisualStyleBackColor = true;
            this.GenerateData_GenerateDataButton.Click += new System.EventHandler(this.GenerateData_GenerateDataButton_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.PosList_CheckedListBox);
            this.groupBox7.Controls.Add(this.label2);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox7.Location = new System.Drawing.Point(3, 3);
            this.groupBox7.Name = "groupBox7";
            this.tableLayoutPanel4.SetRowSpan(this.groupBox7, 2);
            this.groupBox7.Size = new System.Drawing.Size(222, 204);
            this.groupBox7.TabIndex = 0;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Filters";
            // 
            // PosList_CheckedListBox
            // 
            this.PosList_CheckedListBox.CheckOnClick = true;
            this.PosList_CheckedListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PosList_CheckedListBox.FormattingEnabled = true;
            this.PosList_CheckedListBox.Location = new System.Drawing.Point(3, 29);
            this.PosList_CheckedListBox.Name = "PosList_CheckedListBox";
            this.PosList_CheckedListBox.Size = new System.Drawing.Size(216, 172);
            this.PosList_CheckedListBox.TabIndex = 0;
            this.PosList_CheckedListBox.SelectedIndexChanged += new System.EventHandler(this.PosList_CheckedListBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(3, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(185, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Include words with the following POS:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.GenerateData_OrderMeaningsStrategyComboBox);
            this.groupBox5.Controls.Add(this.label19);
            this.groupBox5.Controls.Add(this.GenerateData_OrderMeaningsComboBox);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(231, 108);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(222, 99);
            this.groupBox5.TabIndex = 41;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Dictionary";
            // 
            // GenerateData_OrderMeaningsStrategyComboBox
            // 
            this.GenerateData_OrderMeaningsStrategyComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GenerateData_OrderMeaningsStrategyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GenerateData_OrderMeaningsStrategyComboBox.FormattingEnabled = true;
            this.GenerateData_OrderMeaningsStrategyComboBox.Location = new System.Drawing.Point(9, 63);
            this.GenerateData_OrderMeaningsStrategyComboBox.Name = "GenerateData_OrderMeaningsStrategyComboBox";
            this.GenerateData_OrderMeaningsStrategyComboBox.Size = new System.Drawing.Size(207, 21);
            this.GenerateData_OrderMeaningsStrategyComboBox.TabIndex = 48;
            this.GenerateData_OrderMeaningsStrategyComboBox.SelectedIndexChanged += new System.EventHandler(this.GenerateData_OrderMeaningsStrategyComboBox_SelectedIndexChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 20);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(84, 13);
            this.label19.TabIndex = 47;
            this.label19.Text = "Order meanings:";
            // 
            // GenerateData_OrderMeaningsComboBox
            // 
            this.GenerateData_OrderMeaningsComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GenerateData_OrderMeaningsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GenerateData_OrderMeaningsComboBox.FormattingEnabled = true;
            this.GenerateData_OrderMeaningsComboBox.Location = new System.Drawing.Point(9, 36);
            this.GenerateData_OrderMeaningsComboBox.Name = "GenerateData_OrderMeaningsComboBox";
            this.GenerateData_OrderMeaningsComboBox.Size = new System.Drawing.Size(207, 21);
            this.GenerateData_OrderMeaningsComboBox.TabIndex = 46;
            this.GenerateData_OrderMeaningsComboBox.SelectedIndexChanged += new System.EventHandler(this.GenerateData_OrderMeaningsComboBox_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.SetColumnSpan(this.groupBox2, 2);
            this.groupBox2.Controls.Add(this.tableLayoutPanel3);
            this.groupBox2.Location = new System.Drawing.Point(3, 213);
            this.groupBox2.Name = "groupBox2";
            this.tableLayoutPanel4.SetRowSpan(this.groupBox2, 2);
            this.groupBox2.Size = new System.Drawing.Size(450, 204);
            this.groupBox2.TabIndex = 39;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Features";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Controls.Add(this.GenerateData_FeatureGroupsListView, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.GenerateData_RemoveButton, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.GenerateData_AddButton, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.GenerateData_DownButton, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.GenerateData_UpButton, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(444, 185);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // GenerateData_FeatureGroupsListView
            // 
            this.GenerateData_FeatureGroupsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GenerateData_FeatureGroupsListView.BackColor = System.Drawing.SystemColors.Window;
            this.GenerateData_FeatureGroupsListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GenerateData_FeatureGroupsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.GenerateData_SourceHeader,
            this.GenerateData_ElementsHeader,
            this.GenerateData_TypeHeader,
            this.GenerateData_CompressionHeader,
            this.GenerateData_CompressionElementsHeader,
            this.GenerateData_AggregationHeader});
            this.tableLayoutPanel3.SetColumnSpan(this.GenerateData_FeatureGroupsListView, 4);
            this.GenerateData_FeatureGroupsListView.FullRowSelect = true;
            this.GenerateData_FeatureGroupsListView.GridLines = true;
            this.GenerateData_FeatureGroupsListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.GenerateData_FeatureGroupsListView.HideSelection = false;
            this.GenerateData_FeatureGroupsListView.Location = new System.Drawing.Point(3, 3);
            this.GenerateData_FeatureGroupsListView.MultiSelect = false;
            this.GenerateData_FeatureGroupsListView.Name = "GenerateData_FeatureGroupsListView";
            this.GenerateData_FeatureGroupsListView.Size = new System.Drawing.Size(438, 149);
            this.GenerateData_FeatureGroupsListView.TabIndex = 26;
            this.GenerateData_FeatureGroupsListView.UseCompatibleStateImageBehavior = false;
            this.GenerateData_FeatureGroupsListView.View = System.Windows.Forms.View.Details;
            this.GenerateData_FeatureGroupsListView.SelectedIndexChanged += new System.EventHandler(this.GenerateData_FeatureGroupsListView_SelectedIndexChanged);
            this.GenerateData_FeatureGroupsListView.DoubleClick += new System.EventHandler(this.GenerateData_FeatureGroupsListView_DoubleClick);
            // 
            // GenerateData_SourceHeader
            // 
            this.GenerateData_SourceHeader.Text = "Source";
            this.GenerateData_SourceHeader.Width = 100;
            // 
            // GenerateData_ElementsHeader
            // 
            this.GenerateData_ElementsHeader.Text = "Elements";
            this.GenerateData_ElementsHeader.Width = 150;
            // 
            // GenerateData_TypeHeader
            // 
            this.GenerateData_TypeHeader.Text = "Type";
            this.GenerateData_TypeHeader.Width = 70;
            // 
            // GenerateData_CompressionHeader
            // 
            this.GenerateData_CompressionHeader.Text = "Compression";
            this.GenerateData_CompressionHeader.Width = 170;
            // 
            // GenerateData_CompressionElementsHeader
            // 
            this.GenerateData_CompressionElementsHeader.Text = "Compression elements";
            this.GenerateData_CompressionElementsHeader.Width = 150;
            // 
            // GenerateData_AggregationHeader
            // 
            this.GenerateData_AggregationHeader.Text = "Aggregation";
            this.GenerateData_AggregationHeader.Width = 200;
            // 
            // GenerateData_RemoveButton
            // 
            this.GenerateData_RemoveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GenerateData_RemoveButton.Location = new System.Drawing.Point(336, 158);
            this.GenerateData_RemoveButton.Name = "GenerateData_RemoveButton";
            this.GenerateData_RemoveButton.Size = new System.Drawing.Size(105, 24);
            this.GenerateData_RemoveButton.TabIndex = 38;
            this.GenerateData_RemoveButton.Text = "Remove";
            this.GenerateData_RemoveButton.UseVisualStyleBackColor = true;
            this.GenerateData_RemoveButton.Click += new System.EventHandler(this.GenerateData_RemoveButton_Click);
            // 
            // GenerateData_AddButton
            // 
            this.GenerateData_AddButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GenerateData_AddButton.Location = new System.Drawing.Point(225, 158);
            this.GenerateData_AddButton.Name = "GenerateData_AddButton";
            this.GenerateData_AddButton.Size = new System.Drawing.Size(105, 24);
            this.GenerateData_AddButton.TabIndex = 37;
            this.GenerateData_AddButton.Text = "Add";
            this.GenerateData_AddButton.UseVisualStyleBackColor = true;
            this.GenerateData_AddButton.Click += new System.EventHandler(this.GenerateData_AddButton_Click);
            // 
            // GenerateData_DownButton
            // 
            this.GenerateData_DownButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GenerateData_DownButton.Location = new System.Drawing.Point(114, 158);
            this.GenerateData_DownButton.Name = "GenerateData_DownButton";
            this.GenerateData_DownButton.Size = new System.Drawing.Size(105, 24);
            this.GenerateData_DownButton.TabIndex = 36;
            this.GenerateData_DownButton.Text = "Down";
            this.GenerateData_DownButton.UseVisualStyleBackColor = true;
            this.GenerateData_DownButton.Click += new System.EventHandler(this.GenerateData_DownButton_Click);
            // 
            // GenerateData_UpButton
            // 
            this.GenerateData_UpButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GenerateData_UpButton.Location = new System.Drawing.Point(3, 158);
            this.GenerateData_UpButton.Name = "GenerateData_UpButton";
            this.GenerateData_UpButton.Size = new System.Drawing.Size(105, 24);
            this.GenerateData_UpButton.TabIndex = 35;
            this.GenerateData_UpButton.Text = "Up";
            this.GenerateData_UpButton.UseVisualStyleBackColor = true;
            this.GenerateData_UpButton.Click += new System.EventHandler(this.GenerateData_UpButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.GenerateData_OverlapCheckBox);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.GenerateData_LeftContextNumericUpDown);
            this.groupBox3.Controls.Add(this.GenerateData_RightContextNumericUpDown);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(231, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(222, 99);
            this.groupBox3.TabIndex = 40;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Context";
            // 
            // GenerateData_OverlapCheckBox
            // 
            this.GenerateData_OverlapCheckBox.AutoSize = true;
            this.GenerateData_OverlapCheckBox.Location = new System.Drawing.Point(85, 71);
            this.GenerateData_OverlapCheckBox.Name = "GenerateData_OverlapCheckBox";
            this.GenerateData_OverlapCheckBox.Size = new System.Drawing.Size(63, 17);
            this.GenerateData_OverlapCheckBox.TabIndex = 28;
            this.GenerateData_OverlapCheckBox.Text = "Overlap";
            this.GenerateData_OverlapCheckBox.UseVisualStyleBackColor = true;
            this.GenerateData_OverlapCheckBox.CheckedChanged += new System.EventHandler(this.GenerateData_OverlapCheckBox_CheckedChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 21);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 13);
            this.label11.TabIndex = 26;
            this.label11.Text = "Left context:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 47);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(73, 13);
            this.label14.TabIndex = 27;
            this.label14.Text = "Right context:";
            // 
            // GenerateData_LeftContextNumericUpDown
            // 
            this.GenerateData_LeftContextNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GenerateData_LeftContextNumericUpDown.Location = new System.Drawing.Point(85, 19);
            this.GenerateData_LeftContextNumericUpDown.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.GenerateData_LeftContextNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.GenerateData_LeftContextNumericUpDown.Name = "GenerateData_LeftContextNumericUpDown";
            this.GenerateData_LeftContextNumericUpDown.Size = new System.Drawing.Size(131, 20);
            this.GenerateData_LeftContextNumericUpDown.TabIndex = 29;
            this.GenerateData_LeftContextNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.GenerateData_LeftContextNumericUpDown.ValueChanged += new System.EventHandler(this.GenerateData_LeftContextNumericUpDown_ValueChanged);
            // 
            // GenerateData_RightContextNumericUpDown
            // 
            this.GenerateData_RightContextNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GenerateData_RightContextNumericUpDown.Location = new System.Drawing.Point(85, 45);
            this.GenerateData_RightContextNumericUpDown.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.GenerateData_RightContextNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.GenerateData_RightContextNumericUpDown.Name = "GenerateData_RightContextNumericUpDown";
            this.GenerateData_RightContextNumericUpDown.Size = new System.Drawing.Size(131, 20);
            this.GenerateData_RightContextNumericUpDown.TabIndex = 30;
            this.GenerateData_RightContextNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.GenerateData_RightContextNumericUpDown.ValueChanged += new System.EventHandler(this.GenerateData_RightContextNumericUpDown_ValueChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label7);
            this.groupBox6.Controls.Add(this.GenerateData_OutputFormatComboBox);
            this.groupBox6.Controls.Add(this.label6);
            this.groupBox6.Controls.Add(this.GenerateData_SavingStrategyComboBox);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(459, 213);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(222, 99);
            this.groupBox6.TabIndex = 45;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Output";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 34;
            this.label7.Text = "Output format:";
            // 
            // GenerateData_OutputFormatComboBox
            // 
            this.GenerateData_OutputFormatComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GenerateData_OutputFormatComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GenerateData_OutputFormatComboBox.FormattingEnabled = true;
            this.GenerateData_OutputFormatComboBox.Location = new System.Drawing.Point(9, 70);
            this.GenerateData_OutputFormatComboBox.Name = "GenerateData_OutputFormatComboBox";
            this.GenerateData_OutputFormatComboBox.Size = new System.Drawing.Size(207, 21);
            this.GenerateData_OutputFormatComboBox.TabIndex = 33;
            this.GenerateData_OutputFormatComboBox.SelectedIndexChanged += new System.EventHandler(this.GenerateData_OutputFormatComboBox_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 32;
            this.label6.Text = "Saving strategy:";
            // 
            // GenerateData_SavingStrategyComboBox
            // 
            this.GenerateData_SavingStrategyComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GenerateData_SavingStrategyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GenerateData_SavingStrategyComboBox.FormattingEnabled = true;
            this.GenerateData_SavingStrategyComboBox.Location = new System.Drawing.Point(9, 30);
            this.GenerateData_SavingStrategyComboBox.Name = "GenerateData_SavingStrategyComboBox";
            this.GenerateData_SavingStrategyComboBox.Size = new System.Drawing.Size(207, 21);
            this.GenerateData_SavingStrategyComboBox.TabIndex = 31;
            this.GenerateData_SavingStrategyComboBox.SelectedIndexChanged += new System.EventHandler(this.GenerateData_SavingStrategyComboBox_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.GenerateData_ExtractValidationSetCheckBox);
            this.groupBox4.Controls.Add(this.GenerateData_ValidationSetPercentageLabel);
            this.groupBox4.Controls.Add(this.GenerateData_ValidationSetPercentageTrackBar);
            this.groupBox4.Controls.Add(this.GenerateData_ShuffleDataCheckbox);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(459, 3);
            this.groupBox4.Name = "groupBox4";
            this.tableLayoutPanel4.SetRowSpan(this.groupBox4, 2);
            this.groupBox4.Size = new System.Drawing.Size(222, 204);
            this.groupBox4.TabIndex = 44;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Dataset";
            // 
            // GenerateData_ExtractValidationSetCheckBox
            // 
            this.GenerateData_ExtractValidationSetCheckBox.AutoSize = true;
            this.GenerateData_ExtractValidationSetCheckBox.Location = new System.Drawing.Point(6, 42);
            this.GenerateData_ExtractValidationSetCheckBox.Name = "GenerateData_ExtractValidationSetCheckBox";
            this.GenerateData_ExtractValidationSetCheckBox.Size = new System.Drawing.Size(124, 17);
            this.GenerateData_ExtractValidationSetCheckBox.TabIndex = 40;
            this.GenerateData_ExtractValidationSetCheckBox.Text = "Extract validation set";
            this.GenerateData_ExtractValidationSetCheckBox.UseVisualStyleBackColor = true;
            this.GenerateData_ExtractValidationSetCheckBox.CheckedChanged += new System.EventHandler(this.GenerateData_ExtractValidationSetCheckBox_CheckedChanged);
            // 
            // GenerateData_ValidationSetPercentageLabel
            // 
            this.GenerateData_ValidationSetPercentageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GenerateData_ValidationSetPercentageLabel.AutoSize = true;
            this.GenerateData_ValidationSetPercentageLabel.Location = new System.Drawing.Point(189, 68);
            this.GenerateData_ValidationSetPercentageLabel.Name = "GenerateData_ValidationSetPercentageLabel";
            this.GenerateData_ValidationSetPercentageLabel.Size = new System.Drawing.Size(27, 13);
            this.GenerateData_ValidationSetPercentageLabel.TabIndex = 42;
            this.GenerateData_ValidationSetPercentageLabel.Text = "50%";
            // 
            // GenerateData_ValidationSetPercentageTrackBar
            // 
            this.GenerateData_ValidationSetPercentageTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GenerateData_ValidationSetPercentageTrackBar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.GenerateData_ValidationSetPercentageTrackBar.Location = new System.Drawing.Point(6, 65);
            this.GenerateData_ValidationSetPercentageTrackBar.Maximum = 50;
            this.GenerateData_ValidationSetPercentageTrackBar.Minimum = 5;
            this.GenerateData_ValidationSetPercentageTrackBar.Name = "GenerateData_ValidationSetPercentageTrackBar";
            this.GenerateData_ValidationSetPercentageTrackBar.Size = new System.Drawing.Size(177, 45);
            this.GenerateData_ValidationSetPercentageTrackBar.TabIndex = 41;
            this.GenerateData_ValidationSetPercentageTrackBar.TickFrequency = 5;
            this.GenerateData_ValidationSetPercentageTrackBar.Value = 5;
            this.GenerateData_ValidationSetPercentageTrackBar.Scroll += new System.EventHandler(this.GenerateData_ValidationSetPercentageTrackBar_Scroll);
            // 
            // GenerateData_ShuffleDataCheckbox
            // 
            this.GenerateData_ShuffleDataCheckbox.AutoSize = true;
            this.GenerateData_ShuffleDataCheckbox.Location = new System.Drawing.Point(6, 19);
            this.GenerateData_ShuffleDataCheckbox.Name = "GenerateData_ShuffleDataCheckbox";
            this.GenerateData_ShuffleDataCheckbox.Size = new System.Drawing.Size(83, 17);
            this.GenerateData_ShuffleDataCheckbox.TabIndex = 43;
            this.GenerateData_ShuffleDataCheckbox.Text = "Shuffle data";
            this.GenerateData_ShuffleDataCheckbox.UseVisualStyleBackColor = true;
            this.GenerateData_ShuffleDataCheckbox.CheckedChanged += new System.EventHandler(this.GenerateData_ShuffleDataCheckbox_CheckedChanged);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.Controls.Add(this.groupBox6, 2, 2);
            this.tableLayoutPanel4.Controls.Add(this.groupBox5, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.groupBox4, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.groupBox7, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.groupBox3, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.groupBox2, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.groupBox1, 2, 3);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 4;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(684, 420);
            this.tableLayoutPanel4.TabIndex = 49;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(459, 318);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(222, 99);
            this.groupBox1.TabIndex = 46;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Info";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.GenerateData_SaveGenerationInfoButton, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.GenerateData_LoadGenerationInfoButton, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(216, 80);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // GenerateData_SaveGenerationInfoButton
            // 
            this.GenerateData_SaveGenerationInfoButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GenerateData_SaveGenerationInfoButton.Location = new System.Drawing.Point(3, 43);
            this.GenerateData_SaveGenerationInfoButton.Name = "GenerateData_SaveGenerationInfoButton";
            this.GenerateData_SaveGenerationInfoButton.Size = new System.Drawing.Size(212, 34);
            this.GenerateData_SaveGenerationInfoButton.TabIndex = 1;
            this.GenerateData_SaveGenerationInfoButton.Text = "Save generation info";
            this.GenerateData_SaveGenerationInfoButton.UseVisualStyleBackColor = true;
            this.GenerateData_SaveGenerationInfoButton.Click += new System.EventHandler(this.GenerateData_SaveGenerationInfoButton_Click);
            // 
            // GenerateData_LoadGenerationInfoButton
            // 
            this.GenerateData_LoadGenerationInfoButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GenerateData_LoadGenerationInfoButton.Location = new System.Drawing.Point(3, 3);
            this.GenerateData_LoadGenerationInfoButton.Name = "GenerateData_LoadGenerationInfoButton";
            this.GenerateData_LoadGenerationInfoButton.Size = new System.Drawing.Size(212, 34);
            this.GenerateData_LoadGenerationInfoButton.TabIndex = 0;
            this.GenerateData_LoadGenerationInfoButton.Text = "Load generation info";
            this.GenerateData_LoadGenerationInfoButton.UseVisualStyleBackColor = true;
            this.GenerateData_LoadGenerationInfoButton.Click += new System.EventHandler(this.GenerateData_LoadGenerationInfoButton_Click);
            // 
            // DataGenerationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(684, 461);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Controls.Add(this.GenerateData_GenerateDataButton);
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "DataGenerationForm";
            this.Text = "Data generation";
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GenerateData_LeftContextNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GenerateData_RightContextNumericUpDown)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GenerateData_ValidationSetPercentageTrackBar)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button GenerateData_GenerateDataButton;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox GenerateData_OrderMeaningsComboBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.ListView GenerateData_FeatureGroupsListView;
        private System.Windows.Forms.ColumnHeader GenerateData_SourceHeader;
        private System.Windows.Forms.ColumnHeader GenerateData_ElementsHeader;
        private System.Windows.Forms.Button GenerateData_RemoveButton;
        private System.Windows.Forms.Button GenerateData_AddButton;
        private System.Windows.Forms.Button GenerateData_DownButton;
        private System.Windows.Forms.Button GenerateData_UpButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox GenerateData_OverlapCheckBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown GenerateData_LeftContextNumericUpDown;
        private System.Windows.Forms.NumericUpDown GenerateData_RightContextNumericUpDown;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox GenerateData_OutputFormatComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox GenerateData_SavingStrategyComboBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox GenerateData_ExtractValidationSetCheckBox;
        private System.Windows.Forms.Label GenerateData_ValidationSetPercentageLabel;
        private System.Windows.Forms.TrackBar GenerateData_ValidationSetPercentageTrackBar;
        private System.Windows.Forms.CheckBox GenerateData_ShuffleDataCheckbox;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.CheckedListBox PosList_CheckedListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.ColumnHeader GenerateData_TypeHeader;
        private System.Windows.Forms.ColumnHeader GenerateData_CompressionHeader;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button GenerateData_SaveGenerationInfoButton;
        private System.Windows.Forms.Button GenerateData_LoadGenerationInfoButton;
        private System.Windows.Forms.ColumnHeader GenerateData_CompressionElementsHeader;
        private System.Windows.Forms.ColumnHeader GenerateData_AggregationHeader;
        private System.Windows.Forms.ComboBox GenerateData_OrderMeaningsStrategyComboBox;
    }
}