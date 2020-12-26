using System;
using WordTranslationGraph.GraphSystem;

namespace WordTranslationGraph
{
    class Program
    {
        static void Main(string[] args)
        {
            WordTranslation wordTranslation = new WordTranslation();
            
            wordTranslation.AddWord("correct", "правильно");
            wordTranslation.AddWord("right", "правильно");
            wordTranslation.AddWord("right", "право");
            wordTranslation.AddWord("hi", "привет");
            wordTranslation.AddWord("hello", "привет");
            wordTranslation.AddWord("hello", "здравствуйте");

            bool isRussian = true;
            while (true)
            {
                var word = Console.ReadLine();
                if (word == "exit") break;
                if (word == "eng")
                {
                    isRussian = true;
                    Console.WriteLine("Теперь переводы с английского > русский");
                    continue;
                }

                if (word == "rus")
                {
                    isRussian = false;
                    Console.WriteLine("Теперь переводы с русского > английский");
                    continue;
                }
                
                if (word == "json")
                {
                    Console.WriteLine(wordTranslation.GetJson());
                    continue;
                }
                
                var matches = wordTranslation.SeekWordTranslation(word, isRussian);
                if (matches.Count > 0)
                {
                    Console.WriteLine("Перевод: " + string.Join(", ", matches));
                }
                else
                {
                    Console.WriteLine($"Нет перевода для слова {word} на " + (isRussian ? "русском" : "английском") + " языке");
                    Console.WriteLine("Введи " + (isRussian ? "eng чтобы перевести с английского" : "rus - чтобы перевести с русского"));
                }
            }
        }
    }
}