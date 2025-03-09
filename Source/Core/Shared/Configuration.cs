namespace Shared;

public static class Configuration
{

    public class DatabaseConfiguration
    {
        public string InMemoryDatabaseName { get; set; } = string.Empty;
        public string Statement { get; set; } = string.Empty;

        public List<KeyValuePair<string, string>> GetConnectionStringList()
        {
            List<KeyValuePair<string, string>> connectionStrings = new List<KeyValuePair<string, string>>();

            foreach (PropertyInfo property in typeof(DatabaseConfiguration).GetProperties())
            {
                string? constring = property.GetValue(this) as string;

                if (!string.IsNullOrEmpty(constring))
                    connectionStrings.Add(new KeyValuePair<string, string>(property.Name, constring));
            }

            return connectionStrings;
        }

        public class AzureConfiguration
        {
            public bool Bypass { get; set; } = true;
            public string Authority { get; set; } = string.Empty;
            public string TenantName { get; set; } = string.Empty;
            public string TenantId { get; set; } = string.Empty;
            public Guid ClientId { get; set; } = Guid.Empty;
            public string SecretKey { get; set; } = string.Empty;
            public string RedirectUrl { get; set; } = string.Empty;
            public string HomeUrl { get; set; } = string.Empty;
            public int SessionTimeoutInMinuts { get; set; } = 20;
            public bool SecureConnectValidation { get; set; } = false;
        }

        public class LogginConfiguration
        {
            public string ElasticSearchServer { get; set; } = string.Empty;
            public LogLevelConfiguration? LogLevel { get; set; }
            public string LogstashServer { get; set; } = string.Empty;
            public string MinimumLevel { get; set; } = "Debug";
            public bool UseConsoleJsonFormatter { get; set; } = false;
        }

        public partial class LogLevelConfiguration
        {
            public string Default { get; set; } = "Error";
            public string Microsoft { get; set; } = String.Empty;
            public string? MicrosoftHostingLifetime { get; set; }

        }

        public class RabbitMqConfiguration
        {
            public string Host { get; set; } = string.Empty;
            public string Password { get; set; } = String.Empty;
            public int Port { get; set; }
            public ushort PreFethCount { get; set; } = 10;
            public ushort ThreadsPerConsumer { get; set; } = 10;

            public string User { get; set; } = string.Empty;
            public string VirtualHost { get; set; } = "/";
            public string Exchange { get; set; } = string.Empty;
            public string RollbackRequestQueue { get; set; } = string.Empty;
            public string RollbackRequestRoutingKey { get; set; } = string.Empty;
        }

        public class QueueAndExchange
        {
            public string Queue { get; set; } = string.Empty;
            public string Exchange { get; set; } = string.Empty;
            public string RoutingKey { get; set; } = string.Empty;
        }

        public class SecureGatewayConfiguration
        {
            public string AuthenticationKey { get; set; } = string.Empty;
            public string ServiceUrl { get; set; } = string.Empty;

        }
    }
}

