using System;
using Xamarin.Forms.Platform.iOS;
using XamarinFormsPOC.iOS;
using XamarinFormsPOC;
using Xamarin.Forms;
using System.Threading.Tasks;
[assembly: ExportRenderer(typeof(WebViewer), typeof(WebViewRender))]
namespace XamarinFormsPOC.iOS
{
   public class WebViewRender : WebViewRenderer
{
	protected override void OnElementChanged(VisualElementChangedEventArgs e)
	{		base.OnElementChanged(e);

		var webView = e.NewElement as WebViewer;
		if (webView != null)
			webView.EvaluateJavascript = (js) =>
			{
				return Task.FromResult(this.EvaluateJavascript(js));
			};
	} }
}
