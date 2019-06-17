namespace WsdPreprocessingStudio.Core.Plugins
{
    public interface IPlugin
    {
        string DisplayName { get; }
        
        IPluginComponent[] GetComponents();
    }
}
