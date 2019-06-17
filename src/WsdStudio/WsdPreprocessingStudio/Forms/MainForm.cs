using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using WsdPreprocessingStudio.Binders;
using WsdPreprocessingStudio.Core;
using WsdPreprocessingStudio.Core.Data;
using WsdPreprocessingStudio.Core.Data.Validation;
using WsdPreprocessingStudio.Core.Extensions;
using WsdPreprocessingStudio.Core.Helpers;
using WsdPreprocessingStudio.Core.Plugins;
using WsdPreprocessingStudio.Core.Threading;
using WsdPreprocessingStudio.Core.UI;
using WsdPreprocessingStudio.UI;

namespace WsdPreprocessingStudio.Forms
{
    public partial class MainForm : Form
    {
        private WsdProjectCreateInfo _projectCreateInfoPlainText = new WsdProjectCreateInfo
        {
            DataType = InputDataType.PlainText
        };

        private WsdProjectCreateInfo _projectCreateInfoUEFXML = new WsdProjectCreateInfo
        {
            DataType = InputDataType.UEFXML
        };

        private bool _refreshingUI;
        private IProgressHandleFactory _progressFactory;
        private PluginInfo[] _pluginInfos;

        public MainForm()
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer, true);

            Text = $"WSD Preprocessing Studio v.{Assembly.GetExecutingAssembly().GetName().Version}";

            _progressFactory = new ProgressHandleFactory(this);

