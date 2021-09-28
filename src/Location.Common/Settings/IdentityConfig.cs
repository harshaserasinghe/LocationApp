using System.Collections.Generic;

namespace Location.Common.Settings
{
    public class IdentityConfig
    {
        public string Server { get; set; }
        public string SuperSecret { get; set; }
        public IDictionary<string, string> Scopes { get; set; }
        public IDictionary<string, string> Policies { get; set; }
        public List<Client> Clients { get; set; }
    }

    public class Client
    {
        public string Key { get; set; }
        public string Id { get; set; }
        public string Secret { get; set; }
        public string Name { get; set; }
        public List<string> AllowedScopes { get; set; }

    }
}
