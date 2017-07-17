using System;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace XamarinFormsPOC
{
	public class WebViewer : WebView
	{
		public static BindableProperty EvaluateJavascriptProperty =
		BindableProperty.Create(nameof(EvaluateJavascript), typeof(Func<string, Task<string>>), typeof(WebViewer), null, BindingMode.OneWayToSource);

        Action<string> action;

        public Func<string, Task<string>> EvaluateJavascript
		{
			get { return (Func<string, Task<string>>)GetValue(EvaluateJavascriptProperty); }
			set { SetValue(EvaluateJavascriptProperty, value); }
		}

        public void RegisterAction(Action<string> callback)
        {
            action = callback;
        }
        public void Cleanup()
        {
            action = null;
        }

        public void InvokeAction(string data)
        {
            if (action == null || data == null)
            {
                return;
            }
            action.Invoke(data);
        }
    }
}
