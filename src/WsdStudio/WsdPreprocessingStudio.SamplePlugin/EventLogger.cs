namespace WsdPreprocessingStudio.SamplePlugin
{
    public class EventLogger
    {
        public delegate void OnMessageLoggedHandler(string message);

        public event OnMessageLoggedHandler OnMessageLogged;

        public void LogMessage(string message)
        {
            OnMessageLogged?.Invoke(message);
        }
    }
}
