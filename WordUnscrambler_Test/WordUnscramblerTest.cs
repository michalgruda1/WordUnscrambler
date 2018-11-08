using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordUnscrambler;

namespace WordUnscrambler_Test
{
    [TestClass]
    public class WordUnscramblerTest
    {

        [TestMethod]
        public void AlphabetizationWorksCorrectlyWithWordMadeFromRandomizedAlphabet()
        {
            // init this way 1st, to shuffle it later
            char[] wordToUnscramble = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'r', 's', 't', 'u', 'w', 'x', 'y', 'z' };

            // init this way 1st to ensure visual comparability with above variable and make a dictionary word from it later
            char[]   wordDictionary = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'r', 's', 't', 'u', 'w', 'x', 'y', 'z' };

            Random random = new Random();

            // Fisher–Yates shuffle
            for (int i = wordToUnscramble.Length - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                char temp = wordToUnscramble[j];
                wordToUnscramble[j] = wordToUnscramble[i];
                wordToUnscramble[i] = temp;
            }

            // after shuffle, they're different
            Assert.AreNotEqual(wordDictionary, wordToUnscramble);

            // now shuffled alphabet creates a word to unscramble
            string wordToUnscramble_1 = new string(wordToUnscramble);

            // now ordered alphabet creates a dictionary word
            string wordDictionary_1 = new string(wordDictionary);

            Word wordToUnscramble_2 = new Word(wordToUnscramble_1);
            Word wordDictionary_2 = new Word(wordDictionary_1);

            // alphabetization makes scrambled word and dictionary word equal again
            Assert.AreEqual(wordDictionary_2.AlphabetizedWord, wordToUnscramble_2.AlphabetizedWord);
        }
    }
}
