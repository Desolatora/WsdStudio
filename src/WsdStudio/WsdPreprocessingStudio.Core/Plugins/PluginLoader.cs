using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WsdPreprocessingStudio.Core.Resources;
using WsdPreprocessingStudio.Core.Threading;

namespace WsdPreprocessingStudio.Core.Plugins
{
    public class PluginLoader
    {
        public static readonly string PluginDirectory;

        static PluginLoader()
        {
            var exeDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            PluginDirectory = Path.Combine(exeDirectory, "Plugins");

            Directory.CreateDirectory(PluginDirectory);
            AppDomain.CurrentDomain.AssemblyResolve += ResolvePluginDependencies;
        }

        public static PluginInfo[] LoadPlugins(IProgressHandle progress)
        {
            var plugins = new List<PluginInfo>();
            var assemblyFiles = Directory.GetFiles(PluginDirectory, "*.dll", SearchOption.AllDirectories);

            using (var scope = progress.Scope(assemblyFiles.Length, MessageFormat.LoadingPlugins_Files))
            {
                var counter = 0;

                foreach (var assemblyFile in assemblyFiles)
                {
                    try
                    {
                        var assembly = Assembly.LoadFile(assemblyFile);
                        var pluginTypes = assembly.GetTypes()
                            .Where(x => typeof(IPlugin).IsAssignableFrom(x))
                            .ToArray();

                        foreach (var pluginType in pluginTypes)
                        {
                            try
                            {
                                var pluginInstance = (IPlugin) Activator.CreateInstance(pluginType);
                                var pluginComponents = pluginInstance.GetComponents() ?? new IPluginComponent[0];

                                plugins.Add(new PluginInfo(
                                    true, assemblyFile, assembly.GetName(),
                                    pluginInstance, pluginComponents));
                            }
                            catch
                            {
                                plugins.Add(new PluginInfo(
                                    true, assemblyFile, assembly.GetName(), null, null));
                            }
                        }
                    }
                    catch
                    {
                    }

                    scope.TrySet(++counter);
                }
            }

            return plugins.ToArray();
        }

        private static Assembly ResolvePluginDependencies(object sender, ResolveEventArgs args)
        {
            var originalAssemblyDirectory = Path.GetDirectoryName(args.RequestingAssembly.Location);
            var otherAssemblies = new DirectoryInfo(originalAssemblyDirectory)
                .GetFiles("*.dll", SearchOption.AllDirectories);
            var assembly = otherAssemblies.FirstOrDefault(x => x.Name == args.Name);

            return assembly != null
                ? Assembly.LoadFile(assembly.FullName)
                : null;
        }
    }
}
