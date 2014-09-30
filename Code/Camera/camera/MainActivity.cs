using System;
using System.Configuration;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Provider;
using Java.Lang;
using Java.Lang.Reflect;
using Android.Graphics;
using Android.Content.PM;
using Java.IO;
namespace camera
{

	public static class BitmapHelpers
	{
		public static Bitmap LoadAndResizeBitmap(this string fileName, int width, int height)
		{
			// First we get the the dimensions of the file on disk
			BitmapFactory.Options options = new BitmapFactory.Options { InJustDecodeBounds = true };
			BitmapFactory.DecodeFile(fileName, options);

			// Next we calculate the ratio that we need to resize the image by
			// in order to fit the requested dimensions.
			int outHeight = options.OutHeight;
			int outWidth = options.OutWidth;
			int inSampleSize = 1;

			if (outHeight > height || outWidth > width)
			{
				inSampleSize = outWidth > outHeight
					? outHeight / height
					: outWidth / width;
			}

			// Now we will load the image and have BitmapFactory resize it for us.
			options.InSampleSize = inSampleSize;
			options.InJustDecodeBounds = false;
			Bitmap resizedBitmap = BitmapFactory.DecodeFile(fileName, options);

			return resizedBitmap;
		}
	}
	[Activity (Label = "camera", MainLauncher = true)]
	public class MainActivity : Activity
	{
		Java.IO.File _file;
		Java.IO.File _dir;
		ImageView _imageView;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			if (IsThereAnAppToTakePictures())
			{
				CreateDirectoryForPictures();

				Button button = FindViewById<Button>(Resource.Id.myButton);
				_imageView = FindViewById<ImageView>(Resource.Id.imageView1);

				button.Click += TakeAPicture;
			}
		}
		private bool IsThereAnAppToTakePictures()
		{
		
			Intent intent = new Intent(MediaStore.ActionImageCapture);	
			//			IList<ResolveInfo> availableActivities = PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
			return true;//availableActivities != null && availableActivities.Count > 0;
		}

		private void CreateDirectoryForPictures()
		{
			_dir = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), "CameraAppDemo");
			if (!_dir.Exists())
			{
				_dir.Mkdirs();
			}

			
		}
		private void TakeAPicture(object sender, EventArgs eventArgs)
		{
			Intent intent = new Intent(MediaStore.ActionImageCapture);

			_file = new File(_dir, System.String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));

			intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(_file));
		
			StartActivityForResult(intent, 0);

		}
		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);

			// make it available in the gallery
			Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
			Android.Net.Uri contentUri = Android.Net.Uri.FromFile(_file);
			mediaScanIntent.SetData(contentUri);
			SendBroadcast(mediaScanIntent);

			// display in ImageView. We will resize the bitmap to fit the display
			// Loading the full sized image will consume to much memory 
			// and cause the application to crash.
			int height = _imageView.Height;
			int width = Resources.DisplayMetrics.WidthPixels;
			using (Bitmap bitmap = _file.Path.LoadAndResizeBitmap(width, height))
			{
				_imageView.SetImageBitmap(bitmap);
			}
		}
	}
}


