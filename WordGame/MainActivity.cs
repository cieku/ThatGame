using Android.App;
using Android.Widget;
using Android.OS;
using WordGame.GameCore;
using Android.Content;
using Android.Views;

namespace WordGame
{
    [Activity(Label = "WordGame", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            TextView aa = FindViewById<TextView>(Resource.Id.textView1);
            aa.Text = "Maruś kocham Cie. 555";
        }

        /// <summary>
        /// On click for go to gameplay.
        /// </summary>
        /// <param name="v"></param>
        [Java.Interop.Export("GoToGameplay")]
        public void GoToGameplay(View v)
        {
            StartActivity(typeof(GameplayViewActivity));
        }
    }
}

