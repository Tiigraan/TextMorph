namespace TextMorph.Tests;

public class HasMappingTests
{
    [Fact]
    public void HasMapping_KeyExists_ReturnsTrue()
    {
        // Arrange
        var morpher = new TextMorpher();
        morpher.AddMapping("hello", "привет");

        // Act
        bool result = morpher.HasMapping("hello");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void HasMapping_KeyDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var morpher = new TextMorpher();
        morpher.AddMapping("hello", "привет");

        // Act
        bool result = morpher.HasMapping("world");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void HasMapping_KeyIsCaseSensitive_ReturnsFalseForDifferentCase()
    {
        // Arrange
        var morpher = new TextMorpher();
        morpher.AddMapping("hello", "привет");

        // Act
        bool result = morpher.HasMapping("HELLO");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void HasMapping_EmptyKey_ReturnsFalse()
    {
        // Arrange
        var morpher = new TextMorpher();

        // Act
        bool result = morpher.HasMapping("");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void HasMapping_NullKey_ThrowsArgumentNullException()
    {
        // Arrange
        var morpher = new TextMorpher();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => morpher.HasMapping(null));
    }
    
    [Fact]
    public void HasMapping_DuplicateKey_ReturnsTrue()
    {
        // Arrange
        var morpher = new TextMorpher();
        morpher.AddMapping("hello", "привет");
        morpher.AddMapping("hello", "здравствуйте");

        // Act
        bool result = morpher.HasMapping("hello");

        // Assert
        Assert.True(result);
    }
}