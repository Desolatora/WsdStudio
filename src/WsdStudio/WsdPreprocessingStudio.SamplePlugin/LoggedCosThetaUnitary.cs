using System;
using System.Collections.Generic;
using WsdPreprocessingStudio.DataGeneration.Data;
using WsdPreprocessingStudio.DataGeneration.Features;
using WsdPreprocessingStudio.DataGeneration.Plugins;

namespace WsdPreprocessingStudio.SamplePlugin
{
    public class LoggedCosThetaUnitary : IPluginCompressionFunction
    {
        public string DisplayName => "LoggedCosTheta(x, _Unitary_)";
        public IList<FeatureValueType> SupportedFeatureTypes => new[] {FeatureValueType.Numeric};
        public bool RequiresCompressionElements => false;

        public FeatureValue Compress(
            IList<FeatureValue> values, RawRecord record,
            FeatureGroup featureGroup, FeatureSelectionContext context)
        {
            context.Project.PluginData
                .GetData<LoggingPlugin, UsageStatistics>(string.Empty)
                .CosThetaUnitaryCounter++;

            var scalarProduct = 0.0d;
            var vectorXNorm = 0.0d;
            var vectorYNorm = 0.0d;

            for (var i = 0; i < values.Count; i++)
            {
                scalarProduct += values[i].NumericValue;
                vectorXNorm += Math.Pow(values[i].NumericValue, 2);
                vectorYNorm += 1;
            }

            if (vectorXNorm == 0)
                return new FeatureValue(0);

            vectorXNorm = Math.Sqrt(vectorXNorm);
            vectorYNorm = Math.Sqrt(vectorYNorm);

            return new FeatureValue((float) (scalarProduct / (vectorXNorm * vectorYNorm)));
        }
    }
}
