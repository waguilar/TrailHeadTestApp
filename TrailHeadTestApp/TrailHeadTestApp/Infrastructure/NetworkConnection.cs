using Plugin.Connectivity;
using TrailHeadTestApp.Interfaces.Infrastructure.Helpers;

namespace TrailHeadTestApp.Infrastructure
{
    public class NetworkConnection : IConnection
    {
        public bool IsConnected
        {
            get
            {
                if (!CrossConnectivity.IsSupported)
                {
                    return true;
                }
                return CrossConnectivity.Current.IsConnected;
            }
        }
    }
}
