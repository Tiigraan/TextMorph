namespace TextMorph;

public class TextMorpher
{
    private readonly Trie _trie = new ();
    
    public void AddMapping(string key, string value)
        => _trie.Add(key, value);

    public bool HasMapping(string key)
    {
        ArgumentNullException.ThrowIfNull(key);

        return _trie.TryGetValue(key, out _);
    }

    public string Morph(string text)
        => throw new NotImplementedException();
}