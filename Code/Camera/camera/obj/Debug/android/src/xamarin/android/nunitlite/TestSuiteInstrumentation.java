package xamarin.android.nunitlite;


public abstract class TestSuiteInstrumentation
	extends android.app.Instrumentation
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onStart:()V:GetOnStartHandler\n" +
			"";
	}


	public TestSuiteInstrumentation () throws java.lang.Throwable
	{
		super ();
	}

	public void onCreate (android.os.Bundle arguments)
	{
		android.content.Context context = getContext ();

		// Mono Runtime Initialization {{{
		try {
			android.content.pm.ApplicationInfo runtimeInfo = context.getPackageManager ().getApplicationInfo ("Mono.Android.DebugRuntime", 0);
			android.content.pm.ApplicationInfo apiInfo = mono.MonoPackageManager.getApiPackageName () != null
				? context.getPackageManager ().getApplicationInfo (mono.MonoPackageManager.getApiPackageName (), 0)
				: null;
			mono.MonoPackageManager.LoadApplication (context, runtimeInfo.dataDir,
					apiInfo != null
					? new String[]{runtimeInfo.sourceDir, apiInfo.sourceDir, context.getApplicationInfo ().sourceDir}
					: new String[]{runtimeInfo.sourceDir, context.getApplicationInfo ().sourceDir});
		} catch (android.content.pm.PackageManager.NameNotFoundException e) {
			throw new RuntimeException ("Unable to find application Mono.Android.DebugRuntime" + 
					(mono.MonoPackageManager.getApiPackageName () == null ? "" : " or " + mono.MonoPackageManager.getApiPackageName ()) +
					"!", e);
		}
		// }}}
		mono.android.Runtime.register ("Xamarin.Android.NUnitLite.TestSuiteInstrumentation, Xamarin.Android.NUnitLite, Version=1.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", TestSuiteInstrumentation.class, __md_methods);
		n_onCreate (arguments);
	}

	private native void n_onCreate (android.os.Bundle arguments);


	public void onStart ()
	{
		n_onStart ();
	}

	private native void n_onStart ();

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
