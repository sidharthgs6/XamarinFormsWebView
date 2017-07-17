using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using XamarinFormsPOC;
using XamarinFormsPOC.UWP;

[assembly: ExportRenderer(typeof(WebViewer), typeof(WebViewRender))]
namespace XamarinFormsPOC.UWP
{
    public class WebViewRender : WebViewRenderer
    {

        const string JavaScriptFunction = "function invokeCSharpAction(data){window.external.notify(data);}";
        WebViewer webviewobj;
        protected async override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);
            var webView = e.NewElement as WebViewer;
            webviewobj = webView;
            if (e.OldElement != null)
            {
                Control.NavigationCompleted -= OnWebViewNavigationCompleted;
                Control.ScriptNotify -= OnWebViewScriptNotify;
            }
            if (e.NewElement != null)
            {
                Control.NavigationCompleted += OnWebViewNavigationCompleted;
                Control.ScriptNotify += OnWebViewScriptNotify;

            }

            if (webView != null)
                webView.EvaluateJavascript = async (js) =>
                {
                     return await Control.InvokeScriptAsync("eval", new[] { js });
                    
                };
        }
        async void OnWebViewNavigationCompleted(Windows.UI.Xaml.Controls.WebView sender, Windows.UI.Xaml.Controls.WebViewNavigationCompletedEventArgs args)
        {
            if (args.IsSuccess)
            {
                // Inject JS script
                await Control.InvokeScriptAsync("eval", new[] { JavaScriptFunction });
            }
        }

        void OnWebViewScriptNotify(object sender, Windows.UI.Xaml.Controls.NotifyEventArgs e)
        {
            // Element.InvokeAction(e.Value);
            webviewobj.InvokeAction(e.Value);


        }


    }
}
