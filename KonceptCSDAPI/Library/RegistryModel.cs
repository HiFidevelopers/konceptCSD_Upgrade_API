using System;
using System.Collections.Generic;
using System.Text;

namespace KonceptSupportLibrary
{
    public class RegistryModel
    {
        public List<ReRoute> ReRoutes { get; set; }
        public GlobalConfiguration GlobalConfiguration { get; set; }
    }

    public class DownstreamHostAndPort
    {
        public string Host { get; set; }
        public int Port { get; set; }
    }

    public class ReRoute
    {
        public string DownstreamPathTemplate { get; set; }
        public string DownstreamScheme { get; set; }
        public List<DownstreamHostAndPort> DownstreamHostAndPorts { get; set; }
        public string UpstreamPathTemplate { get; set; }
        public List<string> UpstreamHttpMethod { get; set; }
        public AuthenticationOptions AuthenticationOptions { get; set; }
    }

    public class GlobalConfiguration
    {
        public string AdministrationPath { get; set; }
    }

    public class AuthenticationOptions
    {
        public string AuthenticationProviderKey { get; set; }

        public string[] AllowedScopes { get; set; }
    }
}
