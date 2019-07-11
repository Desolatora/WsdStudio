using System;
using System.Windows.Forms;
using WsdPreprocessingStudio.Core;
using WsdPreprocessingStudio.StatisticsPlugin.Data;

namespace WsdPreprocessingStudio.StatisticsPlugin
{
    public partial class StatisticsForm : Form
    {
        private readonly WsdProject _project;
        private readonly StatisticsConfig _config = new StatisticsConfig
        {
            PluginEnabled = true,
            HandlerExecutionPriority = -1,
            RequireTrainingSet = true,
            RequireTestSet = true
        };

        private bool _refreshingUI;

        public StatisticsForm(WsdProject project)
        {
            _project = project;

            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer, true);

            RefreshUI();
            SaveConfig();
        }

        private void RefreshUI()
        {
            _refreshingUI = true;

            try
            {
                EnablePlugin_Checkbox.Checked = _config.PluginEnabled;
                ExecutionPriority_UpDown.Value = _config.HandlerExecutionPriority;
                Statistics_GroupBox.Enabled = _config.PluginEnabled;
                RequireTrainingSet_CheckBox.Checked = _config.RequireTrainingSet;
                RequireTestSet_CheckBox.Checked = _config.RequireTestSet;
                MinimumExamples_UpDown.Value = _config.MinimumTrainingValidationExamples;
                AbortGeneration_CheckBox.Checked = _config.AbortGenerationAfterStatisticsAreComputed;
            }
            finally
            {
                _refreshingUI = false;
            }
        }

        private void SaveConfig()
        {
            _project.PluginData.SetData<StatisticsPlugin, StatisticsConfig>(string.Empty, _config);
        }

        private void EnablePlugin_Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (_refreshingUI)
                return;

            _config.PluginEnabled = EnablePlugin_Checkbox.Checked;

            RefreshUI();
            SaveConfig();
        }

        private void ExecutionPriority_UpDown_ValueChanged(object sender, EventArgs e)
        {
            if (_refreshingUI)
                return;

            _config.HandlerExecutionPriority = (int) ExecutionPriority_UpDown.Value;

            RefreshUI();
            SaveConfig();
        }

        private void RequireTrainingSet_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_refreshingUI)
                return;

            _config.RequireTrainingSet = RequireTrainingSet_CheckBox.Checked;

            RefreshUI();
            SaveConfig();
        }
        
        private void RequireTestSet_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_refreshingUI)
                return;

            _config.RequireTestSet = RequireTestSet_CheckBox.Checked;

            RefreshUI();
            SaveConfig();
        }

        private void MinimumExamples_UpDown_ValueChanged(object sender, EventArgs e)
        {
            if (_refreshingUI)
                return;

            _config.MinimumTrainingValidationExamples = (int) MinimumExamples_UpDown.Value;

            RefreshUI();
            SaveConfig();
        }

        private void AbortGeneration_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_refreshingUI)
                return;

            _config.AbortGenerationAfterStatisticsAreComputed = AbortGeneration_CheckBox.Checked;

            RefreshUI();
            SaveConfig();
        }
    }
}
