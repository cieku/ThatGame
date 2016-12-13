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


        TextView aa;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            aa = FindViewById<TextView>(Resource.Id.textView1);
            aa.Text = "Maruś kocham Cie. 555";

            Button buttonKliknij = FindViewById<Button>(Resource.Id.kliknijMnieButton);
            buttonKliknij.Click += delegate
                {
                    aa.Text = "Maro i Pysio zakochana paraaa. ;D";
                };

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


        /// <summary>
        /// Goes to login page.
        /// </summary>
        /// <param name="v"></param>
        [Java.Interop.Export("LogIntoGoogle")]
        public void LogIntoGoogle(View v)
        {
            StartActivity(typeof(LoginActivity));
        }
    }
}

