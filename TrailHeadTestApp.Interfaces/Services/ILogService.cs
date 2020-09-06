using System;
using System.Collections.Generic;
using System.Text;

namespace TrailHeadTestApp.Interfaces.Services
{
    public interface ILogService
    {
        void LogError(Exception theException);
    }
}
