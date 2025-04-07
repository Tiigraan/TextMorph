namespace TextMorph.Tests;

public class MorphTests
{
    [Fact]
    public void Morph_HappyPath()
    {
        // Arrange
        const string text = "abbbc";
        var morpher = new TextMorpher();
        morpher.AddMapping("bbb", "BBB");

        // Act
        var actual = morpher.Morph(text);

        // Assert
        Assert.Equal("aBBBc", actual);
    }

    [Fact]
    public void Morph_NotMathKey_ReturnSourceText()
    {
        // Arrange
        const string text = "abc";
        var morpher = new TextMorpher();
        morpher.AddMapping("e", "d");
        
        // Act
        var actual = morpher.Morph(text);
        
        // Assert
        Assert.Equal(text, actual);
    }
    
    [Fact]
    public void Morph_WithoutMapping_ReturnSourceText()
    {
        // Arrange
        const string text = "abc";
        var morpher = new TextMorpher();
        
        // Act
        var actual = morpher.Morph(text);
        
        // Assert
        Assert.Equal(text, actual);
    }

    [Fact]
    public void Morph_NullText_ThrowsArgumentNullException()
    {
        // Arrange
        var morpher = new TextMorpher();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => morpher.Morph(null));
    }

    [Fact]
    public void Morph_EmptyText_ReturnEmptyText()
    {
        // Arrange
        var morpher = new TextMorpher();
        
        // Act
        var actual = morpher.Morph(string.Empty);
        
        // Assert
        Assert.Equal(string.Empty, actual);
    }

    [Fact]
    public void Morph_WhenDuplicatingKey_UsedLastKey()
    {
        // Arrange
        const string text = "abbbc";
        var morpher = new TextMorpher();
        morpher.AddMapping("bbb", "ddd");
        morpher.AddMapping("bbb", "eee");
        
        // Act
        var actual = morpher.Morph(text);
        
        // Assert
        Assert.Equal("aeeec", actual);
    }

    [Fact]
    public void Morph_CaseSensitiveMappingWithDifferentCase_DoesNotReplace()
    {
        // Arrange
        const string text = "aBBBc";
        var morpher = new TextMorpher(caseInsensitive: false);
        morpher.AddMapping("bbb", "ddd");
        
        // Act
        var actual = morpher.Morph(text);
        
        // Assert
        Assert.Equal(text, actual);
    }
    
    [Fact]
    public void Morph_CaseInsensitiveMappingWithDifferentCase_ReplacesSuccessfully()
    {
        // Arrange
        const string text = "aBBBc";
        var morpher = new TextMorpher(caseInsensitive: true);
        morpher.AddMapping("bbb", "ddd");
        
        // Act
        var actual = morpher.Morph(text);
        
        // Assert
        Assert.Equal("adddc", actual);
    }
    
    [Theory]
    [InlineData("bbB", "x")]
    [InlineData("bBb", "x")]
    [InlineData("Bbb", "x")]
    [InlineData("bBB", "x")]
    [InlineData("BbB", "x")]
    [InlineData("BBb", "x")]
    public void Morph_CaseInsensitive_MixedCasing_Replaces(string input, string expected)
    {
        // Arrange
        var morpher = new TextMorpher(caseInsensitive: true);
        morpher.AddMapping("bbb", "x");
        
        // Act
        var actual = morpher.Morph(input);
        
        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Morph_CaseSensitive_MixedCasing_NotReplaced()
    {
        // Arrange
        const string text = "bBb";
        var morpher = new TextMorpher(caseInsensitive: false);
        morpher.AddMapping("bbb", "x");
        
        // Act
        var actual = morpher.Morph("bBb");
        
        // Assert
        Assert.Equal(text, actual);
    }
}