namespace _2023.Tests;

public class Tests
{
    [SetUp]
    public void SetUp()
    {

    }

    [Test]
    public void GetIntegersFromString_should_return_empty_when_no_integers()
    {
        // Arrange
        var line = "onetwothree";

        // Act
        var result = One.GetIntegersFromString(line);

        // Assert
        result.Should().BeEmpty();
    }

    [Test]
    public void GetIntegersFromString_should_return_integers_when_exist()
    {
        // Arrange
        var line = "1two3";

        // Act
        var result = One.GetIntegersFromString(line);

        // Assert
        result.Should().Be("13");
    }

    [Test]
    public void GetFirstAndLastNumeric_should_return_same_number_twice_when_only_one()
    {
        // Arrange
        var line = "1";

        // Act
        var result = One.GetFirstAndLastNumeric(line);

        // Assert
        result.Should().Be("11");
    }

    [Test]
    public void GetFirstAndLastNumeric_should_return_first_and_last_number()
    {
        // Arrange
        var line = "123";

        // Act
        var result = One.GetFirstAndLastNumeric(line);

        // Assert
        result.Should().Be("13");
    }

    [Test]
    public void StringNumberToInteger_should_replace_numbersintext_to_integers()
    {
        // Arrange
        var line = "onetwone34five";

        // Act
        var result = One.StringNumberToInteger(line);

        // Assert
        result.Should().Be("one1onetwo2twone1one34five5five");
    }
}