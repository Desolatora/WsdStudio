using System;
using System.Windows.Forms;
using WsdPreprocessingStudio.Core;
using WsdPreprocessingStudio.Core.Extensions;

namespace WsdPreprocessingStudio.SamplePlugin
{
    public partial class LoggingForm : Form
    {
        public LoggingForm(WsdProject project)
        {
            InitializeComponent();

            var logger = new EventLogger();

            logger.OnMessageLogged += message =>
            {
                this.InvokeIfRequired(() =>
                {
                    Log_RichTextBox.AppendText(message + Environment.NewLine);
                });
            };

            project.PluginData.SetData<LoggingPlugin, EventLogger>(string.Empty, logger);
            project.PluginData.SetData<LoggingPlugin, UsageStatistics>(string.Empty, new UsageStatistics());
        }
    }
}
