using Refit;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrailHeadTestApp.API.HttpClient
{
    public class ServiceFactory<T>
    {
        private readonly string _baseUrl;
        public ServiceFactory()
        {
            _baseUrl = Constants.BaseUrl;
        }
        public T CallApi()
        {
            return RestService.For<T>(_baseUrl);
        }
    }
}
