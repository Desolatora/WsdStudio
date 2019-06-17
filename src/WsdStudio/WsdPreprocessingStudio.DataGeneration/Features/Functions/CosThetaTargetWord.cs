using System;
using System.Collections.Generic;
using System.Linq;
using WsdPreprocessingStudio.DataGeneration.Data;

namespace WsdPreprocessingStudio.DataGeneration.Features.Functions
{
    public class CosThetaTargetWord : ICompressionFunction
    {
        public string DisplayName => "CosTheta(x, _TargetWord_)";
        public IList<FeatureValueType> SupportedFeatureTypes => new[] {FeatureValueType.Numeric};
        public bool RequiresCompressionElements => true;

        public FeatureValue Compress(
            IList<FeatureValue> values, RawRecord record,
            FeatureGroup featureGroup, FeatureSelectionContext context)
        {
            var vectorX = values;
            var vectorY = new List<FeatureValue>(values.Count);

            for (var i = 0; i < featureGroup.CompressionElements.Count; i++)
            {
                var featureValues = featureGroup.CompressionElements[i].GetValues(record, context);

                for (var j = 0; j < featureValues.Count; j++)
                {
                    vectorY.Add(featureValues[j]);
                }
            }

            if (vectorX.Count < vectorY.Count)
            {
                vectorX = vectorX.ToList();

                var missingCount = vectorY.Count - vectorX.Count;

                for (var i = 0; i < missingCount; i++)
                {
                    vectorX.Add(new FeatureValue(0));
                }
            }
            else if (vectorY.Count < vectorX.Count)
            {
                var missingCount = vectorX.Count - vectorY.Count;

                for (var i = 0; i < missingCount; i++)
                {
                    vectorY.Add(new FeatureValue(0));
                }
            }

            var scalarProduct = 0.0d;
            var vectorXNorm = 0.0d;
            var vectorYNorm = 0.0d;

            for (var i = 0; i < vectorX.Count; i++)
            {
                scalarProduct += vectorX[i].NumericValue * vectorY[i].NumericValue;
                vectorXNorm += Math.Pow(vectorX[i].NumericValue, 2);
                vectorYNorm += Math.Pow(vectorY[i].NumericValue, 2);
            }

            if (vectorXNorm == 0 || vectorYNorm == 0)
                return new FeatureValue(0);

            vectorXNorm = Math.Sqrt(vectorXNorm);
            vectorYNorm = Math.Sqrt(vectorYNorm);

            return new FeatureValue((float) (scalarProduct / (vectorXNorm * vectorYNorm)));
        }
    }
}
