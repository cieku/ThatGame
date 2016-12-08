using Android.App;
using Android.Widget;
using Android.OS;
using Android.Gms.Common.Apis;
using Android.Gms.Common;
using System;
using Android.Runtime;
using static Android.Gms.Common.Apis.GoogleApiClient;
using Android.Gms.Plus;
using Android.Content;
using Android.Gms.Plus.Model.People;

namespace WordGame
{
    [Activity(Label = "WordGame", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, IConnectionCallbacks, IOnConnectionFailedListener
    {

        private GoogleApiClient googleApiClient;
        private SignInButton googleSignInButton;

        private ConnectionResult connectonResult;

        private bool intentInProgress;
        private bool signInClicked;
        private bool infoPopulated;

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


            googleSignInButton = FindViewById<SignInButton>(Resource.Id.SignInButton);

            googleSignInButton.Click += googleSignInButton_Click;

            GoogleApiClient.Builder builder = new GoogleApiClient.Builder(this);
            builder.AddConnectionCallbacks(this);
            builder.AddOnConnectionFailedListener(this);
            builder.AddApi(PlusClass.API);
            builder.AddScope(PlusClass.ScopePlusProfile);
            builder.AddScope(PlusClass.ScopePlusLogin);

            googleApiClient = builder.Build();

            //   GoogleApiClient client = new GoogleApiClient.Builder(this)
            //       .enableAutoManage(this /* FragmentActivity */,
            //                        this /* OnConnectionFailedListener */)
            //.addApi(Drive.API)
            //.addScope(Drive.SCOPE_FILE)
            //.setAccountName("users.account.name@gmail.com")
            //.build();



        }
        private void googleSignInButton_Click(object sender, EventArgs e)
        {
            if(!googleApiClient.IsConnecting)
            {
                signInClicked = true;
                ResolveSignInError();
            }


        }

        private void ResolveSignInError()
        {
            if(googleApiClient.IsConnected)
            {
                return;
            }

            if (connectonResult.HasResolution)
            {
                try
                {
                    intentInProgress = true;
                    StartIntentSenderForResult(connectonResult.Resolution.IntentSender, 0, null, 0, 0, 0);
                }
                catch (Android.Content.IntentSender.SendIntentException e)
                {
                    intentInProgress = false;
                    googleApiClient.Connect();
                }
            }
                

        }


        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            if(requestCode == 0)
            {
                if (resultCode != Result.Ok)
                {
                    signInClicked = false;
                }

                intentInProgress = false;

                if(!googleApiClient.IsConnecting)
                {
                    googleApiClient.Connect();
                }
            }
        }
        protected override void OnStart()
        {
            base.OnStart();
            googleApiClient.Connect();
        }

        protected override void OnStop()
        {
            base.OnStop();
            if(googleApiClient.IsConnected)
            {
                googleApiClient.Disconnect();
            }

        }

        public void OnConnected(Bundle connectionHint)
        {
            signInClicked = false;
            
            if(PlusClass.PeopleApi.GetCurrentPerson(googleApiClient) != null)
            {
                IPerson plusUser = PlusClass.PeopleApi.GetCurrentPerson(googleApiClient);
                aa.Text = plusUser.DisplayName;
            }
        }

        public void OnConnectionSuspended(int cause)
        {
            throw new NotImplementedException();
        }

        public void OnConnectionFailed(ConnectionResult result)
        {
            if(!intentInProgress)
            {
                connectonResult = result;

                if(signInClicked)
                {
                    ResolveSignInError();
                }
            }
        }
    }
}

