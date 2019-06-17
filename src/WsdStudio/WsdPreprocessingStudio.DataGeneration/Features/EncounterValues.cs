using System.Collections.Generic;
using WsdPreprocessingStudio.Core.Data;

namespace WsdPreprocessingStudio.DataGeneration.Features
{
    public class EncounterValues
    {
        public RawWordEncounter Encounter { get; }
        public IList<FeatureValue> Values { get; }

        public EncounterValues(RawWordEncounter encounter, IList<FeatureValue> values)
        {
            Encounter = encounter;
            Values = values;
        }
    }
}
