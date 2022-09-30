# TextAnalysis
Решение практических задач сайта https://courses.openedu.ru темы Коллекции, строки, файлы курса Программирование на C# УрФУ
Общий код проекта в том числе тестирование был уже написан преподавателями. 
Конечная цель — сделать алгоритм продолжения текста, который, обучившись на большом тексте, будет способен по первому введенному слову предложить правдоподобное продолжение фразы.

Вся задача разбита на 3 этапа:
Разбиение анализируемого текста на предложения и слова (файл SentencesParserTask.cs)
Составление N-граммной модели текста по списку предложений (файл FrequencyAnalysisTask.cs)
Генерация текста по N-граммной модели (файл TextGeneratorTask.cs)