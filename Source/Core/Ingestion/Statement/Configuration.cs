namespace Statement;

public static class Configuration
{
    public static string? Name { get; set; } = "statement-assets";
    public static string? Description { get; set; } = "statement";
    public static Shared.ValueObjects.Environment Environment { get; set; } = new();
    public static Shared.Configuration.DatabaseConfiguration Database { get; set; } = new();
    public static string? Host { get; set; } = "/";
    
}
