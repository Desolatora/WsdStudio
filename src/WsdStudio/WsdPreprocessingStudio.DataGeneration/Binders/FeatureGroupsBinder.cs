using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WsdPreprocessingStudio.Core.UI;
using WsdPreprocessingStudio.DataGeneration.Features;

namespace WsdPreprocessingStudio.DataGeneration.Binders
{
    public class FeatureGroupsBinder
    {
        public static void BindToListView(IList<FeatureGroup> featureGroups, ListView control)
        {
            using (control.UpdateScope())
            {
                control.Items.Clear();

                foreach (var featureGroup in featureGroups)
                {
                    control.AddItem(
                        featureGroup.Source.DisplayName,
                        string.Join(", ", featureGroup.Elements.Select(x => x.DisplayName)),
                        featureGroup.ValueType.ToString(),
                        featureGroup.CompressionFunction?.DisplayName ?? string.Empty,
                        featureGroup.CompressionElements != null
                            ? string.Join(", ", featureGroup.CompressionElements.Select(x => x.DisplayName))
                            : string.Empty,
                        featureGroup.AggregationFunction?.DisplayName ?? string.Empty);
                }
            }
        }
    }
}
