using System.Collections.Generic;
using System.Linq;
using WsdPreprocessingStudio.Core;
using WsdPreprocessingStudio.Core.Data;
using WsdPreprocessingStudio.Core.Extensions;
using WsdPreprocessingStudio.DataGeneration.Data;

namespace WsdPreprocessingStudio.DataGeneration.Algorithms
{
    public class GenerationAlgorithm
    {
        public IList<GeneratedTextData> GenerateRecords(
            IList<TextData> data, WsdProject project, GenerationInfo info)
        {
            return data
                .Select(x => new GeneratedTextData(x.TextName, GenerateRecords(x.Data, project, info)))
                .ToArray();
        }

        private IList<RawRecord> GenerateRecords(
            IList<RawWordEncounter> input, WsdProject project, GenerationInfo info)
        {
            var contextWindowLength = info.LeftContext + 1 + info.RightContext;
            var wordIndexInContext = info.LeftContext;
            var contextWindow = new RawWordEncounter[contextWindowLength];
            var records = new List<RawRecord>();

            contextWindow.ShiftLeft(new RawWordEncounter
            {
                Word = RawWordEncounter.EndOfSentence
            });

            using (var enumerator = input.GetEnumerator())
            {
                bool moveNext;

                do
                {
                    moveNext = enumerator.MoveNext();

                    if (moveNext)
                    {
                        if (!string.IsNullOrEmpty(enumerator.Current.Pos) &&
                            !info.FilteredPosList.Contains(enumerator.Current.Pos))
                            continue;

                        contextWindow.ShiftLeft(enumerator.Current);
                    }
                    else
                    {
                        contextWindow.ShiftLeft();
                    }

                    var currentEncounter = contextWindow[wordIndexInContext];

                    if (currentEncounter == null ||
                        currentEncounter.Word == RawWordEncounter.EmptyWord ||
                        currentEncounter.Word == RawWordEncounter.EndOfSentence ||
                        string.IsNullOrWhiteSpace(currentEncounter.Meaning))
                        continue;

                    var dictionaryWord = project.Dictionary.GetByName(currentEncounter.Word);

                    if (dictionaryWord == null || dictionaryWord.Meanings.Count <= 1)
                        continue;

                    var context = new RawWordEncounter[contextWindowLength - 1];

                    if (info.Overlap)
                    {
                        for (var i = 0; i < contextWindowLength; i++)
                        {
                            if (i == wordIndexInContext)
                                continue;

                            var indexInBuffer = i < wordIndexInContext ? i : i - 1;

                            context[indexInBuffer] = contextWindow[i] ?? RawWordEncounter.EmptyWordEncounter;
                        }
                    }
                    else
                    {
                        var endOfSentence = false;

                        for (var i = wordIndexInContext - 1; i >= 0; i--)
                        {
                            context[i] = endOfSentence
                                ? RawWordEncounter.EmptyWordEncounter
                                : contextWindow[i] ?? RawWordEncounter.EmptyWordEncounter;

                            if (contextWindow[i]?.Word == RawWordEncounter.EndOfSentence)
                                endOfSentence = true;
                        }

                        endOfSentence = false;

                        for (var i = wordIndexInContext + 1; i < contextWindowLength; i++)
                        {
                            context[i - 1] = endOfSentence
                                ? RawWordEncounter.EmptyWordEncounter
                                : contextWindow[i] ?? RawWordEncounter.EmptyWordEncounter;

                            if (contextWindow[i]?.Word == RawWordEncounter.EndOfSentence)
                                endOfSentence = true;
                        }
                    }

                    records.Add(new RawRecord
                    {
                        Word = currentEncounter.Word,
                        Meaning = currentEncounter.Meaning,
                        Pos = currentEncounter.Pos,
                        Context = context
                    });
                } while (moveNext || !contextWindow.IsEmpty());
            }

            return records;
        }
    }
}
