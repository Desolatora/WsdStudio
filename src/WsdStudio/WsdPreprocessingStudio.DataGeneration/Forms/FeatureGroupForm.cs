using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WsdPreprocessingStudio.Core.Extensions;
using WsdPreprocessingStudio.Core.Helpers;
using WsdPreprocessingStudio.Core.Plugins;
using WsdPreprocessingStudio.Core.UI;
using WsdPreprocessingStudio.DataGeneration.Features;
using WsdPreprocessingStudio.DataGeneration.Features.Elements;
using WsdPreprocessingStudio.DataGeneration.Features.Functions;
using WsdPreprocessingStudio.DataGeneration.Features.Sources;

namespace WsdPreprocessingStudio.DataGeneration.Forms
{
    public partial class FeatureGroupForm : Form
    {
        private readonly IFeatureSource[] _sources;
        private readonly FeatureValueType[] _types;
        private readonly IAggregationFunction[] _aggregations;
        private readonly ICompressionFunction[] _compressions;
        private readonly IFeatureElement[] _elements;

        private bool _refreshingUI;

        public FeatureGroup FeatureGroup { get; }

        public FeatureGroupForm(
            IPluginComponent[] pluginComponents, FeatureGroup featureGroup)
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer, true);

            _sources = new IFeatureSource[]
                {
                    new TargetWordSource(),
                    new ContextSource(),
                    new WindowSource(),
                    new ColocationSource()
                }.Union(pluginComponents
                    .Where(x => x is IFeatureSource)
                    .Cast<IFeatureSource>())
                .ToArray();

            _types = new[]
            {
                FeatureValueType.Numeric,
                FeatureValueType.String
            };

            _aggregations = new IAggregationFunction[]
                {
                    new StringConcat(),
                    new VectorSumWeightedByTrainEncounters()
                }.Union(pluginComponents
                    .Where(x => x is IAggregationFunction)
                    .Cast<IAggregationFunction>())
                .ToArray();

            _compressions = new ICompressionFunction[]
                {
                    new CosThetaUnitary(),
                    new CosThetaTargetWord()
                }.Union(pluginComponents
                    .Where(x => x is ICompressionFunction)
                    .Cast<ICompressionFunction>())
                .ToArray();

            _elements = new IFeatureElement[]
                {
                    new WordElement(),
                    new WordIdElement(),
                    new WordEmbeddingElement(),
                    new PosElement(),
                    new PosVectorElement(),
                    new MeaningElement(),
                    new MeaningIdElement(),
                    new MeaningEmbeddingElement(),
                    new MeaningEmbeddingOrMostFrequentElement(),
                    new MeaningEmbeddingOrAverageElement(),
                    new MeaningEmbeddingOrWeightedAverageElement()
                }.Union(pluginComponents
                    .Where(x => x is IFeatureElement)
                    .Cast<IFeatureElement>())
                .ToArray();

            SourceComboBox.Items.AddRange(
                _sources.Select(x => (object) x.DisplayName).ToArray());

            TypeComboBox.Items.AddRange(
                _types.Select(x => (object) x.ToString()).ToArray());

            if (featureGroup == null)
            {
                FeatureGroup = new FeatureGroup
                {
                    Elements = new List<IFeatureElement>(),
                    CompressionElements = new List<IFeatureElement>()
                };
                Text = "Add feature group";
            }
            else
            {
                FeatureGroup = new FeatureGroup
                {
                    Source = featureGroup.Source,
                    ValueType = featureGroup.ValueType,
                    Elements = featureGroup.Elements ?? new List<IFeatureElement>(),
                    CompressionFunction = featureGroup.CompressionFunction,
                    AggregationFunction = featureGroup.AggregationFunction,
                    CompressionElements = featureGroup.CompressionElements
                                          ?? new List<IFeatureElement>()
                };
                Text = "Edit feature group";
            }

