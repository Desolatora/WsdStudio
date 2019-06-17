using System.Windows.Forms;
using WsdPreprocessingStudio.Core.Data.Statistics;
using WsdPreprocessingStudio.Core.UI;

namespace WsdPreprocessingStudio.Binders
{
    public class EmbeddingStatisticsBinder
    {
        public static void BindToListView(
            EmbeddingStatistics wordData, EmbeddingStatistics meaningsData, ListView control)
        {
            control
                .AddGroup("Word embeddings statistics", group =>
                {
                    group
                        .AddItem("Embedding count", wordData.EmbeddingCount.ToString())
                        .AddItem("Vector length", wordData.VectorLength.ToString());
                })
                .AddGroup("Meaning embeddings statistics", group =>
                {
                    group
                        .AddItem("Embedding count", meaningsData.EmbeddingCount.ToString())
                        .AddItem("Vector length", meaningsData.VectorLength.ToString());
                });
        }
    }
}
