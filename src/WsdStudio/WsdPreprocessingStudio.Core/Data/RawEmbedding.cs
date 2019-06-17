namespace WsdPreprocessingStudio.Core.Data
{
    public class RawEmbedding
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => _name = string.Intern(value);
        }

        public float[] Vector { get; set; }
    }
}