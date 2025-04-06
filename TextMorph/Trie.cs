using System.Diagnostics.CodeAnalysis;

namespace TextMorph;

internal class Trie
{
    // ToDo This field used only in root node, need optimize?
    private readonly Dictionary<char, uint> _alphabet = new();

    private readonly Dictionary<uint, Trie> _next = new();

    private string? _value;

    public void Add(ReadOnlySpan<char> word, string value)
    {
        var node = this;
        foreach (var letter in word)
        {
            var index = ToIndex(letter);
            if (!node._next.ContainsKey(index))
            {
                node._next[index] = new Trie();
            }
            
            node = node._next[index];
        }

        node._value = value;
    }

    public bool TryGetValue(ReadOnlySpan<char> word, [NotNullWhen(true)]out string? value)
    {
        value = null;
        var node = this;
        
        foreach (var letter in word)
        {
            var index = ToIndex(letter);
            if (!node._next.TryGetValue(index, out node))
                return false;
        }

        value = node._value;
        
        return value is not null;
    }

    public bool TryGetFirstMatch(
        ReadOnlySpan<char> word, 
        [NotNullWhen(true)] out string? subKey,
        [NotNullWhen(true)] out string? value)
    {
        subKey = null;
        value = null;
        var node = this;

        for (var i = 0; i < word.Length; i++)
        {
            if (node._value is not null)
            {
                value = node._value;
                subKey = word[..i].ToString();
                return true;
            }
            
            var index = ToIndex(word[i]);
            if (!node._next.TryGetValue(index, out  node))
                return false;
        }

        value = node._value;
        
        return value is not null;
    }
    
    private uint ToIndex(char letter)
    {
        if (_alphabet.TryGetValue(letter, out var index)) 
            return index;
        
        index = (uint)_alphabet.Count;
        _alphabet.Add(letter, index);

        return index;
    }
}