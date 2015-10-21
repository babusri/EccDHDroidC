using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content.PM;

using System;
using System.Windows.Input;
using System.Security.Cryptography;
using Elliptic;


namespace EccDHDroidC
{
    [Activity(Label = "Elliptic Key Cryptography Demo", MainLauncher = true, Icon = "@mipmap/icon", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {

        byte[] aliceRandomBytes;
        byte[] alicePrivateBytes;
        byte[] alicePublicBytes;

        byte[] bobRandomBytes;
        byte[] bobPrivateBytes;
        byte[] bobPublicBytes;

        byte[] aliceBobSharedBytes;
        byte[] bobAliceSharedBytes;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Button a1b = FindViewById<Button>(Resource.Id.a1b);
            TextView AliceRandomText = FindViewById<TextView>(Resource.Id.a1t);

            Button a2b = FindViewById<Button>(Resource.Id.a2b);
            TextView AlicePrivateKeyText = FindViewById<TextView>(Resource.Id.a2t);

            Button a3b = FindViewById<Button>(Resource.Id.a3b);
            TextView AlicePublicKeyText = FindViewById<TextView>(Resource.Id.a3t);

            Button b1b = FindViewById<Button>(Resource.Id.b1b);
            TextView BobRandomText = FindViewById<TextView>(Resource.Id.b1t);

            Button b2b = FindViewById<Button>(Resource.Id.b2b);
            TextView BobPrivateKeyText = FindViewById<TextView>(Resource.Id.b2t);

            Button b3b = FindViewById<Button>(Resource.Id.b3b);
            TextView BobPublicKeyText = FindViewById<TextView>(Resource.Id.b3t);

            Button a4b = FindViewById<Button>(Resource.Id.a4b);
            TextView AliceBobSharedKeyText = FindViewById<TextView>(Resource.Id.a4t);

            Button b4b = FindViewById<Button>(Resource.Id.b4b);
            TextView BobAliceSharedKeyText = FindViewById<TextView>(Resource.Id.b4t);

            a1b.Click += delegate {
                alicePrivateBytes = null;
                AlicePrivateKeyText.Text = "";

                alicePublicBytes = null;
                AlicePublicKeyText.Text = "";

                aliceBobSharedBytes = null;
                bobAliceSharedBytes = null;

                AliceBobSharedKeyText.Text = "";
                BobAliceSharedKeyText.Text = "";

                aliceRandomBytes = new byte[32];
                RNGCryptoServiceProvider.Create().GetBytes(aliceRandomBytes);
                AliceRandomText.Text = BitConverter.ToString(aliceRandomBytes).Replace("-","");
            };

            a2b.Click += delegate {
                if (aliceRandomBytes != null)
                {
                    alicePrivateBytes = Curve25519.ClampPrivateKey(aliceRandomBytes);
                    AlicePrivateKeyText.Text = BitConverter.ToString(alicePrivateBytes).Replace("-","");
                }
            };

            a3b.Click += delegate {
                if (alicePrivateBytes != null)
                {
                    alicePublicBytes = Curve25519.GetPublicKey(alicePrivateBytes);
                    AlicePublicKeyText.Text = BitConverter.ToString(alicePublicBytes).Replace("-","");
                }
            };

            b1b.Click += delegate {
                bobPrivateBytes = null;
                BobPrivateKeyText.Text = ""; // Reset

                bobPublicBytes = null;
                BobPublicKeyText.Text = ""; // Reset

                aliceBobSharedBytes = null;
                bobAliceSharedBytes = null;

                AliceBobSharedKeyText.Text = "";
                BobAliceSharedKeyText.Text = "";

                bobRandomBytes = new byte[32];
                RNGCryptoServiceProvider.Create().GetBytes(bobRandomBytes);
                BobRandomText.Text = BitConverter.ToString(bobRandomBytes).Replace("-","");
            };

            b2b.Click += delegate {
                if (bobRandomBytes != null)
                {
                    bobPrivateBytes = Curve25519.ClampPrivateKey(bobRandomBytes);
                    BobPrivateKeyText.Text = BitConverter.ToString(bobPrivateBytes).Replace("-","");
                }
            };

            b3b.Click += delegate {
                if (bobPrivateBytes != null)
                {
                    bobPublicBytes = Curve25519.GetPublicKey(bobPrivateBytes);
                    BobPublicKeyText.Text = BitConverter.ToString(bobPublicBytes).Replace("-","");
                }
            };

            a4b.Click += delegate {
                if ( (alicePrivateBytes != null) && (bobPublicBytes != null) )
                {
                    aliceBobSharedBytes = Curve25519.GetSharedSecret(alicePrivateBytes, bobPublicBytes);
                    AliceBobSharedKeyText.Text = BitConverter.ToString(aliceBobSharedBytes).Replace("-","");
                }

            };


            b4b.Click += delegate {
                if ( (bobPrivateBytes != null) && (alicePublicBytes != null) )
                {
                    bobAliceSharedBytes = Curve25519.GetSharedSecret(bobPrivateBytes, alicePublicBytes);
                    BobAliceSharedKeyText.Text = BitConverter.ToString(bobAliceSharedBytes).Replace("-","");
                }
            };


        }
    }
}
