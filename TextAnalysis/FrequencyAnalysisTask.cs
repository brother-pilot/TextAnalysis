using System.Collections.Generic;
using System;
using System.Linq;
/*
 * N-грамма — это N соседних слов в одном предложении. 2-граммы называют биграммами. 3-граммы — триграммами.
Например, из текста: "She stood up. Then she left." можно выделить следующие биграммы "she stood", "stood up", "then she" и "she left", но не "up then". И две триграммы "she stood up" и "then she left", но не "stood up then".
По списку предложений, составленному в прошлой задаче, составьте словарь самых частотных продолжений биграмм и триграмм. Это словарь, ключами которого являются все возможные начала биграмм и триграмм, а значениями — их самые частотные продолжения.
Более формально так:
Для каждой пары (key, value) из словаря должно выполняться одно из следующих условий:
1.	В тексте есть хотя бы одна биграмма (key, value), и для любой другой присутствующей в тексте биграммы (key, otherValue), начинающейся с того же слова, value должен быть лексикографически меньше otherValue.
2.	Либо в тексте есть хотя бы одна триграмма (w1, w2, value), такая что w1 + " " + w2 == key и для любой другой присутствующей в тексте триграммы (w1, w2, otherValue), начинающейся с той же пары слов, value должен быть лексикографически меньше otherValue.
Для лексикографического сравнения используйте встроенный в .NET способ сравнения Ordinal, например с помощью метода string.CompareOrdinal.
Такой словарь назовём N-граммной моделью текста.
Реализуйте этот алгоритм в классе FrequencyAnalysisTask.
Все вопросы и детали уточняйте с помощью примера ниже и тестов.
Пример
По тексту a b c d. b c d. e b c a d. должен быть составлен такой словарь:
Обратите внимание:
•	из двух биграмм "a b" и "a d", встречающихся однократно, в словаре есть только пара "a": "b", как лексикографически меньшая.
•	из двух встречающихся в тексте биграмм "c d" и "c a" в словаре есть только более частотная пара "c": "d".
•	из двух триграмм "b c d" и "b c a" в словаре есть только более частотная "b c": "d".
*/

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = new Dictionary<string, string>(16384);
            List<string> grammCount = new List<string>(65536);
            var resultCount = new Dictionary<string, string>(16384);
            List<string> biGramm = new List<string>(16384);
            //работаем с биграммами
            FindAnyGramm(text, biGramm,2);
            CountGramm(biGramm, grammCount);
            CompareGramm(result, resultCount, grammCount, 2);
            //работаем с триграммами
            List<string> triGramm = new List<string>(16384);
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
            }
            return grammCount;
        }
        
        public static List<string> FindAnyGramm(List<List<string>> text, List<string> gramm, int typeGramm)
        {
            //ищем все возможные в тексте граммы 
            for (var i = 0; i < text.Count; i++)
            {
                if (typeGramm == 2)
                    for (var j = 0; j < text[i].Count - 1; j++)
                    {
                        //добавляем биграммы 
                        gramm.Add(text[i][j] + " " + text[i][j + 1]);
                    }
                if (typeGramm == 3)
                    for (var j = 0; j < text[i].Count - 2; j++)
                    {
                        //добавляем триграммы
                        gramm.Add(text[i][j] + " " + text[i][j + 1] + " " + text[i][j + 2]);
                    }
            }
            //сортируем элементы 
            gramm.Sort();
            return gramm;
        }
    }
}