using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinFormsPOC.Droid;
using System.Threading.Tasks;
using Android.Webkit;
using XamarinFormsPOC;
[assembly: ExportRenderer(typeof(WebViewer), typeof(WebViewRender))]
namespace XamarinFormsPOC.Droid
{
    public class WebViewRender : WebViewRenderer
    {
	 protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
	 {
		base.OnElementChanged(e);

		var webView = e.NewElement as WebViewer;
		if (webView != null)
			webView.EvaluateJavascript = async (js) =>
			{
				var reset = new System.Threading.ManualResetEvent(false);
				var response = string.Empty;
				Device.BeginInvokeOnMainThread(() =>
				{
					Control?.EvaluateJavascript(js, new JavascriptCallback((r) => { response = r; reset.Set(); }));
				});
				await Task.Run(() => { reset.WaitOne(); });
				return response;
			};
	 }
   }

internal class JavascriptCallback : Java.Lang.Object, IValueCallback
{
	public JavascriptCallback(Action<string> callback)
	{
		_callback = callback;
	}

	private Action<string> _callback;
	public void OnReceiveValue(Java.Lang.Object value)
	{
		_callback?.Invoke(Convert.ToString(value));
	} }
}
