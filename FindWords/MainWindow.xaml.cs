using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace FindWords
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> words = new List<string>(); // список слов
        
        List<string> litter = new List<string>(); // список букв из которых необходимо собрать слово
        
        List<string> wordRepetition = new List<string>() // список слов которые игрок уже отгадал
        {""};
        private int level = 0; // индекс уровня
        


    private bool inputConsoleIsFree = true; // переменная определяющая состояние консоли ввода текста
        private bool victory; // переменная определяющая завершил ли игрок уровень
        public MainWindow()
        {
            InitializeComponent(); // метод инициализирующий компоненты .xaml
            InitializeGame(); // метод инициализирующий программу
        }

        private void InitializeGame() // метод создает объект(Base) ссылочного типа, и принимает список слов и букв    
        {
            WordBase wordBase = new WordBase();  
            wordBase.GetWords(level);
            words = wordBase.Words;
            litter = wordBase.litter;

            MoveGameBoard();
        }

        private void MoveGameBoard() //метод устанавливает значение игровых элементов
        {
            Random random = new Random();

            foreach (Button button in mainGrid.Children.OfType<Button>())
            {
                if (litter.Count > 0)
                {
                    int index = random.Next(litter.Count);
                    string nextLitter = litter[index];
                    button.Content = nextLitter;
                    litter.RemoveAt(index);
                }
                else if (button.Name != check.Name && button.Name != clean.Name)
                {
                    button.Visibility = Visibility.Collapsed;
                }
            }
            inputText.Text = "";
            countWord.Visibility = Visibility.Visible;
            countWord.Text = words.Count.ToString();
            findWords.Text = "";
            findWords.Visibility = Visibility.Visible;
        }  

        private void Button_Click(object sender, RoutedEventArgs e) // метод отвечает за ввод слова
        {
            Button button = sender as Button;
            if (!inputConsoleIsFree)
            {
                
                inputText.Text = "";
                inputText.Text += button.Content;
                inputConsoleIsFree = true;

            }
            else if (!victory)
            {
               
                inputText.Text += button.Content;
            }
            
        }
        private void CheckVictory() // метод проверяет всели слова отгадал игрок
        {
            if (words.Count <= 0)
            {
                inputText.Text = "Отлично!!! Сыграем еще?";
                victory = true;
                countWord.Visibility = Visibility.Collapsed;
                findWords.Visibility = Visibility.Collapsed;
                level++;

                foreach (Button button in mainGrid.Children.OfType<Button>())
                {
                    if (button.Name != check.Name)
                    {
                        button.Visibility = Visibility.Collapsed;
                    }
                    else 
                    {
                        button.Content = "Играем!";
                    }
                    
                    
                }

            }
        }

        private void check_Click(object sender, RoutedEventArgs e)// метод проверяет верно ли веденое игроком слово
        {
            if (victory)
            {
                foreach (Button button in mainGrid.Children.OfType<Button>())
                {
                    button.Visibility = Visibility.Visible;
                    victory = false;
                    if (button.Name == check.Name) button.Content = "Проверить";

                }
                
                    InitializeGame();
            }
            else
            {
                bool wordFind = false;
                for (int i = 0; i < words.Count; i++)
                {
                    if (words[i] == inputText.Text)
                    {
                        wordRepetition.Add(inputText.Text);
                        words.RemoveAt(i);
                        findWords.Text += "\n" + inputText.Text;
                        inputText.Text = "Верно!!!";
                        CheckVictory();
                        wordFind = true;
                        countWord.Text = words.Count.ToString();
                        break;
                    }
                }
                if (!wordFind) CheckRepeat(inputText.Text);
                inputConsoleIsFree = false;
            }

        }

        private void CheckRepeat(string word) // метод проверяет повторяется ли слово
        {
           
                for (int i = 0; i < wordRepetition.Count; i++)
                {
                    if (wordRepetition[i] == word)
                    {
                        inputText.Text = "Вы уже угадали это слово";
                        break;
                    }
                    else
                    {
                        inputText.Text = "Не верно!!!";
                    }
                }
            
        }

        private void clean_Click(object sender, RoutedEventArgs e) // метод очищает строку ввода
        {
            
            inputText.Text = "";
        }

        
    }
}
