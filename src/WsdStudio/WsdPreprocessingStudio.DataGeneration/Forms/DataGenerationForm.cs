using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using WsdPreprocessingStudio.Core;
using WsdPreprocessingStudio.Core.Data.Validation;
using WsdPreprocessingStudio.Core.Extensions;
using WsdPreprocessingStudio.Core.Helpers;
using WsdPreprocessingStudio.Core.IO.Readers.System;
using WsdPreprocessingStudio.Core.IO.Writers.System;
using WsdPreprocessingStudio.Core.Plugins;
using WsdPreprocessingStudio.Core.Threading;
using WsdPreprocessingStudio.DataGeneration.Binders;
using WsdPreprocessingStudio.DataGeneration.Data;
using WsdPreprocessingStudio.DataGeneration.Features;
using WsdPreprocessingStudio.DataGeneration.Features.Elements;
using WsdPreprocessingStudio.DataGeneration.Features.Functions;
using WsdPreprocessingStudio.DataGeneration.Features.Sources;

namespace WsdPreprocessingStudio.DataGeneration.Forms
{
    public partial class DataGenerationForm : Form
    {
        private IPluginComponent[] _pluginComponents;
        private WsdProject _project;
        private IProgressHandleFactory _progressFactory;

        private GenerationInfo _generationInfo = new GenerationInfo
        {
            LeftContext = 5,
            RightContext = 5,
            OrderMeanings = OrderMeanings.ByDictionary,
            OrderMeaningsStrategy = OrderMeaningsStrategy.GroupByWordAndPos,
            ValidationSetPercentage = 20,
            FeatureGroups =
            {
                new FeatureGroup
                {
                    Source = new TargetWordSource(),
                    ValueType = FeatureValueType.Numeric,
                    Elements = new List<IFeatureElement>
                    {
                        new MeaningIdElement()
                    }
                },
                new FeatureGroup
                {
                    Source = new TargetWordSource(),
                    ValueType = FeatureValueType.Numeric,
                    Elements = new List<IFeatureElement>
                    {
                        new WordEmbeddingElement()
                    },
                    CompressionFunction = new CosThetaUnitary()
                },
                new FeatureGroup
                {
                    Source = new ContextSource(),
                    ValueType = FeatureValueType.Numeric,
                    Elements = new List<IFeatureElement>
                    {
                        new WordEmbeddingElement()
                    },
                    CompressionFunction = new CosThetaTargetWord(),
                    CompressionElements = new List<IFeatureElement>
                    {
                        new WordEmbeddingElement()
                    }
                },
                new FeatureGroup
                {
                    Source = new ContextSource(),
                    ValueType = FeatureValueType.Numeric,
                    Elements = new List<IFeatureElement>
                    {
                        new PosVectorElement()
                    }
                }
            }
        };

        private bool _refreshingUI;
        private int _selectedFeatureGroupIndex = -1;

        public DataGenerationForm(
            IPluginComponent[] pluginComponents, WsdProject project, IProgressHandleFactory progressFactory)
        {
            _pluginComponents = pluginComponents ?? throw new ArgumentNullException(nameof(project));
            _project = project ?? throw new ArgumentNullException(nameof(project));
            _progressFactory = progressFactory ?? throw new ArgumentNullException(nameof(progressFactory));

            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer, true);

            GenerateData_SavingStrategyComboBox.Items.AddRange(new object[]
            {
                SavingStrategy.SingleFile,
                SavingStrategy.FilePerWord,
                SavingStrategy.FilePerPos,
                SavingStrategy.FilePerWordAndPos,
                SavingStrategy.OriginalFiles
            });

            GenerateData_OutputFormatComboBox.Items.AddRange(new object[]
            {
                OutputFormat.txt,
                OutputFormat.arff
            });

            GenerateData_OrderMeaningsComboBox.Items.AddRange(new object[]
            {
                OrderMeanings.None,
                OrderMeanings.ByDictionary,
                OrderMeanings.ByTrainingSet,
                OrderMeanings.ByDictionaryAndTrainingSet
            });

            GenerateData_OrderMeaningsStrategyComboBox.Items.AddRange(new object[]
            {
                OrderMeaningsStrategy.GroupByWordAndPos,
                OrderMeaningsStrategy.GroupByWord
            });

