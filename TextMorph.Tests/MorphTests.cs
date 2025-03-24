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
}