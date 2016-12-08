using Android.App;
using Android.Widget;
using Android.OS;
using WordGame.GameCore;

namespace WordGame
{
    [Activity(Label = "WordGame", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            WordLibraryClass wlc = new WordLibraryClass(this);
            wlc.LoadWordsFromFile();
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            TextView aa = FindViewById<TextView>(Resource.Id.textView1);
            aa.Text = "Maruś kocham Cie. 555";
            // Set our view from the "main" layout resource
            
        }
    }
}

