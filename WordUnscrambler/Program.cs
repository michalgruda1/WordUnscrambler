using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WordUnscrambler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("We are going to unscramble words you provide, using a dictionary from a file.");
            Console.WriteLine("Which one do you choose:\nPress A if you want to load words to unscramble from file\nPress B if you want to provide words to unscramble by typing here (comma [,] separated)");

            string input = Console.ReadLine();

            List<Word> wordsToUnscramble = new List<Word>();

            List<Word> dictionary = new List<Word>();

            // read words to unscramble from file and create list of words to unscramble
            if (input.ToLower().Equals("a"))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(@"C:\Users\micha\source\repos\WordUnscrambler\WordUnscrambler\wordsToUnscramble.txt"))
                    {
                        
                        while (sr.Peek() != -1)
                        {
                            string line = sr.ReadLine();
                            wordsToUnscramble.Add(new Word(line));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Stumbled reading words to unscramble file :(\nComplaint: {0}",ex.Message);
                }
            }
            // read words from user input
            else if (input.ToLower().Equals("b"))
            {
                string input1 = Console.ReadLine();
                string[] Input1Splitted = input1.Split(',');

                // turn user input into words to unscramble
                foreach (string word in Input1Splitted)
                {
                    wordsToUnscramble.Add(new Word(word.Trim()));
                }
            }
            else
            {
                Console.WriteLine("A or B, please :)");
            }

            // read dictionary and create list of words from dictionary
            try
            {
                using (StreamReader sr = new StreamReader(@"C:\Users\micha\source\repos\WordUnscrambler\WordUnscrambler\dictionary.txt"))
                {
                    
                    while (sr.Peek() != -1)
                    {
                        string line = sr.ReadLine();
                        dictionary.Add(new Word(line));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Stumbled reading dictionary file :(\nComplaint: {0}", ex.Message);
            }

            // match words to unscramble with words from dictionary - at first by length, then by equality of alpahbetized words
            // if a word from dictionary, alphabetized, matches word from input, alphabetized, then word from dictionary is a match

            foreach (Word word in wordsToUnscramble)
            {
                // search alphabetized scrambled word in a dictionary of alphabetized words
                List<Word> matchingByLength = dictionary.FindAll(a => a.TheWord.Length == word.TheWord.Length);


                foreach (Word matchByLength in matchingByLength)
                {
                    if (matchByLength.AlphabetizedWord.Equals(word.AlphabetizedWord))
                    {
                        Console.WriteLine("A match found: {0} - {1}",word.TheWord,matchByLength.TheWord);
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
