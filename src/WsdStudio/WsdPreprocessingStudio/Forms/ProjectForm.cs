using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WsdPreprocessingStudio.Core;
using WsdPreprocessingStudio.Core.Plugins;
using WsdPreprocessingStudio.Core.Threading;
using WsdPreprocessingStudio.DataGeneration.Forms;

namespace WsdPreprocessingStudio.Forms
{
    public partial class ProjectForm : Form
    {
        private List<Form> _forms = new List<Form>();
        
        public ProjectForm(
            IPluginComponent[] pluginComponents, WsdProject project, IProgressHandleFactory progressFactory)
        {
            if (project == null)
                throw new ArgumentNullException(nameof(project));

            if (progressFactory == null)
                throw new ArgumentNullException(nameof(progressFactory));

            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer, true);

            AddForm("Project info", new ProjectInfoForm(project));
            AddForm("Data generation", new DataGenerationForm(pluginComponents, project, progressFactory));
            
            var projectForms = pluginComponents
                .Where(x => x is IPluginProjectForm)
                .Cast<IPluginProjectForm>()
                .ToArray();

            foreach (var projectForm in projectForms)
            {
                AddForm(
                    projectForm.DisplayName,
                    projectForm.CreateForm(pluginComponents, project, progressFactory));
            }

            FormsListBox.SelectedIndex = 0;
        }

        private void AddForm(string title, Form form)
        {
            form.Dock = DockStyle.Fill;
            form.FormBorderStyle = FormBorderStyle.None;
            form.MinimumSize = new Size(750, 500);
            form.Size = new Size(750, 500);
            form.TopLevel = false;

            _forms.Add(form);

            FormsListBox.Items.Add(title);
            FormPanel.Controls.Add(form);

            form.Show();
        }

        private void FormsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FormsListBox.SelectedIndex >= 0)
            {
                _forms[FormsListBox.SelectedIndex].BringToFront();

                FormPanel.Visible = true;
            }
            else
            {
                FormPanel.Visible = false;
            }
        }
    }
}
