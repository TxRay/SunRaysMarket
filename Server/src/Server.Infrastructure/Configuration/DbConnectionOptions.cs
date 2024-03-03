using System.ComponentModel.DataAnnotations;
using static System.String;

namespace SunRaysMarket.Server.Infrastructure.Configuration;

public class DbConnectionOptions
{
    public const string DbConnection = "DbConnection";
    
    public string? Server { get; set; }
    public string? Host { get; set; }
    public string? Database { get; set; }
    public string? Id { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Port { get; set; }

    public override string ToString()
    {
        List<string> expressions = [];

        foreach (var propertyInfo in GetType().GetProperties())
        {
            if (propertyInfo.GetValue(this) is string value && !IsNullOrEmpty(value))
            {
                expressions.Add(
                    $"{propertyInfo.Name}={value}"
                );
            }
        }

        return Join(';', expressions);
    }
}