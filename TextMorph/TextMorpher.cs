using System.Text;

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
    {
        ArgumentNullException.ThrowIfNull(text);

        var textSpan = text.AsSpan();
        var builder = new StringBuilder();
        var lastMathEnd = 0;
        
        for (var i = 0; i < textSpan.Length; i++)
        {
            if (!_trie.TryGetFirstMatch(textSpan[i..], out var subKey, out var value))
                continue;
            
            builder.Append(textSpan[lastMathEnd..i]);
            builder.Append(value);
            lastMathEnd = i + subKey.Length;
            i = lastMathEnd - 1;
        }
        
        builder.Append(textSpan[lastMathEnd..]);
        
        return builder.ToString();
    }
}