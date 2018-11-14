using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace WordUnscrambler
{
    class Program
    {
        public const string pathToDictionaryFile = @"C:\Users\micha\source\repos\WordUnscrambler\WordUnscrambler\";
        public const string dictionaryFileName = @"dictionary.txt";
        public const string pathToFileWithWordsToUnscramble = @"C:\Users\micha\source\repos\WordUnscrambler\WordUnscrambler\";
        public const string wordsToUnscrambleFileName = @"wordsToUnscramble.txt";

        static void Main(string[] args)
        {
            // declare
            List<Word> wordsToUnscramble;
            List<Word> dictionary;
            bool continueToUnscrambleUserDecision;

            do
            {
                // initialize
                wordsToUnscramble = new List<Word>();
                dictionary = new List<Word>();
                continueToUnscrambleUserDecision = true;

                Console.WriteLine("We are going to unscramble words you provide, using a dictionary from a file.");
                Console.WriteLine("Which one do you choose:\nPress A if you want to load words to unscramble from file\nPress B if you want to provide words to unscramble by typing them here");

                string input = Console.ReadLine() ?? string.Empty;

                // read words to unscramble from file and create list of words to unscramble

                switch (input.ToLower())
                {
                    case "a":
                        try
                        {
                            using (StreamReader sr = new StreamReader(pathToFileWithWordsToUnscramble + wordsToUnscrambleFileName))
                            {
                                while (sr.Peek() != -1) // check if next line exists
                                {
                                    string line = sr.ReadLine();
                                    wordsToUnscramble.Add(new Word(line.Trim()));
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Stumbled reading words to unscramble file :(\n\nComplaint: {0}", e.Message);
                        }
                        break;
                    case "b":
                        // take words from user
                        Console.WriteLine("Type in words to unscramble - comma [,] separated. Example: words, to, unscramble");
                        string userProvidedWords = Console.ReadLine() ?? string.Empty;
                        string[] Input1Splitted = userProvidedWords.Split(',');

                        // turn user input into words to unscramble
                        foreach (string word in Input1Splitted)
                        {
                            wordsToUnscramble.Add(new Word(word.Trim()));
                        }
                        break;
                    default:
                        Console.WriteLine("Press key: A or B to continue, or ESC to exit program");
                        break;
                }

                // read dictionary and create list of words from dictionary
                try
                {
                    Console.WriteLine("Unscrambling...");
                    using (StreamReader sr = new StreamReader(pathToDictionaryFile + dictionaryFileName))
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
                    // search alphabetized scrambled word in a dictionary of alphabetized words by length to find possible match
                    List<Word> matchingByLength = dictionary.FindAll(a => a.TheWord.Length == word.TheWord.Length);

                    foreach (Word matchByLength in matchingByLength)
                    {
                        if (matchByLength.AlphabetizedWord.Equals(word.AlphabetizedWord))
                        {
                            Console.WriteLine("A match found: {0} - {1}", word.TheWord, matchByLength.TheWord);
                        }
                    }
                }

                // ask user if to continue the program
                Console.WriteLine("Do you want to continue? Press Y for yes, and N to exit program");
                input = Console.ReadLine();
                if (input.Equals("n",StringComparison.OrdinalIgnoreCase)) {
                    continueToUnscrambleUserDecision = false;
                }
            }
            while (continueToUnscrambleUserDecision);

            Console.WriteLine("Exiting...");
        }
    }
}
