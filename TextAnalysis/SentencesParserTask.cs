using System.Collections.Generic;
using System.Text;
/*
 В этом задании нужно реализовать метод в классе SentencesParserTask. Метод должен делать следующее:
1.	Разделять текст на предложения, а предложения на слова.
a. Считайте, что слова состоят только из букв (используйте метод char.IsLetter) или символа апострофа ' и отделены друг от друга любыми другими символами.
b. Предложения состоят из слов и отделены друг от друга одним из следующих символов .!?;:()
2.	Приводить символы каждого слова в нижний регистр.
3.	Пропускать предложения, в которых не оказалось слов.
Метод должен возвращать список предложений, где каждое предложение — это список из одного или более слов в нижнем регистре.
*/

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


