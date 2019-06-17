using System.Windows.Forms;
using WsdPreprocessingStudio.Core.Data.Statistics;
using WsdPreprocessingStudio.Core.UI;

namespace WsdPreprocessingStudio.Binders
{
    public class DictionaryStatisticsBinder
    {
        public static void BindToListView(DictionaryStatistics data, ListView control)
        {
            control
                .AddGroup("Dictionary statistics", group =>
                {
                    group
                        .AddItem("Word count", data.WordCount.ToString())
                        .AddItem("Monosemantic word count", data.MonosemanticWordCount.ToString())
                        .AddItem("Polysemantic word count", data.PolysemanticWordCount.ToString())
                        .AddItem("Max meanings per word", data.MaxMeaningsPerWord.ToString())
                        .AddItem("Average meanings per word", data.AverageMeaningsPerWord.ToString("F2"))
                        .AddItem("Unique meanings count", data.UniqueMeaningsCount.ToString());
                });
        }
    }
}
