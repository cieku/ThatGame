using System;

using Android.App;
using Android.Widget;
using Android.Content;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Plus;
using Android.Gms.Plus.Model.People;
using Android.Runtime;
using Android.OS;

using static Android.Gms.Common.Apis.GoogleApiClient;

namespace WordGame
{

    [Activity(Label = "LoginGoogleView", Icon = "@drawable/icon")]
    public class LoginActivity : Activity, IConnectionCallbacks, IOnConnectionFailedListener
    {

        private GoogleApiClient googleApiClient;
        private SignInButton googleSignInButton;

        private ConnectionResult connectonResult;

        private bool intentInProgress;
        private bool signInClicked;
        private bool infoPopulated;

        TextView textViewer;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.LoginView);

            textViewer = FindViewById<TextView>(Resource.Id.textView1);            

            googleSignInButton = FindViewById<SignInButton>(Resource.Id.SignInButton);

            googleSignInButton.Click += googleSignInButton_Click;

            GoogleApiClient.Builder builder = new GoogleApiClient.Builder(this);
            builder.AddConnectionCallbacks(this);
            builder.AddOnConnectionFailedListener(this);
            builder.AddApi(PlusClass.API);
            builder.AddScope(PlusClass.ScopePlusProfile);
            builder.AddScope(PlusClass.ScopePlusLogin);

            googleApiClient = builder.Build();
        }


        private void googleSignInButton_Click(object sender, EventArgs e)
        {
            if (!googleApiClient.IsConnecting)
            {
                signInClicked = true;
                ResolveSignInError();
            }
        }

        private void ResolveSignInError()
        {
            if (googleApiClient.IsConnected)
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
            if (requestCode == 0)
            {
                if (resultCode != Result.Ok)
                {
                    signInClicked = false;
                }

                intentInProgress = false;

                if (!googleApiClient.IsConnecting)
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
            if (googleApiClient.IsConnected)
            {
                googleApiClient.Disconnect();
            }

        }

        public void OnConnected(Bundle connectionHint)
        {
            signInClicked = false;

            if (infoPopulated)
            {
                // No need to populate info again.
                return;
            }

            if (PlusClass.PeopleApi.GetCurrentPerson(googleApiClient) != null)
            {
                IPerson plusUser = PlusClass.PeopleApi.GetCurrentPerson(googleApiClient);

                if (plusUser.HasDisplayName)
                {
                    textViewer.Text = plusUser.DisplayName;
                }

                // Collecting info done.
                infoPopulated = true;
            }
        }

        public void OnConnectionSuspended(int cause)
        {
            throw new NotImplementedException();
        }

        public void OnConnectionFailed(ConnectionResult result)
        {
            if (!intentInProgress)
            {
                connectonResult = result;

                if (signInClicked)
                {
                    ResolveSignInError();
                }
            }
        }
    }
}