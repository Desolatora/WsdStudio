using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using WsdPreprocessingStudio.Core.Data.Validation;
using WsdPreprocessingStudio.DataGeneration.Features;

namespace WsdPreprocessingStudio.DataGeneration.Data
{
    public class GenerationInfo : IValidatable
    {
        [JsonIgnore]
        public string DestinationFolder { get; set; }

        public int LeftContext { get; set; }
        public int RightContext { get; set; }
        public bool Overlap { get; set; }
        public bool ExtractValidationSet { get; set; }
        public int ValidationSetPercentage { get; set; }
        public bool ShuffleData { get; set; }
        public SavingStrategy SavingStrategy { get; set; }
        public OutputFormat OutputFormat { get; set; }
        public OrderMeanings OrderMeanings { get; set; }
        public OrderMeaningsStrategy OrderMeaningsStrategy { get; set; }
        public List<string> FilteredPosList { get; }
        public List<FeatureGroup> FeatureGroups { get; }

        public GenerationInfo()
        {
            FilteredPosList = new List<string>();
            FeatureGroups = new List<FeatureGroup>();
        }

        public void AssertIsValid()
        {
            var result = Validate();

            if (result != ValidationResult.Success)
                throw new ValidationException(result);
        }

        public ValidationResult Validate()
        {
            var errors = new List<ValidationError>();

            if (FilteredPosList.Count == 0)
                errors.Add(new ValidationError(
                    "At least one POS is required.", nameof(FilteredPosList)));

            if (FeatureGroups.Count == 0)
                errors.Add(new ValidationError(
                    "At least one FeatureGroup is required.", nameof(FeatureGroups)));

            for (var i = 0; i < FeatureGroups.Count; i++)
            {
                if (FeatureGroups[i].Source == null)
                    errors.Add(new ValidationError(
                        $"FeatureGroup {i + 1}: Source is required.",
                        nameof(FeatureGroup.Source)));

                if (FeatureGroups[i].Source.RequiresAggregation &&
                    FeatureGroups[i].AggregationFunction == null)
                    errors.Add(new ValidationError(
                        $"FeatureGroup {i + 1}: Source required aggregation.",
                        nameof(FeatureGroup.AggregationFunction)));

                if (FeatureGroups[i].Elements.Count == 0)
                    errors.Add(new ValidationError(
                        $"FeatureGroup {i + 1}: At least one element is required.",
                        nameof(FeatureGroup.Elements)));

                if (FeatureGroups[i].Elements.Any(x => x.FeatureType != FeatureGroups[i].ValueType))
                    errors.Add(new ValidationError(
                        $"FeatureGroup {i + 1}: Only elements of type " +
                        $"{FeatureGroups[i].ValueType} are allowed.",
                        nameof(FeatureGroup.Elements)));

                if (FeatureGroups[i].AggregationFunction != null &&
                    !FeatureGroups[i].AggregationFunction.SupportedFeatureTypes.Contains(
                        FeatureGroups[i].ValueType))
                    errors.Add(new ValidationError(
                        $"FeatureGroup {i + 1}: Aggregation function does not support " +
                        $"features of type {FeatureGroups[i].ValueType}.",
                        nameof(FeatureGroup.AggregationFunction)));

                if (FeatureGroups[i].CompressionFunction != null &&
                    !FeatureGroups[i].CompressionFunction.SupportedFeatureTypes.Contains(
                        FeatureGroups[i].ValueType))
                    errors.Add(new ValidationError(
                        $"FeatureGroup {i + 1}: Compression function does not support " +
                        $"features of type {FeatureGroups[i].ValueType}.",
                        nameof(FeatureGroup.AggregationFunction)));

                if (FeatureGroups[i].CompressionFunction != null &&
                    FeatureGroups[i].CompressionFunction.RequiresCompressionElements &&
                    (FeatureGroups[i].CompressionElements == null ||
                     FeatureGroups[i].CompressionElements.Count == 0))
                    errors.Add(new ValidationError(
                        $"FeatureGroup {i + 1}: At least one compression element is required.",
                        nameof(FeatureGroup.CompressionElements)));

                if (FeatureGroups[i].CompressionElements != null &&
                    FeatureGroups[i].CompressionElements.Any(x => x.FeatureType != FeatureGroups[i].ValueType))
                    errors.Add(new ValidationError(
                        $"FeatureGroup {i + 1}: Only compression elements of type " +
                        $"{FeatureGroups[i].ValueType} are allowed.",
                        nameof(FeatureGroup.CompressionElements)));

            }

            return errors.Count == 0
                ? ValidationResult.Success
                : new ValidationResult(errors);
        }
    }

    public enum SavingStrategy
    {
        SingleFile,
        FilePerWord,
        FilePerPos,
        FilePerWordAndPos,
        OriginalFiles
    }

    public enum OutputFormat
    {
        txt,
        arff
    }

    public enum OrderMeanings
    {
        None,
        ByDictionary,
        ByTrainingSet,
        ByDictionaryAndTrainingSet
    }

    public enum OrderMeaningsStrategy
    {
        GroupByWordAndPos,
        GroupByWord
    }
}
