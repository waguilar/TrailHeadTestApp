using System;
using System.Collections.Generic;
using System.Text;
using TrailHeadTestApp.Interfaces.Services;

namespace TrailHeadTestApp.Services
{
    public class LogService : ILogService
    {
        public void LogError(Exception ex)
        {
            try
            {
                Serilog.Log.Error(ex, "TH Demo Exception: ");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
