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
            WordDictionary dictionary, out XmlParseError[] errors,
            IProgressHandle progress = null)
        {
            var scope = progress?.Scope(1);

            try
            {
                var result = new List<TextData>();
                var serializer = new XmlSerializer(typeof(UefXmlData));
                var xmlParseErrors = new List<XmlParseError>();

                using (var reader = new StreamReader(dataPath))
                {
                    var goldKeys = File.ReadAllLines(goldKeyPath)
                        .Where(x => !string.IsNullOrWhiteSpace(x))
                        .Select(x => x.Trim(' ').Split(' '))
                        .Where(x => x.Length > 1)
                        .DistinctBy(x => x[0])
                        .ToDictionary(x => x[0], x => string.Join(" ", x.Skip(1)));

                    var dataXml = (UefXmlData)serializer.Deserialize(reader);

                    foreach (var text in dataXml.Texts)
                    {
                        var encounters = new List<RawWordEncounter>();

                        foreach (var sentence in text.Sentences)
                        {
                            for (var i = 0; i < sentence.Encounters.Length; i++)
                            {
                                var encounter = sentence.Encounters[i];
                                var encounterType = sentence.EnumTypes[i];
                                var rawWordEncounter = new RawWordEncounter
                                {
                                    Word = encounter.Lemma,
                                    Pos = encounter.Pos,
                                    Meaning = string.Empty
                                };

                                if (encounterType == ItemChoiceType.instance)
                                {
                                    var status = SynsetHelper.TryGetMeaning(
                                        dictionary, goldKeys, synsetMappings,
                                        encounter.Lemma, encounter.Id, out var meaning);

                                    if (status == TryGetMeaningStatus.OK)
                                    {
                                        rawWordEncounter.Meaning = meaning;
                                    }
                                    else
                                    {
                                        xmlParseErrors.Add(new XmlParseError
                                        {
                                            EncounterId = encounter.Id,
                                            Error = status
                                        });
                                    }
                                }

                                encounters.Add(rawWordEncounter);
                            }

                            encounters.Add(RawWordEncounter.EndOfSentenceEncounter);
                        }

                        result.Add(new TextData(text.Id, encounters.ToArray()));
                    }
                }

                errors = xmlParseErrors.ToArray();

                return result.ToArray();
            }
            finally
            {
                scope?.Dispose();
            }
        }
    }
}