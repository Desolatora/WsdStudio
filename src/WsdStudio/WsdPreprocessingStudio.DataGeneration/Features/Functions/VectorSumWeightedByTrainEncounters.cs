using System.Collections.Generic;
using System.Linq;
using WsdPreprocessingStudio.Core.Data;
using WsdPreprocessingStudio.DataGeneration.Data;

namespace WsdPreprocessingStudio.DataGeneration.Features.Functions
{
    public class VectorSumWeightedByTrainEncounters : IAggregationFunction
    {
        public string DisplayName => "VectorSum = weight[x]*x + weight[y]*y";
        public IList<FeatureValueType> SupportedFeatureTypes => new[] {FeatureValueType.Numeric};

        public IList<FeatureValue> Aggregate(
            IList<EncounterValues> values, RawRecord record, 
            FeatureGroup featureGroup, FeatureSelectionContext context)
        {
            var wordAnalyses = new WordAnalysis[values.Count];
            var hasNullWordAnalysis = false;
            
            for (var i = 0; i < values.Count; i++)
            {
                if (values[i].Encounter == RawWordEncounter.EmptyWordEncounter)
                    return FeatureValue.NewArray(0, values[i].Values.Count);

                wordAnalyses[i] = context.Project.DataAnalysis.GetByName(values[i].Encounter.Word);

                if (wordAnalyses[i] == null)
                {
                    hasNullWordAnalysis = true;
                }
            }

            var weights = new double[wordAnalyses.Length];

            if (hasNullWordAnalysis)
            {
                var weight = 1d / wordAnalyses.Length;

                for (var i = 0; i < weights.Length; i++)
                {
                    weights[i] = weight;
                }
            }
            else
            {
                var trainEncounters = new double[wordAnalyses.Length];
                var trainEncountersSum = 0;

                for (var i = 0; i < weights.Length; i++)
                {
                    var currentTrainingEncounters = wordAnalyses[i].TrainEncounters.Values
                        .Sum(x => x.Encounters);

                    trainEncounters[i] = currentTrainingEncounters;
                    trainEncountersSum += currentTrainingEncounters;
                }

                if (trainEncountersSum == 0)
                    return FeatureValue.NewArray(0, values[0].Values.Count);

                for (var i = 0; i < weights.Length; i++)
                {
                    weights[i] = trainEncounters[i] / trainEncountersSum;
                }
            }

            var result = new float[values[0].Values.Count];

            for (var vectorIndex = 0; vectorIndex < result.Length; vectorIndex++)
            for (var encounterIndex = 0; encounterIndex < values.Count; encounterIndex++)
            {
                result[vectorIndex] += (float) (weights[encounterIndex] *
                                                values[encounterIndex].Values[vectorIndex].NumericValue);
            }

            return FeatureValue.NewArray(result);
        }
    }
}
