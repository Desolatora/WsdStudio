using System.Windows.Forms;
using WsdPreprocessingStudio.Core.Data;
using WsdPreprocessingStudio.Core.UI;

namespace WsdPreprocessingStudio.Binders
{
    public class ProjectInfoBinder
    {
        public static void BindToListView(WsdProjectInfo data, ListView control)
        {
            control
                .AddGroup("Info", group =>
                {
                    group
                        .AddItem(
                            "Project name",
                            data.ProjectName)
                        .AddItem(
                            "Project version",
                            data.ProjectVersion)
                        .AddItem(
                            "Created with (application version)",
                            data.ApplicationVersion);
                });
        }
    }
}
