using Android.App;
using Android.Widget;
using Android.OS;

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
            aa.Text = "Dupa";
            // Set our view from the "main" layout resource
            
        }
    }
}

