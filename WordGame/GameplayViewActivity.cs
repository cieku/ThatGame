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
using WordGame.GameCore;
using Android.Content.PM;
using System.Text.RegularExpressions;

namespace WordGame
{
    [Activity(Label = "GameplayView", ScreenOrientation = ScreenOrientation.Portrait)]
    public class GameplayViewActivity : Activity
    {
        string wordToDisplay;
        string wordToGuess;
        public System.Timers.Timer _timer;
        List<int> lettersRevealed = new List<int>();
        int indexof = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _timer = new System.Timers.Timer();
            _timer.Interval = 2000;
            _timer.Elapsed += OnTimedEvent;
            _timer.Enabled = true;

            SetContentView(Resource.Layout.GameplayView);

            TextView hintTv = FindViewById<TextView>(Resource.Id.hintTV);

            WordLibraryClass wlc = new WordLibraryClass(this);
            wlc.LoadWordsFromFile();

            wordToGuess = wlc.GetRandomWord();

            wordToDisplay = wordToGuess.Aggregate(string.Empty, (c, i) => c + i + ' ');

            Regex pattern = new Regex(@"\w");
            wordToDisplay= pattern.Replace(wordToDisplay, "X");
            hintTv.Text = wordToDisplay;

            
        }
        
        private void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            Random rnd = new Random();
            int indexof = rnd.Next(wordToDisplay.Length - 1);

            while (lettersRevealed.Contains(indexof))
            {
                indexof = rnd.Next(wordToGuess.Length - 1);
            }

            int indexOfLetter = GetIndexOfLetterInDisplayed(indexof);
            StringBuilder sb = new StringBuilder(wordToDisplay);
            sb[indexOfLetter] = wordToGuess[indexof];

            TextView hintTv = FindViewById<TextView>(Resource.Id.hintTV);
            wordToDisplay = sb.ToString();
            RunOnUiThread(() => hintTv.Text = wordToDisplay);
            //indexof++;
            //if(indexof == wordToGuess.Length)
            //{
            //    _timer.Enabled = false;
            //}
            lettersRevealed.Add(indexof);
            if(lettersRevealed.Count == wordToGuess.Length)
            {
                _timer.Enabled = false;
            }
        }

        public int GetIndexOfLetterInDisplayed(int indexOfLetterInWord)
        {
            if(indexOfLetterInWord == 0)
            {
                return 0;
            }
            return 2 * indexOfLetterInWord;
        }
    }
}