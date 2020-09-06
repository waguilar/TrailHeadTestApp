using Acr.UserDialogs;
using TrailHeadTestApp.Interfaces.Infrastructure;
using Xamarin.Forms;

namespace TrailHeadTestApp.Infrastructure
{
    public class DialogHelper : IDialogHelper
    {
        public IProgressDialog Loading()
        {

            return UserDialogs.Instance.Loading();
        }

        public void Toast(string message)
        {
            UserDialogs.Instance.Toast(message);
        }

        public void Error(string message)
        {
            var cfg = new ToastConfig(message) { BackgroundColor = Color.DarkRed, MessageTextColor = Color.White };
            UserDialogs.Instance.Toast(cfg);
        }
    }
}
