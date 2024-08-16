using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.IO;
using System.Runtime.CompilerServices;

namespace FindWords
{
    internal class WordBase
    {
        private string[] words; // массив слов необходимый для корректного чтения .txt файла с библиотекой слов
        public List<string> Words = new List<string>(); // список слов которые должен угадать игрок 
        public List<string> litter = new List<string>(); // список букв из которых необходимо собрать слово
        


        public void GetWords(int index) // метод обращается к файлам согласно индексу и задает список слов(Words),
                                        // присваивает значение переменой (litters) и сообщает ее методу GetLitters
        {
            var file = Directory.GetFiles("Levels")[index];
            words = File.ReadAllLines(file);
            string litters = words[0];
            for (int i = 0; i < words.Length; i++) 
            {
                Words.Add(words[i]);

            }
            GetLitters(litters);
        }

        private void GetLitters(string litters) // принимает слово из списка слов и "разбирает его на буквы"
        {
            for(int i = 0;i < litters.Length;i++) 
            {
                litter.Add(litters[i].ToString());
            }
        }
    }
}
