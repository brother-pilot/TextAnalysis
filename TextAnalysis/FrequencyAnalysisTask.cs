using System.Collections.Generic;
using System;
using System.Linq;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = new Dictionary<string, string>(16384);
            //List<List<string>> grammCount = new List<List<string>>(65536);
            List<string> grammCount = new List<string>(65536);
            var resultCount = new Dictionary<string, string>(16384);
            List<string> biGramm = new List<string>(16384);
            //List<List<string>> biGramm = new List<List<string>>(65536);
            //работаем с биграммами
            FindAnyGramm(text, biGramm,2);
            CountGramm(biGramm, grammCount);
            CompareGramm(result, resultCount, grammCount, 2);
            //работаем с триграммами
            List<string> triGramm = new List<string>(16384);
            //if (biGramm.Count>400)
                //Console.WriteLine();
            grammCount = new List<string>(65536);
            FindAnyGramm(text, triGramm, 3);
            CountGramm(triGramm, grammCount);
            CompareGramm(result, resultCount, grammCount, 3);
            return result;
        }

        public static Dictionary<string, string> CompareGramm(Dictionary<string, string> result, Dictionary<string, string> resultCount, List<string> grammCount,int typeGramm)
        {
            int l = 0;
            //чтобы исключить лишний перебор считаем на втором шаге с того шаге где остановились во внутр цикле
            for (var i = 0; i < grammCount.Count; i = l)
            {
                string[] words = grammCount[i].Split();
                //if (i>12754 && typeGramm == 2) 
                //    Console.WriteLine();
                string secondWord;
                string firstWord;
                if (words.Length == 3 && typeGramm == 2)
                {
                    firstWord = words[1];
                    secondWord = words[2];
                }
                else if (words.Length == 4 && typeGramm == 3)
                {
                    firstWord = words[1] + " " + words[2];
                    secondWord = words[3];
                }
                else
                {
                    i++;
                    continue;
                }
                //добавляем в словарь грамму
                if (!result.ContainsKey(firstWord))
                {
                    result.Add(firstWord, secondWord);
                    resultCount.Add(firstWord, words[0]);
                }
                else
                {
                    secondWord = result[firstWord];
                    words[0] = resultCount[firstWord];
                    i--;
                }
                for (l = i+1; l < grammCount.Count; l++)
                {
                    string[] wordsOther = grammCount[l].Split();
                    var fff = wordsOther[0];
                    string secondWordOther;
                    string firstWordOther;
                    if (words.Length == 3 && typeGramm == 2)
                    {
                        firstWordOther = wordsOther[1];
                        secondWordOther = wordsOther[2];
                    }
                    else if (words.Length == 4 && typeGramm == 3)
                    {
                        firstWordOther = wordsOther[1] + " " + wordsOther[2];
                        secondWordOther = wordsOther[3];
                    }
                    else
                    {
                        l++;
                        continue;
                    }
                    if (firstWord == firstWordOther)
                    {
                        //проверяем какая грамма встречается чаще. что самая первая или выбранная
                        if (int.Parse(words[0]) < int.Parse(wordsOther[0]))
                        {
                            ReplaceResult(firstWordOther, result, resultCount, secondWordOther, wordsOther[0]);
                            words[0] = wordsOther[0];
                        }
                        else if (int.Parse(words[0]) == int.Parse(wordsOther[0]))
                        {
                            //проверяем какая лексически короче
                            if (String.CompareOrdinal(firstWord + " " + result[firstWord], firstWordOther + " " + secondWordOther) > 0)
                            {
                                ReplaceResult(firstWordOther, result, resultCount, secondWordOther, wordsOther[0]);
                            }
                        }
                    }
                    else break;
                }
            }
            return result;
        }

        public static void ReplaceResult(string firstWord, Dictionary<string, string> result, Dictionary<string, string> resultCount, string lastWord,string countGramm)
        {
            result.Remove(firstWord);
            result.Add(firstWord, lastWord);
            resultCount.Remove(firstWord);
            resultCount.Add(firstWord, countGramm);
        }

        public static List<string> CountGramm(List<string> gramm, List<string> grammCount)
        {
            int l = 0;
            for (var k = 0; k < gramm.Count; k = l)
            {
                int count = 0;
                for (l = k; l < gramm.Count; l++)
                {
                    if (gramm[k] == gramm[l]) count++;
                    else break;
                }
                grammCount.Add(count + " " + gramm[k]);
                //    var bbb = biGramm[k].Split();
                //grammCount.Add();
                //count.ToString() (count.ToString()+" "+ biGramm[k]).Split()
            }
            return grammCount;
        }
        
        public static List<string> FindAnyGramm(List<List<string>> text, List<string> gramm, int typeGramm)
        //public static List<List<string>> FindAnyGramm(List<List<string>> text, List<List<string>> gramm, int typeGramm)
        {
            //ищем все возможные в тексте граммы 
            for (var i = 0; i < text.Count; i++)
            {
                if (typeGramm == 2)
                    for (var j = 0; j < text[i].Count - 1; j++)
                    {
                        //добавляем биграммы 
                        gramm.Add(text[i][j] + " " + text[i][j + 1]);
                        //gramm.Add(new List<string> { text[i][j], text[i][j + 1]});
                    }
                if (typeGramm == 3)
                    for (var j = 0; j < text[i].Count - 2; j++)
                    {
                        //добавляем триграммы
                        gramm.Add(text[i][j] + " " + text[i][j + 1] + " " + text[i][j + 2]);
                        //gramm.Add(new List<string> { text[i][j], text[i][j + 1], text[i][j + 2]});
                    }
            }
            //сортируем элементы //при реализации через лист здесь не сортирует. нужно делать это сложнее. потом вернуться
            gramm.Sort();
            //if (gramm.Count>400)
            //Console.WriteLine();
            return gramm;
        }
    }
}