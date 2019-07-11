using System;
using System.Collections.Generic;
using System.Linq;
using WsdPreprocessingStudio.Core.Threading;
using WsdPreprocessingStudio.DataGeneration.Data;
using WsdPreprocessingStudio.DataGeneration.Resources;

namespace WsdPreprocessingStudio.DataGeneration.Algorithms
{
    public class ValidationSetExtractor
    {
        public void Extract(
            IList<DataSetGroup> dataSetGroups, GenerationInfo info, IProgressHandle progress)
        {
            using (var scope = progress.Scope(dataSetGroups.Count, MessageFormat.ExtractingValidationSet_Groups))
            {
                var counter = 0;

                foreach (var dataSetGroup in dataSetGroups)
                {
                    scope.TrySet(counter++);

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
}