            RefreshUI();
        }

        private void RefreshUI()
        {
            _refreshingUI = true;

            try
            {
                CloseProjectButton.Enabled = MainTabControl.SelectedIndex > 0;

                // Create project PlainText
                var validationResultPlainText = _projectCreateInfoPlainText.Validate();

                CreateProject_PlainText_DictionaryFileTextBox.Text =
                    _projectCreateInfoPlainText.DictionaryPath;
                CreateProject_PlainText_TrainFolderTextBox.Text =
                    _projectCreateInfoPlainText.TrainDataPath;
                CreateProject_PlainText_TestFolderTextBox.Text =
                    _projectCreateInfoPlainText.TestDataPath;
                CreateProject_PlainText_WordEmbeddingsFileTextBox.Text =
                    _projectCreateInfoPlainText.WordEmbeddingsPath;
                CreateProject_PlainText_MeaningEmbeddingsFileTextBox.Text =
                    _projectCreateInfoPlainText.MeaningEmbeddingsPath;
                CreateProject_PlainText_CreateProjectButton.Enabled =
                    validationResultPlainText == ValidationResult.Success;

                // Create project UEFXML
                var validationResultUEFXML = _projectCreateInfoUEFXML.Validate();

                CreateProject_UEFXML_DictionaryFileTextBox.Text = _projectCreateInfoUEFXML.DictionaryPath;
                CreateProject_UEFXML_SynsetMappingsTextBox.Text = _projectCreateInfoUEFXML.SynsetMappingsPath;
                CreateProject_UEFXML_TrainDataTextBox.Text = _projectCreateInfoUEFXML.TrainDataPath;
                CreateProject_UEFXML_TestDataTextBox.Text = _projectCreateInfoUEFXML.TestDataPath;
                CreateProject_UEFXML_TrainGoldKeyTextBox.Text = _projectCreateInfoUEFXML.TrainGoldKeyPath;
                CreateProject_UEFXML_TestGoldKeyTextBox.Text = _projectCreateInfoUEFXML.TestGoldKeyPath;
                CreateProject_UEFXML_WordEmbeddingsTextBox.Text = _projectCreateInfoUEFXML.WordEmbeddingsPath;
                CreateProject_UEFXML_MeaningEmbeddingsTextBox.Text = _projectCreateInfoUEFXML.MeaningEmbeddingsPath;
                CreateProject_UEFXML_CreateProjectButton.Enabled = validationResultUEFXML == ValidationResult.Success;

                // Loaded plugins

                using (LoadedPlugins_ListView.UpdateScope())
                {
                    LoadedPlugins_ListView.Groups.Clear();
                    LoadedPlugins_ListView.Items.Clear();

                    LoadedPluginsBinder.BindToListView(_pluginInfos, LoadedPlugins_ListView);

                    LoadedPlugins_Status_Header.Width = -1;
                    LoadedPlugins_ObjectType_Header.Width = -2;
                    LoadedPlugins_DisplayName_Header.Width = -1;
                    LoadedPlugins_ClassType_Header.Width = -1;
                    LoadedPlugins_AssemblyName_Header.Width = -1;
                    LoadedPlugins_AssemblyPath_Header.Width = -1;
                }
            }
            finally
            {
                _refreshingUI = false;
            }
        }

        private void AddProjectTab(WsdProject project)
        {
            var tabPage = new TabPage
            {
                Text = project.ProjectInfo.ProjectName
            };

            MainTabControl.TabPages.Add(tabPage);

            var pluginComponents = _pluginInfos
                .Where(x => x.Loaded)
                .SelectMany(x => x.Components)
                .ToArray();

            var projectForm = new ProjectForm(pluginComponents, project, _progressFactory)
            {
                TopLevel = false
            };

            tabPage.Controls.Add(projectForm);

            projectForm.FormBorderStyle = FormBorderStyle.None;
            projectForm.Dock = DockStyle.Fill;
            projectForm.Show();

            MainTabControl.SelectedTab = tabPage;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (_pluginInfos != null)
                return;

            Task.Factory.StartNew(() =>
            {
                try
                {
                    using (var progress = _progressFactory.NewInstance("Loading plugins..."))
                    {
                        _pluginInfos = PluginLoader.LoadPlugins(progress);

                        this.InvokeIfRequired(RefreshUI);
                    }
                }
                catch (OperationCanceledException)
                {
                    this.InvokeIfRequired(RefreshUI);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error");
                }
            }, TaskCreationOptions.LongRunning);
        }

        private void MainTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshUI();
        }

        #region Create project PlainText

        private void CreateProject_SelectDictionary_Click(object sender, EventArgs e)
        {
            using (var dialog = DialogEx.OpenFile())
            {
                var result = dialog.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    _projectCreateInfoPlainText.DictionaryPath = dialog.FileName;

                    RefreshUI();
                }
            }
        }

        private void CreateProject_SelectTrainFolderButton_Click(object sender, EventArgs e)
        {
            using (var dialog = DialogEx.SelectFolder())
            {
                var result = dialog.ShowDialog();

                if (result == CommonFileDialogResult.Ok)
                {
                    _projectCreateInfoPlainText.TrainDataPath = dialog.FileName;

                    RefreshUI();
                }
            }
        }

        private void CreateProject_SelectTestFolderButton_Click(object sender, EventArgs e)
        {
            using (var dialog = DialogEx.SelectFolder())
            {
                var result = dialog.ShowDialog();

                if (result == CommonFileDialogResult.Ok)
                {
                    _projectCreateInfoPlainText.TestDataPath = dialog.FileName;

                    RefreshUI();
                }
            }
        }

        private void CreateProject_SelectWordEmbeddingsFileButton_Click(object sender, EventArgs e)
        {
            using (var dialog = DialogEx.OpenFile())
            {
                var result = dialog.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    _projectCreateInfoPlainText.WordEmbeddingsPath = dialog.FileName;

                    RefreshUI();
                }
            }
        }

        private void CreateProject_SelectMeaningEmbeddingsFileButton_Click(object sender, EventArgs e)
        {
            using (var dialog = DialogEx.OpenFile())
            {
                var result = dialog.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    _projectCreateInfoPlainText.MeaningEmbeddingsPath = dialog.FileName;

                    RefreshUI();
                }
            }
        }

        private void CreateProject_CreateProjectButton_Click(object sender, EventArgs e)
        {
            using (var dialog = DialogEx.SelectFolder())
            {
                var result = dialog.ShowDialog();

                if (result == CommonFileDialogResult.Ok)
                {
                    var projectPath = dialog.FileName;

                    if (Directory.GetFiles(projectPath, "*", SearchOption.AllDirectories).Length > 0)
                    {
                        MessageBox.Show("Directory must be empty.", "Error");

                        return;
                    }

                    Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            using (var progress = _progressFactory.NewInstance("Creating and saving project..."))
                            {
                                var project = WsdProject.CreateAndSave(
                                    _projectCreateInfoPlainText, projectPath, progress);

                                this.InvokeIfRequired(() =>
                                {
                                    RefreshUI();
                                    AddProjectTab(project);
                                });
                            }
                        }
                        catch (OperationCanceledException)
                        {
                            this.InvokeIfRequired(RefreshUI);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString(), "Error");
                        }
                    }, TaskCreationOptions.LongRunning);
                }
            }
        }

        #endregion Create project PlainText

        #region Create project UEFXML
        
        private void CreateProject_UEFXML_SelectDictionary_Click(object sender, EventArgs e)
        {
            using (var dialog = DialogEx.OpenFile())
            {
                var result = dialog.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    _projectCreateInfoUEFXML.DictionaryPath = dialog.FileName;

                    RefreshUI();
                }
            }
        }

        private void CreateProject_UEFXML_SelectSynsetMappings_Click(object sender, EventArgs e)
        {
            using (var dialog = DialogEx.OpenFile())
            {
                var result = dialog.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    _projectCreateInfoUEFXML.SynsetMappingsPath= dialog.FileName;

                    RefreshUI();
                }
            }
        }

        private void CreateProject_UEFXML_SelectTrainData_Click(object sender, EventArgs e)
        {
            using (var dialog = DialogEx.OpenFile())
            {
                var result = dialog.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    _projectCreateInfoUEFXML.TrainDataPath = dialog.FileName;

                    var index = dialog.FileName.IndexOf("data.xml", StringComparison.Ordinal);

                    if (index > 0)
                    {
                        var goldKeyFileName = dialog.FileName.Substring(0, index) + "gold.key.txt";

                        if (File.Exists(goldKeyFileName))
                        {
                            _projectCreateInfoUEFXML.TrainGoldKeyPath = goldKeyFileName;
                        }
                    }

                    RefreshUI();
                }
            }
        }

        private void CreateProject_UEFXML_SelectTestData_Click(object sender, EventArgs e)
        {
            using (var dialog = DialogEx.OpenFile())
            {
                var result = dialog.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    _projectCreateInfoUEFXML.TestDataPath = dialog.FileName;

                    var index = dialog.FileName.IndexOf("data.xml", StringComparison.Ordinal);

                    if (index > 0)
                    {
                        var goldKeyFileName = dialog.FileName.Substring(0, index) + "gold.key.txt";

                        if (File.Exists(goldKeyFileName))
                        {
                            _projectCreateInfoUEFXML.TestGoldKeyPath = goldKeyFileName;
                        }
                    }

                    RefreshUI();
                }
            }
        }

        private void CreateProject_UEFXML_SelectTrainGoldKey_Click(object sender, EventArgs e)
        {
            using (var dialog = DialogEx.OpenFile())
            {
                var result = dialog.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    _projectCreateInfoUEFXML.TrainGoldKeyPath = dialog.FileName;

                    RefreshUI();
                }
            }
        }

        private void CreateProject_UEFXML_SelectTestGoldKey_Click(object sender, EventArgs e)
        {
            using (var dialog = DialogEx.OpenFile())
            {
                var result = dialog.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    _projectCreateInfoUEFXML.TestGoldKeyPath = dialog.FileName;

                    RefreshUI();
                }
            }
        }

        private void CreateProject_UEFXML_SelectWordEmbeddings_Click(object sender, EventArgs e)
        {
            using (var dialog = DialogEx.OpenFile())
            {
                var result = dialog.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    _projectCreateInfoUEFXML.WordEmbeddingsPath = dialog.FileName;

                    RefreshUI();
                }
            }
        }

        private void CreateProject_UEFXML_SelectMeaningEmbeddings_Click(object sender, EventArgs e)
        {
            using (var dialog = DialogEx.OpenFile())
            {
                var result = dialog.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    _projectCreateInfoUEFXML.MeaningEmbeddingsPath = dialog.FileName;

                    RefreshUI();
                }
            }
        }

        private void CreateProject_UEFXML_CreateProjectButton_Click(object sender, EventArgs e)
        {
            using (var dialog = DialogEx.SelectFolder())
            {
                var result = dialog.ShowDialog();

                if (result == CommonFileDialogResult.Ok)
                {
                    var projectPath = dialog.FileName;

                    if (Directory.GetFiles(projectPath, "*", SearchOption.AllDirectories).Length > 0)
                    {
                        MessageBox.Show("Directory must be empty.", "Error");

                        return;
                    }

                    Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            using (var progress = _progressFactory.NewInstance("Creating and saving project..."))
                            {
                                var project = WsdProject.CreateAndSave(
                                    _projectCreateInfoUEFXML, projectPath, progress);

                                this.InvokeIfRequired(() =>
                                {
                                    RefreshUI();
                                    AddProjectTab(project);
                                });
                            }
                        }
                        catch (OperationCanceledException)
                        {
                            this.InvokeIfRequired(RefreshUI);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString(), "Error");
                        }
                    }, TaskCreationOptions.LongRunning);
                }
            }
        }

        #endregion Create project UEFXML

        #region Load project

        private void OpenProjectButton_Click(object sender, EventArgs e)
        {
            using (var dialog = DialogEx.OpenFile("WsdProject files (*.wsdproj)|*.wsdproj"))
            {
                var result = dialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    var projectFile = dialog.FileName;

                    Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            using (var progress = _progressFactory.NewInstance("Loading project..."))
                            {
                                var project = WsdProject.Load(projectFile, progress);

                                this.InvokeIfRequired(() =>
                                {
                                    RefreshUI();
                                    AddProjectTab(project);
                                });
                            }
                        }
                        catch (OperationCanceledException)
                        {
                            this.InvokeIfRequired(RefreshUI);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString(), "Error");
                        }
                    }, TaskCreationOptions.LongRunning);
                }
            }
        }

        private void CloseProjectButton_Click(object sender, EventArgs e)
        {
            if (MainTabControl.SelectedIndex > 0)
            {
                var tab = MainTabControl.SelectedTab;

                MainTabControl.TabPages.Remove(tab);
                tab.Dispose();
            }

            RefreshUI();
        }

        #endregion Load project
    }
}