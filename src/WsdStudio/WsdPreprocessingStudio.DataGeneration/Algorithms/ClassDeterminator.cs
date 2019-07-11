using System;
using System.Collections.Generic;
using System.Linq;
using WsdPreprocessingStudio.Core;
using WsdPreprocessingStudio.Core.Data;
using WsdPreprocessingStudio.Core.Data.Collections;
using WsdPreprocessingStudio.Core.Threading;
using WsdPreprocessingStudio.DataGeneration.Data;
using WsdPreprocessingStudio.DataGeneration.Resources;

namespace WsdPreprocessingStudio.DataGeneration.Algorithms
{
    public class ClassDeterminator
    {
        public WordDictionary GetReorderedDictionary(
            WsdProject project, GenerationInfo info, IProgressHandle progress)
        {
            using (progress.Scope(1, MessageFormat.ReorderingDictionary))
            {
                Func<IEnumerable<DictionaryMeaning>, string, IEnumerable<DictionaryMeaning>> meaningOrderFunc =
                    (enumerable, word) =>
                    {
                        if (info.OrderMeanings == OrderMeanings.ByDictionary ||
                            info.OrderMeanings == OrderMeanings.ByTrainingSet)
                            enumerable = enumerable
                                .OrderByDescending(z => z.Encounters);
                        else if (info.OrderMeanings == OrderMeanings.ByDictionaryAndTrainingSet)
                            enumerable = enumerable
                                .OrderByDescending(z => z.Encounters)
                                .ThenByDescending(z => project.DataAnalysis.GetByName(word)?
                                                           .TrainEncounters
                                                           .GetByName(z.Meaning)?.Encounters ?? 0);

                        return enumerable.Select((z, i) => new DictionaryMeaning
                        {
                            Id = i + 1,
                            Meaning = z.Meaning,
                            PartOfSpeech = z.PartOfSpeech,
                            Encounters = z.Encounters
                        });
                    };

                var result = (info.OrderMeanings == OrderMeanings.ByTrainingSet
                        ? project.DataAnalysis
                            .Values
                            .Where(x => x.TrainEncounters.Any())
                            .Select(x => new DictionaryWord
                            {
                                Id = project.Dictionary[x.Word].Id,
                                Word = x.Word,
                                Meanings = x.TrainEncounters.Values
                                    .OrderByDescending(y => y.Encounters)
                                    .Select((y, i) => new DictionaryMeaning
                                    {
                                        Id = i + 1,
                                        Meaning = y.Meaning,
                                        PartOfSpeech = y.PartOfSpeech,
                                        Encounters = y.Encounters
                                    }).ToMeaningDictionary()
                            })
                        : project.Dictionary.Values)
                    .Select(x =>
                    {
                        var meanings = (IEnumerable<DictionaryMeaning>) x.Meanings.Values;

                        if (info.OrderMeaningsStrategy == OrderMeaningsStrategy.GroupByWordAndPos)
                        {
                            meanings = meanings
                                .GroupBy(y => y.PartOfSpeech)
                                .SelectMany(y => meaningOrderFunc.Invoke(y, x.Word));
                        }
                        else
                        {
                            meanings = meaningOrderFunc.Invoke(meanings, x.Word);
                        }

                        return new DictionaryWord
                        {
                            Id = x.Id,
                            Word = x.Word,
                            Meanings = meanings.ToMeaningDictionary()
                        };
                    })
                    .Where(x => x.Meanings.Count > 0)
                    .ToWordDictionary();

                return result;
            }
        }
    }
}
