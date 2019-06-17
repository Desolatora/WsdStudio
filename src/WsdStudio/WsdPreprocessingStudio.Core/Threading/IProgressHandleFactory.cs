namespace WsdPreprocessingStudio.Core.Threading
{
    public interface IProgressHandleFactory
    {
        IProgressHandle NewInstance(string title);
    }
}
