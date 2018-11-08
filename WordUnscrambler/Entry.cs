using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordUnscrambler
{
    class Word
    {
        public string TheWord { get; }
        public string AlphabetizedWord { get; }

        public Word(string word)
        {
            TheWord = word;
            AlphabetizedWord = Alphabetize(word);
        }

        public Word()
        {

        }

        // make "word", "drow", "rowd" a "dorw" - normalize word to a string consisting of it's chars ordered by alphabet
        // this enables comparisons of scrambled and unscrambled words to find a match
        public string Alphabetize(string word)
        {
            char[] wordChars = word.ToCharArray();
            Array.Sort(wordChars);
            return new string(wordChars);
        }
    }
}
