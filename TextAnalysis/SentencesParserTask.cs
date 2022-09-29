using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        //это можно было бы применить если при поиске символов разделителей использовался метод, но не стал
        public static readonly char[] EndOfSentenceChars =
        {
            '.', '!', '?', ';', ':', '(', ')'
        };
        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            List<string> sentences = new List<string>();
            sentences = DevideToSentences(text,sentences);
            foreach (var sentence in sentences)
            {
                if (sentence.Length != 0)
                {
                    //разбиваем предложения на слова
                    List<string> words = new List<string>();
                    foreach (var word in sentence.Split())
                        if (word.Length != 0)
                        {
                            words.Add(word);
                        }
                    sentencesList.Add(words);
                }
            }
            return sentencesList;   
        }

        public static List<string> DevideToSentences(string text, List<string> sentences)
        {
            var builder = new StringBuilder();
            for (int j = 0; j < text.Length; j++)
            {
                //сначала разбиваем на предложения с использованием этих символов разделителей
                // if (text[j] != '.' && text[j] != '!' && text[j] != '?' && text[j] != ';' && text[j] != ':' && text[j] != '(' && text[j] != ')')
                if (!".!?;:()".Contains(text[j].ToString()))
                {
                    //если этот символ буква или апостроф то записываем иначе ставим пробел
                    if (char.IsLetter(text[j]) || text[j] == '\'') builder.Append(text[j]);
                    else builder.Append(' ');
                }
                else
                {
                    //переводим в строку, потом в нижний регистр
                    sentences.Add(builder.ToString().ToLower().Trim());
                    //чистим записанные символы т.е. предложение
                    builder.Remove(0, builder.Length);
                }
            }
            //для случаев когда предложения заканчиваются не на знак разделитель
            if (sentences.Count == 0 || builder.Length != 0)
                    sentences.Add(builder.ToString().ToLower().Trim());
            return sentences;
        }
    }
}


