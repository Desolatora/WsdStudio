using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace WsdPreprocessingStudio.DataGeneration.Features
{
    public struct FeatureValue
    {
        private object _value;

        public FeatureValueType FeatureType { get; }

        public float NumericValue
        {
            get
            {
                try
                {
                    return (float) _value;
                }
                catch (Exception ex)
                {
                    throw new Exception(
                        "FeatureValue was expected to be a Numeric but is actually a String.", ex);
                }
            }
        }

        public string StringValue
        {
            get
            {
                try
                {
                    return (string) _value;
                }
                catch (Exception ex)
                {
                    throw new Exception(
                        "FeatureValue was expected to be a String but is actually a Numeric.", ex);
                }
            }
        }

        public FeatureValue(float value)
        {
            FeatureType = FeatureValueType.Numeric;

            _value = value;
        }

        public FeatureValue(string value)
        {
            FeatureType = FeatureValueType.String;

            _value = value;
        }

        public void WriteTo(StringBuilder builder, string numericFormat, bool quoteStrings)
        {
            builder.Append(
                FeatureType == FeatureValueType.Numeric
                    ? NumericValue.ToString(numericFormat, CultureInfo.InvariantCulture)
                    : quoteStrings
                        ? "\"" + StringValue + "\""
                        : StringValue);
        }

        public static FeatureValue[] NewArray(float value, int length)
        {
            var result = new FeatureValue[length];

            for (var i = 0; i < result.Length; i++)
            {
                result[i] = new FeatureValue(value);
            }

            return result;
        }

        public static FeatureValue[] NewArray(string value, int length)
        {
            var result = new FeatureValue[length];

            for (var i = 0; i < result.Length; i++)
            {
                result[i] = new FeatureValue(value);
            }

            return result;
        }
        
        public static FeatureValue[] NewArray(IList<float> values)
        {
            var result = new FeatureValue[values.Count];

            for (var i = 0; i < result.Length; i++)
            {
                result[i] = new FeatureValue(values[i]);
            }

            return result;
        }

        public static FeatureValue[] NewArray(IList<string> values)
        {
            var result = new FeatureValue[values.Count];

            for (var i = 0; i < result.Length; i++)
            {
                result[i] = new FeatureValue(values[i]);
            }

            return result;
        }
    }

    public enum FeatureValueType
    {
        Numeric,
        String
    }
}
