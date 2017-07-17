using System;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace XamarinFormsPOC
{
	public class WebViewer : WebView
	{
		public static BindableProperty EvaluateJavascriptProperty =
		BindableProperty.Create(nameof(EvaluateJavascript), typeof(Func<string, Task<string>>), typeof(WebViewer), null, BindingMode.OneWayToSource);

		public Func<string, Task<string>> EvaluateJavascript
		{
			get { return (Func<string, Task<string>>)GetValue(EvaluateJavascriptProperty); }
			set { SetValue(EvaluateJavascriptProperty, value); }
		}
	}
}
