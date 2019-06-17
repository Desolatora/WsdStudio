namespace WsdPreprocessingStudio.Core.Data
{
    public class WsdProjectInfo
    {
        public string ProjectName { get; set; }
        public string ProjectVersion { get; set; }
        public string ApplicationVersion { get; set; }

        public string Dictionary { get; set; }
        public WsdProjectTextDataInfo[] TrainData { get; set; }
        public WsdProjectTextDataInfo[] TestData { get; set; }
        public string WordEmbeddings { get; set; }
        public string MeaningEmbeddings { get; set; }
        public string DataAnalysis { get; set; }
        public string DictionaryStatistics { get; set; }
        public string DataStatistics { get; set; }
        public string WordEmbeddingsStatistics { get; set; }
        public string MeaningEmbeddingsStatistics { get; set; }
    }

    public class WsdProjectTextDataInfo
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }
}