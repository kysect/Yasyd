namespace Kysect.Yasyd.Communication.Headers;

public class YasydSystemHeader
{
    public static YasydSystemHeader Empty { get; } = new YasydSystemHeader();

    private readonly Dictionary<string, string> _headers = new Dictionary<string, string>();

    public IReadOnlyCollection<KeyValuePair<string, string>> EnumerateHeaders()
    {
        return _headers;
    }

    public void Add(string key, string value)
    {
        ArgumentNullException.ThrowIfNull(key);
        ArgumentNullException.ThrowIfNull(value);

        if (_headers.ContainsKey(key))
            throw new ArgumentException($"Key {key} already added");

        _headers[key] = value;
    }
}