            foreach (var pos in project.PosList)
            {
                PosList_CheckedListBox.Items.Add(pos);

                if (pos != "X" && pos != ".")
                    _generationInfo.FilteredPosList.Add(pos);
            }

            RefreshUI(true);
        }

        private void RefreshUI(bool invalidateList = false)
        {
            _refreshingUI = true;

            try
            {
                for (var i = 0; i < PosList_CheckedListBox.Items.Count; i++)
                {
                    PosList_CheckedListBox.SetItemChecked(
                        i, _generationInfo.FilteredPosList.Contains(PosList_CheckedListBox.Items[i]));
                }

                GenerateData_LeftContextNumericUpDown.Value = _generationInfo.LeftContext;
                GenerateData_RightContextNumericUpDown.Value = _generationInfo.RightContext;
                GenerateData_OverlapCheckBox.Checked = _generationInfo.Overlap;
                GenerateData_SavingStrategyComboBox.SelectedItem = _generationInfo.SavingStrategy;
                GenerateData_OutputFormatComboBox.SelectedItem = _generationInfo.OutputFormat;
                GenerateData_OrderMeaningsComboBox.SelectedItem = _generationInfo.OrderMeanings;
                GenerateData_OrderMeaningsStrategyComboBox.SelectedItem = _generationInfo.OrderMeaningsStrategy;
                GenerateData_ExtractValidationSetCheckBox.Checked = _generationInfo.ExtractValidationSet;
                GenerateData_ValidationSetPercentageLabel.Text = _generationInfo.ValidationSetPercentage + "%";
                GenerateData_ValidationSetPercentageTrackBar.Value = _generationInfo.ValidationSetPercentage;
                GenerateData_ValidationSetPercentageLabel.Enabled = _generationInfo.ExtractValidationSet;
                GenerateData_ValidationSetPercentageTrackBar.Enabled =  _generationInfo.ExtractValidationSet;

                GenerateData_ShuffleDataCheckbox.Checked = _generationInfo.ShuffleData;

                if (invalidateList)
                {
                    FeatureGroupsBinder.BindToListView(
                        _generationInfo.FeatureGroups, GenerateData_FeatureGroupsListView);
                }

                GenerateData_FeatureGroupsListView.SelectedIndices.Clear();

                if (_selectedFeatureGroupIndex > -1)
                    GenerateData_FeatureGroupsListView.SelectedIndices.Add(_selectedFeatureGroupIndex);

                GenerateData_UpButton.Enabled = _selectedFeatureGroupIndex > -1;
                GenerateData_DownButton.Enabled = _selectedFeatureGroupIndex > -1;
                GenerateData_RemoveButton.Enabled = _selectedFeatureGroupIndex > -1;
                GenerateData_GenerateDataButton.Enabled = _generationInfo.FeatureGroups.Count > 0;
            }
            finally
            {
                _refreshingUI = false;
            }
        }

        private void GenerateData_LeftContextNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (_refreshingUI)
                return;

