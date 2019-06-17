using System;
using System.Reflection;
using System.Windows.Forms;
using WsdPreprocessingStudio.Core.Plugins;
using WsdPreprocessingStudio.Core.UI;

namespace WsdPreprocessingStudio.Binders
{
    public class LoadedPluginsBinder
    {
        public static void BindToListView(PluginInfo[] plugins, ListView control)
        {
            if (plugins == null)
                return;

            foreach (var plugin in plugins)
            {
                var assemblyName = $"{plugin.AssemblyName.Name}, Version={plugin.AssemblyName.Version}";
                var appDirectoryUri = new Uri(Assembly.GetExecutingAssembly().Location);
                var assemblyPathUri = new Uri(plugin.AssemblyPath);
                var relativeAssemblyPathUri = appDirectoryUri.MakeRelativeUri(assemblyPathUri);
                var assemblyPath = relativeAssemblyPathUri.ToString();

                if (plugin.Loaded)
                {
                    control.AddItem(
                        "Loaded", "Plugin", plugin.Plugin.DisplayName,
                        plugin.Plugin.GetType().FullName, assemblyName, assemblyPath);

                    foreach (var component in plugin.Components)
                    {
                        control.AddItem(
                            "Loaded", "Component", component.DisplayName,
                            component.GetType().FullName, assemblyName, assemblyPath);
                    }
                }
                else
                {
                    control.AddItem(
                        "Failed", "Plugin", "-", "-", assemblyName, assemblyPath);
                }
            }
        }
    }
}
