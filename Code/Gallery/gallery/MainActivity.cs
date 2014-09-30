using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace gallery
{
	[Activity (Label = "gallery", MainLauncher = true)]
	public class MainActivity : Activity
	{
	

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {
				var imageIntent = new Intent ();
				imageIntent.SetType ("image/*");
				imageIntent.SetAction (Intent.ActionGetContent);

				StartActivityForResult (
					Intent.CreateChooser (imageIntent, "Select photo"), 0);

			};

		}
		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);

			if (resultCode == Result.Ok) {
				var imageView = 
					FindViewById<ImageView> (Resource.Id.myImageView);
				imageView.SetImageURI (data.Data);
			}
		}
	}

}


