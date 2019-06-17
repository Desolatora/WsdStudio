using WsdPreprocessingStudio.Core.Data;

namespace WsdPreprocessingStudio.DataGeneration.Data
{
    public class RawRecord : RawWordEncounter
    {
        public RawWordEncounter[] Context { get; set; }
        
        public RawRecord()
        {
            Context = new RawWordEncounter[0];
        }
    }
}