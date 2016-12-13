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

namespace WordGame
{
    [Activity(Label = "ScoreActivity")]
    public class ScoreActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Score);
            string score = Intent.GetStringExtra("score") ?? "Data not available";
            TextView scoreTv = FindViewById<TextView>(Resource.Id.scoreTv);
            scoreTv.Text = string.Format("Your time: {0}", score);
            // Create your application here
        }
    }
}