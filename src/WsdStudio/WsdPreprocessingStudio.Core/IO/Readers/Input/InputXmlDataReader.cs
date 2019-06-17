using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using WsdPreprocessingStudio.Core.Data;
using WsdPreprocessingStudio.Core.Data.Collections;
using WsdPreprocessingStudio.Core.Data.Serialization;
using WsdPreprocessingStudio.Core.Extensions;
using WsdPreprocessingStudio.Core.Helpers;
using WsdPreprocessingStudio.Core.Threading;

namespace WsdPreprocessingStudio.Core.IO.Readers.Input
{
    public static class InputXmlDataReader
    {
        public static TextData[] Read(
            string dataPath, string goldKeyPath, SynsetDictionary synsetMappings,
            WordDictionary dictionary, IProgressHandle progress = null)
        {
            var scope = progress?.Scope(1);

            try
            {
                var result = new List<TextData>();
                var serializer = new XmlSerializer(typeof(UefXmlData));

                using (var reader = new StreamReader(dataPath))
                {
                    var goldKeys = File.ReadAllLines(goldKeyPath)
                        .Where(x => !string.IsNullOrWhiteSpace(x))
                        .Select(x => x.Split(' '))
                        .Where(x => x.Length > 1)
                        .DistinctBy(x => x[0])
                        .ToDictionary(x => x[0], x => string.Join(" ", x.Skip(1)));

                    var dataXml = (UefXmlData)serializer.Deserialize(reader);

                    foreach (var text in dataXml.Texts)
                    {
                        var encounters = new List<RawWordEncounter>();

                        foreach (var sentence in text.Sentences)
                        {
                            foreach (var encounter in sentence.Encounters)
                            {
                                encounters.Add(new RawWordEncounter
                                {
                                    Word = encounter.Lemma,
                                    Pos = encounter.Pos,
                                    Meaning = SynsetHelper.GetMeaning(
                                        dictionary, goldKeys, synsetMappings, 
                                        encounter.Lemma, encounter.SynsetId)
                                });
                            }

                            encounters.Add(RawWordEncounter.EndOfSentenceEncounter);
                        }

                        result.Add(new TextData(text.Id, encounters.ToArray()));
                    }
                }

                return result.ToArray();
            }
            finally
            {
                scope?.Dispose();
            }
        }
    }
}