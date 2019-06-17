using System.Reflection;

namespace WsdPreprocessingStudio.Core.Plugins
{
    public class PluginInfo
    {
        public bool Loaded { get; }
        public string AssemblyPath { get; }
        public AssemblyName AssemblyName { get; }
        public IPlugin Plugin { get; }
        public IPluginComponent[] Components { get; }

        public PluginInfo(
            bool loaded, string assemblyPath, AssemblyName assemblyName,
            IPlugin plugin, IPluginComponent[] components)
        {
            Loaded = loaded;
            AssemblyPath = assemblyPath;
            AssemblyName = assemblyName;
            Plugin = plugin;
            Components = components;
        }
    }
}
