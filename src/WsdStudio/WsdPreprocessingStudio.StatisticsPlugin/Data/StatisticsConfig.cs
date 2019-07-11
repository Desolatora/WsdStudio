namespace WsdPreprocessingStudio.StatisticsPlugin.Data
{
    public class StatisticsConfig
    {
        public bool PluginEnabled { get; set; }
        public int HandlerExecutionPriority { get; set; }
        public bool RequireTrainingSet { get; set; }
        public bool RequireTestSet { get; set; }
        public int MinimumTrainingValidationExamples { get; set; }
        public bool AbortGenerationAfterStatisticsAreComputed { get; set; }
    }
}
