namespace Shared.ValueObjects; 

public class Error
{
    public string Key { get; } = string.Empty;
    public string Value { get; } = string.Empty;

    public Error(string value, string key = "")
    {
        Key = string.IsNullOrEmpty(key) 
            ? Guid.NewGuid().ToString().Replace("-", "")[..8]
            : key;
    Value = value;}
}