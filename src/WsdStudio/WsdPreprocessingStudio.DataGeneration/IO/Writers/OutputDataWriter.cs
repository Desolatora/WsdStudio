using System.Collections.Generic;
using System.Linq;
using System.Text;
using WsdPreprocessingStudio.Core.IO.Writers;
using WsdPreprocessingStudio.DataGeneration.Data;
using WsdPreprocessingStudio.DataGeneration.Features;
using WsdPreprocessingStudio.DataGeneration.Features.Elements;

namespace WsdPreprocessingStudio.DataGeneration.IO.Writers
{
    public class OutputDataWriter : BasicFileWriter<RawRecord>
    {
        private const string Separator = " ";
        private const string NumericFormat = "0.#######";

        private readonly FeatureSelectionContext _context;

        public OutputDataWriter(
            string path, FeatureSelectionContext context)
            : base(path + "." + context.GenerationInfo.OutputFormat)
        {
            _context = context;

            if (context.GenerationInfo.OutputFormat == OutputFormat.arff)
                WriteArffHeader();
        }
        
        protected override void Write(RawRecord record)
        {
            var builder = new StringBuilder();
            var first = true;
            var quoteStrings = _context.GenerationInfo.OutputFormat == OutputFormat.arff;

            foreach (var featureGroup in _context.GenerationInfo.FeatureGroups)
            {
                var tuples = featureGroup.Source.GetTuples(record, _context);

                foreach (var tuple in tuples)
                {
                    IList<FeatureValue> values;

                    if (featureGroup.Source.RequiresAggregation)
                    {
                        var tupleItems = tuple
                            .Select(x => new EncounterValues(
                                x,
                                featureGroup
                                    .Elements
                                    .SelectMany(y => y.GetValues(x, _context))
                                    .ToArray()))
                            .ToArray();

                        values = featureGroup.AggregationFunction.Aggregate(
                            tupleItems, record, featureGroup, _context);
                    }
                    else
                    {
                        values = featureGroup
                            .Elements
                            .SelectMany(y => y.GetValues(tuple[0], _context))
                            .ToArray();
                    }

                    if (featureGroup.CompressionFunction != null)
                    {
                        values = new[]
                        {
                            featureGroup.CompressionFunction.Compress(
                                values, record, featureGroup, _context)
                        };
                    }

                    foreach (var value in values)
                    {
                        if (!first)
                            builder.Append(Separator);
                        else
                            first = false;

                        value.WriteTo(builder, NumericFormat, quoteStrings);
                    }
                }
            }

            BaseWriter.WriteLine(builder.ToString());
        }

        private void WriteArffHeader()
        {
            var builder = new StringBuilder();
            var groupNumber = 1;

            builder.AppendLine("@relation WsdPreprocessingStudioGeneratedData");
            
            foreach (var featureGroup in _context.GenerationInfo.FeatureGroups)
            {
                var tupleCount = featureGroup.Source.GetTupleCount(_context);

                for (var tupleNumber = 1; tupleNumber <= tupleCount; tupleNumber++)
                {
                    if (featureGroup.CompressionFunction != null)
                    {
                        builder.Append("@attribute GRP_");
                        builder.Append(groupNumber);
                        builder.Append("__");
                        builder.Append(featureGroup.Source.ArffAttributeName);
                        builder.Append("_");
                        builder.Append(tupleNumber);
                        builder.Append("_");

                        foreach (var element in featureGroup.Elements)
                        {
                            builder.Append("_");
                            builder.Append(element.ArffAttributeName);
                        }

                        builder.Append("__CMP");

                        if (featureGroup.CompressionFunction.RequiresCompressionElements)
                        {
                            builder.Append("_");

                            foreach (var element in featureGroup.CompressionElements)
                            {
                                builder.Append("_");
                                builder.Append(element.ArffAttributeName);
                            }
                        }

                        builder.Append(" ");
                        builder.Append(
                            featureGroup.ValueType == FeatureValueType.Numeric
                                ? "numeric"
                                : "string");
                        builder.AppendLine();
                    }
                    else
                    {
                        foreach (var element in featureGroup.Elements)
                        {
                            var elementValueCount = element.GetValueCount(_context);

                            for (var elementValueNumber = 1;
                                elementValueNumber <= elementValueCount;
                                elementValueNumber++)
                            {
                                builder.Append("@attribute GRP_");
                                builder.Append(groupNumber);
                                builder.Append("__");
                                builder.Append(featureGroup.Source.ArffAttributeName);
                                builder.Append("_");
                                builder.Append(tupleNumber);
                                builder.Append("__");
                                builder.Append(element.ArffAttributeName);
                                builder.Append("_");
                                builder.Append(elementValueNumber);
                                builder.Append(" ");

                                if (element is INominalFeatureElement nominalElement)
                                {
                                    builder.Append("{ ");

                                    var nominalValues = nominalElement.GetNominalValues(_context);

                                    for (var i = 0; i < nominalValues.Count; i++)
                                    {
                                        if (i > 0)
                                            builder.Append(",");

                                        nominalValues[i].WriteTo(builder, NumericFormat, false);
                                    }

                                    builder.Append(" }");
                                }
                                else
                                {
                                    builder.Append(
                                        featureGroup.ValueType == FeatureValueType.Numeric
                                            ? "numeric"
                                            : "string");
                                }

                                builder.AppendLine();
                            }
                        }
                    }
                }

                groupNumber++;
            }

            builder.Append("@data");

            BaseWriter.WriteLine(builder.ToString());
        }
    }
}