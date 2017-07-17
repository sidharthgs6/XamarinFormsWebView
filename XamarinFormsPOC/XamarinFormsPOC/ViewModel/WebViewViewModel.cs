using System;
using System.Threading.Tasks;
namespace XamarinFormsPOC
{
	public class WebViewViewModel
	{
       private Func<string, Task<string>> _evaluateJavascript;
        public Func<string, Task<string>> EvaluateJavascript
        {
	     get { return _evaluateJavascript; }
	     set { _evaluateJavascript = value; }
        }



		public async Task<string> GetHtmlContentfromWebView()
       {
	     var result = await EvaluateJavascript("document.body.innerHTML");
			string ss=result.ToString();
			return ss;
	      
		}




		public async Task<string> TriggerWebView()
		{
         // var result = await EvaluateJavascript("document.body.innerHTML");


         //var result = await EvaluateJavascript("document.getElementById('txtname').value;");

			await EvaluateJavascript("changeText();"); 

           //string ss=result.ToString();
			return "";
		}
	
	}
}