            RefreshUI();
        }

        private void RefreshUI()
        {
            _refreshingUI = true;

            try
            {
                SourceComboBox.SelectedIndex = FeatureGroup.Source != null
                    ? SourceComboBox.Items.IndexOf(FeatureGroup.Source.DisplayName)
                    : -1;

                TypeComboBox.SelectedIndex = TypeComboBox.Items.IndexOf(FeatureGroup.ValueType.ToString());

                using (AggregationComboBox.UpdateScope())
                {
                    AggregationComboBox.Enabled = FeatureGroup.Source?.RequiresAggregation ?? false;
                    AggregationComboBox.Items.Clear();
                    AggregationComboBox.Items.AddRange(
                        _aggregations
                            .Where(x => x.SupportedFeatureTypes.Contains(FeatureGroup.ValueType))
                            .Select(x => (object) x.DisplayName)
                            .ToArray());
                    AggregationComboBox.SelectedIndex = FeatureGroup.AggregationFunction != null
                        ? AggregationComboBox.Items.IndexOf(FeatureGroup.AggregationFunction.DisplayName)
                        : -1;
                }

                using (CompressionComboBox.UpdateScope())
                {
                    CompressionComboBox.Enabled =
                        _compressions.Any(x => x.SupportedFeatureTypes.Contains(FeatureGroup.ValueType));
                    CompressionComboBox.Items.Clear();
                    CompressionComboBox.Items.AddRange(
                        _compressions
                            .Where(x => x.SupportedFeatureTypes.Contains(FeatureGroup.ValueType))
                            .Select(x => (object) x.DisplayName)
                            .ToArray());
                    CompressionComboBox.SelectedIndex = FeatureGroup.CompressionFunction != null
                        ? CompressionComboBox.Items.IndexOf(FeatureGroup.CompressionFunction.DisplayName)
                        : -1;
                }

                ClearAggregationButton.Enabled = AggregationComboBox.Enabled;
                ClearCompressionButton.Enabled = CompressionComboBox.Enabled;

                using (ElementsListBox.UpdateScope())
                {
                    ElementsListBox.Items.Clear();

                    foreach (var element in _elements)
                    {
                        if (element.FeatureType != FeatureGroup.ValueType)
                            continue;

                        ElementsListBox.Items.Add(
                            element.DisplayName,
                            FeatureGroup.Elements.Any(x => x.DisplayName == element.DisplayName));
                    }
                }

                CompressionElementsListBox.Enabled =
                    FeatureGroup.CompressionFunction != null &&
                    FeatureGroup.CompressionFunction.RequiresCompressionElements;

                if (CompressionElementsListBox.Enabled)
                {
                    using (CompressionElementsListBox.UpdateScope())
                    {
                        CompressionElementsListBox.Items.Clear();

                        foreach (var element in _elements)
                        {
                            if (element.FeatureType != FeatureGroup.ValueType)
                                continue;

                            CompressionElementsListBox.Items.Add(
                                element.DisplayName,
                                FeatureGroup.CompressionElements.Any(x => x.DisplayName == element.DisplayName));
                        }
                    }

                    Size = MaximumSize;
                    CompressionElementsListBox.Visible = true;
                }
                else
                {
                    Size = MinimumSize;
                    CompressionElementsListBox.Visible = false;
                }
            }
            finally
            {
                _refreshingUI = false;
            }
        }

        private void FeatureGroupForm_Shown(object sender, EventArgs e)
        {
            if (Owner != null)
            {
                Location = new Point(
                    Owner.Left + Owner.Width / 2 - Width / 2,
                    Owner.Top + Owner.Height / 2 - Height / 2);
            }
        }

        private void SourceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_refreshingUI)
                return;

            FeatureGroup.Source = _sources
                .FirstOrDefault(x => x.DisplayName == (string) SourceComboBox.SelectedItem);
            FeatureGroup.AggregationFunction = null;

            RefreshUI();
        }

        private void TypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_refreshingUI)
                return;

            FeatureGroup.ValueType = (FeatureValueType)
                Enum.Parse(typeof(FeatureValueType), (string) TypeComboBox.SelectedItem);
            FeatureGroup.AggregationFunction = null;
            FeatureGroup.CompressionFunction = null;
            FeatureGroup.Elements.Clear();
            
            RefreshUI();
        }

        private void AggregationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_refreshingUI)
                return;

            FeatureGroup.AggregationFunction = _aggregations
                .FirstOrDefault(x => x.DisplayName == (string) AggregationComboBox.SelectedItem);

            RefreshUI();
        }

        private void ClearAggregationButton_Click(object sender, EventArgs e)
        {
            FeatureGroup.AggregationFunction = null;

            RefreshUI();
        }

        private void CompressionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_refreshingUI)
                return;

            FeatureGroup.CompressionFunction = _compressions
                .FirstOrDefault(x => x.DisplayName == (string) CompressionComboBox.SelectedItem);

            RefreshUI();
        }

        private void ClearCompressionButton_Click(object sender, EventArgs e)
        {
            FeatureGroup.CompressionFunction = null;

            RefreshUI();
        }

        private void ElementsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_refreshingUI)
                return;

            var newElements = _elements
                .Where(x => ElementsListBox.CheckedItems.Contains(x.DisplayName))
                .ToList();

            if (FeatureGroup.Elements.SequenceCompare(
                FeatureGroup.CompressionElements, 
                (x, y) => x.DisplayName == y.DisplayName))
            {
                FeatureGroup.CompressionElements = newElements;
            }

            FeatureGroup.Elements = newElements;

            RefreshUI();
        }

        private void CompressionElementsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_refreshingUI)
                return;

            FeatureGroup.CompressionElements = _elements
                .Where(x => CompressionElementsListBox.CheckedItems.Contains(x.DisplayName))
                .ToList();

            RefreshUI();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (FeatureGroup.Source == null)
            {
                MessageBox.Show("The field \"Source\" is required.", "Error");

                return;
            }

            if (FeatureGroup.Source.RequiresAggregation &&
                FeatureGroup.AggregationFunction == null)
            {
                MessageBox.Show(
                    "The field \"Aggregation\" is required " +
                    $"when using Source \"{FeatureGroup.Source.DisplayName}\".", "Error");

                return;
            }

            if (FeatureGroup.Elements.Count == 0)
            {
                MessageBox.Show("At least one element must be selected.", "Error");

                return;
            }

            if (FeatureGroup.CompressionFunction != null &&
                FeatureGroup.CompressionFunction.RequiresCompressionElements &&
                FeatureGroup.CompressionElements.Count == 0)
            {
                MessageBox.Show("At least one compression element must be selected.", "Error");

                return;
            }

            DialogResult = DialogResult.OK;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
