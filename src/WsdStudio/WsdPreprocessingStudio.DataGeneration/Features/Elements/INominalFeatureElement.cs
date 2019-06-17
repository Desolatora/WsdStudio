using System.Collections.Generic;

namespace WsdPreprocessingStudio.DataGeneration.Features.Elements
{
    public interface INominalFeatureElement : IFeatureElement
    {
        IList<FeatureValue> GetNominalValues(FeatureSelectionContext context);
    }
}
