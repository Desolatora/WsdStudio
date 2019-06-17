using System;
using System.Collections.Generic;
using System.Linq;
using WsdPreprocessingStudio.DataGeneration.Data;

namespace WsdPreprocessingStudio.DataGeneration.Algorithms
{
    public class ValidationSetExtractor
    {
        public void Extract(
            IEnumerable<DataSetGroup> dataSetGroups, GenerationInfo info)
        {
            foreach (var dataSetGroup in dataSetGroups)
            {
                var oldTrainSet = dataSetGroup.DataSets.GetByName(DataSetName.Train);

                if (oldTrainSet == null)
                    continue;

                var validationSplit = oldTrainSet.Data
                    .GroupBy(x => x.Word + x.Meaning)
                    .Select(x =>
                    {
                        var groupCount = x.Count();
                        var validationGroupCount = (int) Math.Ceiling(
                            groupCount * 0.01 * info.ValidationSetPercentage);

                        return new
                        {
                            TrainGroup = x.Skip(validationGroupCount).ToArray(),
                            ValidationGroup = x.Take(validationGroupCount).ToArray()
                        };
                    })
                    .ToArray();

                var trainExamples = validationSplit.SelectMany(x => x.TrainGroup).ToArray();
                var validationExamples = validationSplit.SelectMany(x => x.ValidationGroup).ToArray();
                
                if (trainExamples.Length > 0)
                {
                    dataSetGroup.DataSets[DataSetName.Train] = new DataSet(DataSetName.Train, trainExamples);
                }
                else
                {
                    dataSetGroup.DataSets.Remove(DataSetName.Train);
                }

                if (validationExamples.Length > 0)
                {
                    dataSetGroup.DataSets[DataSetName.Validation] = new DataSet(
                        DataSetName.Validation, validationExamples);
                }
                else
                {
                    dataSetGroup.DataSets.Remove(DataSetName.Validation);
                }
            }
        }
    }
}
