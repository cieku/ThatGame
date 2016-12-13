using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.Res;
using System.Text.RegularExpressions;

namespace WordGame.GameCore
{
    class WordLibraryClass
    {
        #region variables
        /// <summary>
        /// String displyed with spaces and x's
        /// </summary>
        private string wordToDisplay;

        /// <summary>
        /// Word to guess from list of words.
        /// </summary>
        private string wordToGuess;

        /// <summary>
        /// Indexes of letters that will be reveled.
        /// </summary>
        private List<int> hintLettersIndexes = new List<int>();

        /// <summary>
        /// File name of source of words.
        /// </summary>
        const string fileName = @"Words.txt";

        /// <summary>
        /// List of words.
        /// </summary>
        List<string> words = new List<string>();

        /// <summary>
        /// Context of activity.
        /// </summary>
        private Context context;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context"></param>
        public WordLibraryClass(Context context)
        {
            this.context = context;
            this.LoadWordsFromFile();
        }

        #endregion

        #region methods

        /// <summary>
        /// Checker if the word is fully displayed.
        /// </summary>
        /// <returns>
        /// True if word all letters are visible.
        /// </returns>
        public bool IsWordDisplayed()
        {
            if (hintLettersIndexes.Count == 0)
                return true;
            return false;
        }

        /// <summary>
        /// Gets word to display.
        /// </summary>
        /// <param name="revealRandomLetter">
        /// If true also reveals one more letter in the word.
        /// </param>
        /// <returns>
        /// Word to display string.
        /// </returns>
        public string GetWordToDisplay(bool revealRandomLetter=false)
        {
            if (revealRandomLetter)
                RevealRandomLetter();
            return wordToDisplay;
        }

        /// <summary>
        /// Loads list of words from file.
        /// </summary>
        private void LoadWordsFromFile()
        {
            AssetManager assets = context.Assets;
            System.IO.StreamReader file = new System.IO.StreamReader(assets.Open(fileName));
            string line;
            while ((line = file.ReadLine()) != null)
            {
                words.Add(line);
            }
            file.Close();
        }

        /// <summary>
        /// Rolls random word from the list of words and fills variables with it.
        /// </summary>
        public void RollRandomWord()
        {
            Random r = new Random();
            wordToGuess = words[r.Next(words.Count)];
            hintLettersIndexes = Enumerable.Range(0,wordToGuess.Length).ToList();
            hintLettersIndexes = hintLettersIndexes.OrderBy(item => r.Next()).ToList();
            Regex pattern = new Regex(@"\w");
            wordToDisplay = pattern.Replace(wordToGuess.Aggregate(string.Empty, (c, i) => c + i + ' '), "X");
        }

        /// <summary>
        /// Reveals random letter in wordToDisplay.
        /// </summary>
        private void RevealRandomLetter()
        {
            if(hintLettersIndexes.Count>0)
            {
                StringBuilder sb = new StringBuilder(wordToDisplay);
                sb[GetIndexOfLetterInDisplayed(hintLettersIndexes.First())] = wordToGuess[hintLettersIndexes.First()];
                wordToDisplay = sb.ToString();
                hintLettersIndexes.RemoveAt(0);
            }
        }

        /// <summary>
        /// Returns index of letter from string to guess in the displayed string.
        /// </summary>
        /// <param name="indexOfLetterInWord">
        /// Index of letter in word to guess. (without spaces)
        /// </param>
        /// <returns>
        /// Index of letter in displayed string.
        /// </returns>
        private int GetIndexOfLetterInDisplayed(int indexOfLetterInWord)
        {
            return 2 * indexOfLetterInWord;
        }

        /// <summary>
        /// Word validation.
        /// </summary>
        /// <param name="answer">
        /// Word to validate.
        /// </param>
        /// <returns>
        /// True if answer is euqal to word to guess.
        /// </returns>
        public bool ValidateTypedWord(string answer)
        {
            return wordToGuess.Equals(answer);
        }

        #endregion
    }
}