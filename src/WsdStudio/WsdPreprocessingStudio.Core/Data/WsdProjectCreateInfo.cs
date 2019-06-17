using System.Collections.Generic;
using WsdPreprocessingStudio.Core.Data.Validation;

namespace WsdPreprocessingStudio.Core.Data
{
    public class WsdProjectCreateInfo : IValidatable
    {
        public InputDataType DataType { get; set; }
        public string DictionaryPath { get; set; }
        public string SynsetMappingsPath { get; set; }
        public string TrainDataPath { get; set; }
        public string TrainGoldKeyPath { get; set; }
        public string TestDataPath { get; set; }
        public string TestGoldKeyPath { get; set; }
        public string WordEmbeddingsPath { get; set; }
        public string MeaningEmbeddingsPath { get; set; }

        public void AssertIsValid()
        {
            var result = Validate();

            if (result != ValidationResult.Success)
                throw new ValidationException(result);
        }

        public ValidationResult Validate()
        {
            var errors = new List<ValidationError>();

            if (string.IsNullOrEmpty(DictionaryPath))
                errors.Add(ValidationError.NotNullOrEmpty(nameof(DictionaryPath)));

            if (string.IsNullOrEmpty(TrainDataPath))
                errors.Add(ValidationError.NotNullOrEmpty(nameof(TrainDataPath)));

            if (string.IsNullOrEmpty(TestDataPath))
                errors.Add(ValidationError.NotNullOrEmpty(nameof(TestDataPath)));

            if (string.IsNullOrEmpty(WordEmbeddingsPath))
                errors.Add(ValidationError.NotNullOrEmpty(nameof(WordEmbeddingsPath)));

            if (DataType == InputDataType.UEFXML)
            {
                if (string.IsNullOrEmpty(SynsetMappingsPath))
                    errors.Add(ValidationError.NotNullOrEmpty(nameof(SynsetMappingsPath)));

                if (string.IsNullOrEmpty(TrainGoldKeyPath))
                    errors.Add(ValidationError.NotNullOrEmpty(nameof(TrainGoldKeyPath)));

                if (string.IsNullOrEmpty(TestGoldKeyPath))
                    errors.Add(ValidationError.NotNullOrEmpty(nameof(TestGoldKeyPath)));
            }

            return errors.Count == 0
                ? ValidationResult.Success
                : new ValidationResult(errors);
        }
    }

    public enum InputDataType
    {
        PlainText,
        UEFXML
    }
}