            _generationInfo.LeftContext = (int) GenerateData_LeftContextNumericUpDown.Value;
        }

        private void GenerateData_RightContextNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (_refreshingUI)
                return;

            _generationInfo.RightContext = (int) GenerateData_RightContextNumericUpDown.Value;
        }

        private void GenerateData_OverlapCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_refreshingUI)
                return;

            _generationInfo.Overlap = GenerateData_OverlapCheckBox.Checked;
        }

        private void GenerateData_SavingStrategyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_refreshingUI)
                return;

            _generationInfo.SavingStrategy =
                (SavingStrategy) GenerateData_SavingStrategyComboBox.SelectedItem;

            RefreshUI();
        }

        private void GenerateData_OutputFormatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_refreshingUI)
                return;

            _generationInfo.OutputFormat =
                (OutputFormat) GenerateData_OutputFormatComboBox.SelectedItem;

            var invalidateList = false;

            if (_generationInfo.OutputFormat == OutputFormat.arff)
            {
                var meaningIdFeatureGroup = _generationInfo.FeatureGroups
                    .FirstOrDefault(x => x.Elements.Count == 1 && x.Elements[0] is MeaningIdElement);

                if (meaningIdFeatureGroup != null)
                {
                    var meaningIdFeatureGroupIndex = _generationInfo.FeatureGroups.IndexOf(meaningIdFeatureGroup);

                    if (meaningIdFeatureGroupIndex > -1 &&
                        meaningIdFeatureGroupIndex < _generationInfo.FeatureGroups.Count - 1)
                    {
                        if (MessageBox.Show(
                            "Found a feature group containing only MeaningId element. " +
                            "Move to last position?", "Suggestion", MessageBoxButtons.YesNo) ==
                            DialogResult.Yes)
                        {
                            for (var i = meaningIdFeatureGroupIndex; i < _generationInfo.FeatureGroups.Count - 1; i++)
                            {
                                _generationInfo.FeatureGroups.SwapIndices(i, i + 1);
                            }

                            invalidateList = true;
                        }
                    }
                }
            }

            RefreshUI(invalidateList);
        }

        private void GenerateData_OrderMeaningsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_refreshingUI)
                return;

            _generationInfo.OrderMeanings =
                (OrderMeanings) GenerateData_OrderMeaningsComboBox.SelectedItem;

            RefreshUI();
        }

        private void GenerateData_OrderMeaningsStrategyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_refreshingUI)
                return;

            _generationInfo.OrderMeaningsStrategy =
                (OrderMeaningsStrategy) GenerateData_OrderMeaningsStrategyComboBox.SelectedItem;

            RefreshUI();
        }

        private void GenerateData_ExtractValidationSetCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_refreshingUI)
                return;

            _generationInfo.ExtractValidationSet = GenerateData_ExtractValidationSetCheckBox.Checked;

            RefreshUI();
        }

        private void GenerateData_ValidationSetPercentageTrackBar_Scroll(object sender, EventArgs e)
        {
            if (_refreshingUI)
                return;

            _generationInfo.ValidationSetPercentage = GenerateData_ValidationSetPercentageTrackBar.Value;

            RefreshUI();
        }

        private void GenerateData_ShuffleDataCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (_refreshingUI)
                return;

            _generationInfo.ShuffleData = GenerateData_ShuffleDataCheckbox.Checked;

            RefreshUI();
        }

        private void GenerateData_FeatureGroupsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_refreshingUI)
                return;

            _selectedFeatureGroupIndex = GenerateData_FeatureGroupsListView.SelectedIndices.Count > 0
                ? GenerateData_FeatureGroupsListView.SelectedIndices[0]
                : -1;

            if (_selectedFeatureGroupIndex >= _generationInfo.FeatureGroups.Count)
                _selectedFeatureGroupIndex = -1;

            RefreshUI();
        }

        private void GenerateData_UpButton_Click(object sender, EventArgs e)
        {
            if (_selectedFeatureGroupIndex >= 1 &&
                _selectedFeatureGroupIndex < _generationInfo.FeatureGroups.Count)
            {
                _generationInfo.FeatureGroups.SwapIndices(
                    _selectedFeatureGroupIndex, _selectedFeatureGroupIndex - 1);

                _selectedFeatureGroupIndex--;

                RefreshUI(true);
            }

            GenerateData_FeatureGroupsListView.Focus();
        }

        private void GenerateData_DownButton_Click(object sender, EventArgs e)
        {
            if (_selectedFeatureGroupIndex >= 0 &&
                _selectedFeatureGroupIndex < _generationInfo.FeatureGroups.Count - 1)
            {
                _generationInfo.FeatureGroups.SwapIndices(
                    _selectedFeatureGroupIndex, _selectedFeatureGroupIndex + 1);

                _selectedFeatureGroupIndex++;

                RefreshUI(true);
            }

            GenerateData_FeatureGroupsListView.Focus();
        }

        private void GenerateData_AddButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new FeatureGroupForm(_pluginComponents, null))
            {
                var result = dialog.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    _generationInfo.FeatureGroups.Add(dialog.FeatureGroup);
                    
                    RefreshUI(true);

                    GenerateData_FeatureGroupsListView.Focus();
                }
            }
        }

        private void GenerateData_FeatureGroupsListView_DoubleClick(object sender, EventArgs e)
        {
            if (_selectedFeatureGroupIndex < 0 ||
                _selectedFeatureGroupIndex >= _generationInfo.FeatureGroups.Count)
                return;

            using (var dialog = new FeatureGroupForm(
                _pluginComponents, _generationInfo.FeatureGroups[_selectedFeatureGroupIndex]))
            {
                var result = dialog.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    _generationInfo.FeatureGroups[_selectedFeatureGroupIndex] = dialog.FeatureGroup;

                    RefreshUI(true);

                    GenerateData_FeatureGroupsListView.Focus();
                }
            }
        }

        private void GenerateData_RemoveButton_Click(object sender, EventArgs e)
        {
            if (_selectedFeatureGroupIndex < 0 ||
                _selectedFeatureGroupIndex >= _generationInfo.FeatureGroups.Count)
                return;

            _generationInfo.FeatureGroups.RemoveAt(_selectedFeatureGroupIndex);
            _selectedFeatureGroupIndex =
                Math.Min(_selectedFeatureGroupIndex, _generationInfo.FeatureGroups.Count - 1);

            RefreshUI(true);

            GenerateData_FeatureGroupsListView.Focus();
        }

        private void GenerateData_GenerateDataButton_Click(object sender, EventArgs e)
        {
            var generationInfoValidationResult = _generationInfo.Validate();

            if (generationInfoValidationResult != ValidationResult.Success)
            {
                MessageBox.Show(
                    string.Join(" ", generationInfoValidationResult.Errors.Select(x => x.ErrorMessage)),
                    "Error");

                return;
            }

            if (!_project.HasMeaningEmbeddings &&
                _generationInfo.FeatureGroups
                    .Any(x => x.Elements
                        .Any(y => y is MeaningEmbeddingElement)))
            {
                MessageBox.Show(
                    "Current project does not include meaning embeddings. " +
                    "Please remove all MeaningEmbedding elements or load a different project.",
                    "Error");

                return;
            }

            using (var dialog = DialogEx.SelectFolder())
            {
                var result = dialog.ShowDialog();

                if (result == CommonFileDialogResult.Ok)
                {
                    _generationInfo.DestinationFolder = dialog.FileName;

                    if (Directory.GetFiles(dialog.FileName, "*", SearchOption.AllDirectories).Length > 0)
                    {
                        MessageBox.Show("Directory must be empty.", "Error");

                        return;
                    }

                    Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            using (var progress = _progressFactory.NewInstance("Generating data..."))
                            {
                                new DataGenerator(_pluginComponents)
                                    .Generate(_project, _generationInfo, progress);

                                this.InvokeIfRequired(() => RefreshUI());
                            }

                            if (Directory.Exists(dialog.FileName))
                                Process.Start(dialog.FileName);
                        }
                        catch (OperationCanceledException)
                        {
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString(), "Error");
                        }
                    }, TaskCreationOptions.LongRunning);
                }
            }
        }

        private void PosList_CheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_refreshingUI)
                return;

            _generationInfo.FilteredPosList.Clear();
            _generationInfo.FilteredPosList.AddRange(PosList_CheckedListBox.CheckedItems.Cast<string>());

            RefreshUI();
        }

        private void GenerateData_LoadGenerationInfoButton_Click(object sender, EventArgs e)
        {
            using (var dialog = DialogEx.OpenFile("Generation info files (*.wsdgeninfo)|*.wsdgeninfo"))
            {
                var result = dialog.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    _generationInfo = SystemJsonReader.Read<GenerationInfo>(dialog.FileName);
                    _selectedFeatureGroupIndex = -1;

                    RefreshUI(true);

                    MessageBox.Show("Generation info loaded successfully.", "Success");
                }
            }
        }

        private void GenerateData_SaveGenerationInfoButton_Click(object sender, EventArgs e)
        {
            using (var dialog = DialogEx.SaveFile("Generation info files (*.wsdgeninfo)|*.wsdgeninfo", ".wsdgeninfo"))
            {
                var result = dialog.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    SystemJsonWriter.Write(dialog.FileName, _generationInfo);

                    RefreshUI(true);

                    MessageBox.Show("Generation info saved successfully.", "Success");
                }
            }
        }
    }
}
