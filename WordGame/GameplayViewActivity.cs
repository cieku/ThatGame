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
using System.Diagnostics;

namespace WordGame
{
    [Activity(Label = "GameplayView", ScreenOrientation = ScreenOrientation.Portrait)]
    public class GameplayViewActivity : Activity
    {
        /// <summary>
        /// Timer.
        /// </summary>
        private System.Timers.Timer _timer;

        /// <summary>
        /// The world library class which is a controler for this activity.
        /// </summary>
        WordLibraryClass wlc;

        /// <summary>
        /// Textview with hint letters.
        /// </summary>
        TextView hintTv;

        /// <summary>
        /// Answer text view.
        /// </summary>
        TextView answerTV;

        /// <summary>
        /// Stopwatch to measure time of singe game.
        /// </summary>
        Stopwatch stopwatch;

        /// <summary>
        /// Time elapsed text view.
        /// </summary>
        TextView timeElapsedTV;

        /// <summary>
        /// On create.
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Initialize View elements
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.GameplayView);
            hintTv = FindViewById<TextView>(Resource.Id.hintTV);
            answerTV = FindViewById<TextView>(Resource.Id.answerTV);
            timeElapsedTV = FindViewById<TextView>(Resource.Id.timeElapsed);

            //Initialize veriables
            InitializeTimer();
            wlc = new WordLibraryClass(this);
            wlc.RollRandomWord();

            hintTv.Text = wlc.GetWordToDisplay();

            stopwatch = new Stopwatch();
            stopwatch.Start();
        }
        
        /// <summary>
        /// Event called on timer tick.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            RunOnUiThread(() => hintTv.Text = wlc.GetWordToDisplay(true));
            RunOnUiThread(() => timeElapsedTV.Text = string.Format("Time elapsed: {0}", TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds).Seconds.ToString()));
            if (wlc.IsWordDisplayed())
            {
                _timer.Enabled = false;
            }
        }

        /// <summary>
        /// Initialize timer.
        /// </summary>
        private void InitializeTimer()
        {
            _timer = new System.Timers.Timer();
            _timer.Interval = 2000;
            _timer.Elapsed += OnTimedEvent;
            _timer.Enabled = true;
        }

        /// <summary>
        /// On click for check.
        /// </summary>
        /// <param name="v"></param>
        [Java.Interop.Export("Submit")]
        public void Submit(View v)
        {
            var answer = answerTV.Text;

            if(wlc.ValidateTypedWord(answer.ToUpper()))
            {
                stopwatch.Stop();
                string score = TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds).ToString(@"hh\:mm\:ss\:fff");
                var scoreActivity = new Intent(this, typeof(ScoreActivity));
                scoreActivity.PutExtra("score", score);
                StartActivity(scoreActivity);
            }
            else
            {
                Toast.MakeText(this, "Wrong nigga !", ToastLength.Long).Show();
            }
        }
    }
}