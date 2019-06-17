namespace WsdPreprocessingStudio.Forms
{
    partial class ProjectInfoForm
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
            this.ProjectInfo_ListView = new System.Windows.Forms.ListView();
            this.ProjectInfo_ListView_ParameterHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ProjectInfo_ListView_ValueHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // ProjectInfo_ListView
            // 
            this.ProjectInfo_ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ProjectInfo_ListView_ParameterHeader,
            this.ProjectInfo_ListView_ValueHeader});
            this.ProjectInfo_ListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProjectInfo_ListView.FullRowSelect = true;
            this.ProjectInfo_ListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ProjectInfo_ListView.Location = new System.Drawing.Point(0, 0);
            this.ProjectInfo_ListView.MultiSelect = false;
            this.ProjectInfo_ListView.Name = "ProjectInfo_ListView";
            this.ProjectInfo_ListView.Size = new System.Drawing.Size(684, 461);
            this.ProjectInfo_ListView.TabIndex = 0;
            this.ProjectInfo_ListView.UseCompatibleStateImageBehavior = false;
            this.ProjectInfo_ListView.View = System.Windows.Forms.View.Details;
            // 
            // ProjectInfo_ListView_ParameterHeader
            // 
            this.ProjectInfo_ListView_ParameterHeader.Text = "Parameter";
            this.ProjectInfo_ListView_ParameterHeader.Width = 300;
            // 
            // ProjectInfo_ListView_ValueHeader
            // 
            this.ProjectInfo_ListView_ValueHeader.Text = "Value";
            this.ProjectInfo_ListView_ValueHeader.Width = 300;
            // 
            // ProjectInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(684, 461);
            this.Controls.Add(this.ProjectInfo_ListView);
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "ProjectInfoForm";
            this.Text = "Project info";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView ProjectInfo_ListView;
        private System.Windows.Forms.ColumnHeader ProjectInfo_ListView_ParameterHeader;
        private System.Windows.Forms.ColumnHeader ProjectInfo_ListView_ValueHeader;
    }
}