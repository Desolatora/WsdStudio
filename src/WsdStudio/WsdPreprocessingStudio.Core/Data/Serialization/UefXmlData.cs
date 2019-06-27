using System.Collections.Generic;
using System.Xml.Serialization;

namespace WsdPreprocessingStudio.Core.Data.Serialization
{
    [XmlRoot("corpus")]
    public class UefXmlData
    {
        [XmlElement("text")]
        public List<UefXmlText> Texts { get; set; }
    }
    
    public class UefXmlText
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("sentence")]
        public UefXmlSentence[] Sentences { get; set; }

        public override string ToString()
        {
            return $"id={Id}";
        }
    }
    
    public class UefXmlSentence
    {
        [XmlIgnore]
        public ItemChoiceType[] EnumTypes { get; set; }

        [XmlChoiceIdentifier("EnumTypes")]
        [XmlElement("wf")]
        [XmlElement("instance")]
        public UefXmlEncounter[] Encounters { get; set; }
    }

    [XmlType(IncludeInSchema = false)]
    public enum ItemChoiceType
    {
        wf,
        instance
    }

    public class UefXmlEncounter
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("lemma")]
        public string Lemma { get; set; }

        [XmlAttribute("pos")]
        public string Pos { get; set; }

        public override string ToString()
        {
            return $"id={Id} lemma={Lemma} pos={Pos}";
        }
    }
}
