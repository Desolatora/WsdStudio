using System.Windows.Forms;
using WsdPreprocessingStudio.Binders;
using WsdPreprocessingStudio.Core;
using WsdPreprocessingStudio.Core.UI;

namespace WsdPreprocessingStudio.Forms
{
    public partial class ProjectInfoForm : Form
    {
        private WsdProject _project;
        
        public ProjectInfoForm(WsdProject project)
        {
            _project = project;

            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer, true);

            RefreshUI();
        }

        private void RefreshUI()
        {
            using (ProjectInfo_ListView.UpdateScope())
            {
                ProjectInfo_ListView.Groups.Clear();
                ProjectInfo_ListView.Items.Clear();

                ProjectInfoBinder.BindToListView(_project.ProjectInfo, ProjectInfo_ListView);
                DictionaryStatisticsBinder.BindToListView(_project.DictionaryStatistics, ProjectInfo_ListView);
                DataStatisticsBinder.BindToListView(_project.DataStatistics, ProjectInfo_ListView);
                EmbeddingStatisticsBinder.BindToListView(
                    _project.WordEmbeddingStatistics, _project.MeaningEmbeddingStatistics, ProjectInfo_ListView);
            }
        }
    }
}
