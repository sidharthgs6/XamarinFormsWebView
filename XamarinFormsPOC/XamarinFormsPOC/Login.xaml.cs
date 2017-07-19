using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinFormsPOC
{
   // [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
			this.BindingContext = new WebViewViewModel();

             b2cwebview.RegisterAction(JSToCSharp);

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    string source = "XamarinFormsPOC.LocalHTML.TermsAndCondition.html";
                    string htmlString = string.Empty;
                    var assembly = typeof(Login).GetTypeInfo().Assembly;
                    using (Stream stream = assembly.GetManifestResourceStream(source))
                    {
                        StreamReader reader = new StreamReader(stream);
                        htmlString = reader.ReadToEnd();
                    }

                    b2cwebview.Source = new HtmlWebViewSource { Html = htmlString };
                });

                return false;
            });


        }

        private void JSToCSharp(string obj)
        {
            DisplayAlert("Call Function From JS To C#", obj, "Ok");
        }

        void btnGO_Clicked(object sender, System.EventArgs e)
		{
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                     {
                         Device.BeginInvokeOnMainThread(() =>
                         {
                             b2cwebview.Source = weburl.Text;
                         });

                         return false;
                     });

        }

		async void btnWV_TO_MOB_Clicked(object sender, System.EventArgs e)
		{
           WebViewViewModel obj = (WebViewViewModel)this.BindingContext;
			var name= await obj.EvaluateJavascript("document.getElementById('txtname').value;");
            var address = await obj.EvaluateJavascript("document.getElementById('txtAddress').value;");
            var city = await obj.EvaluateJavascript("document.getElementById('txtCity').value;");
			txtName.Text = name.ToString();
			txtAddress.Text = address.ToString();
			txtCity.Text = city.ToString();
		}

		async void btnMOB_TO_WV_Clicked(object sender, System.EventArgs e)
		{
          WebViewViewModel obj = (WebViewViewModel)this.BindingContext;
           await obj.EvaluateJavascript("changeText('" + txtName.Text +"','"+ txtAddress.Text +"','"+ txtCity.Text +"');");

		}

		async void Handle_Clicked(object sender, System.EventArgs e)
		{ 
           WebViewViewModel obj = (WebViewViewModel)this.BindingContext;
			string htmlcontent = await obj.GetHtmlContentfromWebView();
			txtHtmlContent.Text = htmlcontent;
		}

		//void Handle_Navigating(object sender, Xamarin.Forms.WebNavigatingEventArgs e)
		//{
  //        WebViewViewModel obj = (WebViewViewModel)this.BindingContext;
 
		//}

        private async void btnMOB_TO_WV_CNHG_DDL_Clicked(object sender, EventArgs e)
        {
            WebViewViewModel obj = (WebViewViewModel)this.BindingContext;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            await obj.EvaluateJavascript("changeDropDown('20');");
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
           await DisplayAlert("ExecutionTime:Milliseconds", elapsedMs.ToString(), "Ok");
        }
    }
}