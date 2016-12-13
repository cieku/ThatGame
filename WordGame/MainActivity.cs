using Android.App;
using Android.Widget;

using Android.OS;



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

            Button button = FindViewById<Button>(Resource.Id.StartButton);

            button.Click += delegate
            {
                aa.Text = "Maro i Pysio zakochana paraaa. ;D";
            };
        }
    }
}

