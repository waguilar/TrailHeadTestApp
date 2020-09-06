using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrailHeadTestApp.Interfaces.Infrastructure
{
    public interface IDialogHelper
    {
        IProgressDialog Loading();
        void Toast(string message);
        void Error(string message);
    }
}
