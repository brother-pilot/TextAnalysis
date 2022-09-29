using System.Collections.Generic;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(
            Dictionary<string, string> nextWords,
            string phraseBeginning,
            int wordsCount)
        {
            for (int i = 0; i < wordsCount; i++)
            {
                string[] words = phraseBeginning.Split();
                if (words.Length >= 2 && nextWords.ContainsKey(words[words.Length - 2] + " " + words[words.Length - 1]))
                    phraseBeginning = phraseBeginning + " " + nextWords[words[words.Length - 2] + " " + words[words.Length - 1]];
                else if (nextWords.ContainsKey(words[words.Length - 1]))
                    phraseBeginning = phraseBeginning + " " + nextWords[words[words.Length - 1]];
            }
            return phraseBeginning;
        }
    }
}