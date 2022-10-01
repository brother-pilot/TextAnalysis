using System.Collections.Generic;
/*
 В классе TextGeneratorTask реализуйте алгоритм продолжения текста по N-граммной модели.
Описание алгоритма:
На вход алгоритму передается словарь nextWords, полученный в предыдущей задаче, одно или несколько первых слов фразы phraseBeginning и wordsCount — количество слов, которые нужно дописать к phraseBeginning.
Словарь nextWords в качестве ключей содержит либо отдельные слова, либо пары слов, соединённые через пробел. По ключу key содержится слово, которым нужно продолжать фразы, заканчивающиеся на key.
Алгоритм должен работать следующим образом:
1.	Итоговая фраза должна начинаться с phraseBeginning.
2.	Алгоритм дописывает по одному слову к фразе wordsCount слов следующим образом:
a. Если фраза содержит как минимум два слова и в словаре есть ключ, состоящий из двух последних слов фразы, то продолжать нужно словом, хранящемся в словаре по этому ключу.
b. Иначе, если в словаре есть ключ, состоящий из одного последнего слова фразы, то продолжать нужно словом, хранящемся в словаре по этому ключу.
c. Иначе, нужно закончить генерирование фразы.
Проверяющая система сначала запустит эталонный способ разделения исходного текста на предложения и слова, потом эталонный способ построения словаря наиболее частотных продолжений из предыдущей задачи, а затем вызовет реализованный вами метод. В случае ошибки вы увидите исходный текст, на котором запускался процесс тестирования.
Если запустить проект на выполнение, он предложит ввести начало фразы и сгенерирует продолжение. Позапускайте алгоритм на разных текстах и разных фразах. Результат может быть интересным!
*/

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