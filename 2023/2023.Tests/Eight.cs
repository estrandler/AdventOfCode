
namespace _2023.Tests;

public class EightTests
{

    [Test]
    public void Parse()
    {
        // Arrange
        var lines = new[] {
            "RL",
            "",
            "AAA = (BBB, CCC)",
            "BBB = (DDD, EEE)",
            "CCC = (ZZZ, GGG)",
            "DDD = (DDD, DDD)",
            "EEE = (EEE, EEE)",
            "GGG = (GGG, GGG)",
            "ZZZ = (ZZZ, ZZZ)",
        };

        // Act
        var (instructions, nodes) = Eight.Parse(lines);

        //Assert
        instructions.Should().BeEquivalentTo(new[] { "R", "L" });
        nodes.Should().BeEquivalentTo(new Dictionary<string, (string left, string right)> {
            {"AAA", ("BBB", "CCC")},
            {"BBB", ("DDD", "EEE")},
            {"CCC", ("ZZZ", "GGG")},
            {"DDD", ("DDD", "DDD")},
            {"EEE", ("EEE", "EEE")},
            {"GGG", ("GGG", "GGG")},
            { "ZZZ", ("ZZZ", "ZZZ") },
        });
    }
    public void GetNextPosition()
    {
        // Arrange
        var instruction = "L";
        var left = "BBB";
        var right = "CCC";

        // Act
        var result = Eight.GetNextPosition(instruction, left, right);

        //Assert
        result.Should().Be("BBB");
    }

    [Test]
    public void GetNodeFromKey()
    {
        // Arrange
        var key = "BBB";
        var positions = new Dictionary<string, (string left, string right)>()
        {
            { "AAA", ("BBB", "CCC") },
            { "BBB", ("CCC", "AAA") },
        };

        // Act
        var result = Eight.GetNodeFromKey(key, positions);

        //Assert
        result.Should().Be(("CCC", "AAA"));
    }

    [Test]
    public void GetNextInstruction()
    {
        // Arrange
        var instructions = new[] { "R", "L", "A", "B", "C" };
        int i = 5;
        int j = 63;

        // Act
        var result = Eight.GetNextInstruction(i, instructions);
        var result2 = Eight.GetNextInstruction(j, instructions);

        //Assert
        result.Should().Be("R");
        result2.Should().Be("B");
    }

    [Test]
    public void Should_break_when_ZZZ_is_found()
    {
        // Arrange
        var instructions = new[] { "R", "L" };
        var nodes = new Dictionary<string, (string left, string right)> {
            {"AAA", ("BBB", "CCC")},
            {"BBB", ("DDD", "EEE")},
            {"CCC", ("ZZZ", "GGG")},
            {"DDD", ("DDD", "DDD")},
            {"EEE", ("EEE", "EEE")},
            {"GGG", ("GGG", "GGG")},
            {"ZZZ", ("ZZZ", "ZZZ") },
        };

        // Act
        var nodesVisited = Eight.IterateNodesUntilDone(instructions, nodes, "AAA", "ZZZ");

        //Assert
        nodesVisited.Should().BeEquivalentTo(new List<string>
        {
            "AAA",
            "CCC",
            "ZZZ"
        });
    }

    [Test]
    public void FindStartingNodes()
    {
        // Arrange
        var nodes = new Dictionary<string, (string left, string right)> {
            {"11A", ("11B", "XXX")},
            {"11B", ("XXX", "11Z")},
            {"11Z", ("11B", "XXX")},
            {"22A", ("22B", "XXX")},
            {"22B", ("22C", "22C")},
            {"22C", ("22Z", "22Z")},
            {"22Z", ("22B", "22B")},
            { "XXX", ("XXX", "XXX") },
        };

        // Act
        var result = Eight.FindStartingNodes(nodes);

        //Assert
        result.Should().BeEquivalentTo(new[]
        {
            "11A",
            "22A",
        });
    }

    [Test]
    public void FindEndingNodez()
    {
        // Arrange
        var nodes = new Dictionary<string, (string left, string right)> {
            {"11A", ("11B", "XXX")},
            {"11B", ("XXX", "11Z")},
            {"11Z", ("11B", "XXX")},
            {"22A", ("22B", "XXX")},
            {"22B", ("22C", "22C")},
            {"22C", ("22Z", "22Z")},
            {"22Z", ("22B", "22B")},
            { "XXX", ("XXX", "XXX") },
        };

        // Act
        var result = Eight.FindEndingNodes(nodes);

        //Assert
        result.Should().BeEquivalentTo(new[]
        {
            "11Z",
            "22Z",
        });
    }

    [Test]
    public void AllEndOnZ_should_be_false()
    {
        // Arrange
        var nodes = new[] { "AAZ", "AAB" };

        // Act
        var result = Eight.AllEndOnZ(nodes);

        //Assert
        result.Should().BeFalse();
    }

    [Test]
    public void AllEndOnZ_should_be_true()
    {
        // Arrange
        var nodes = new[] { "AAZ", "BBZ" };

        // Act
        var result = Eight.AllEndOnZ(nodes);

        //Assert
        result.Should().BeTrue();
    }

    //[Test]
    //public void Should_traverse_until_all_end_on_Z()
    //{
    //    // Arrange
    //    var instructions = new[] { "L", "R" };
    //    var startingNodes = new[] { "11A", "22A" };
    //    var nodes = new Dictionary<string, (string left, string right)> {
    //        {"11A", ("11B", "XXX")},
    //        {"11B", ("XXX", "11Z")},
    //        {"11Z", ("11B", "XXX")},
    //        {"22A", ("22B", "XXX")},
    //        {"22B", ("22C", "22C")},
    //        {"22C", ("22Z", "22Z")},
    //        {"22Z", ("22B", "22B")},
    //        { "XXX", ("XXX", "XXX") },
    //    };

    //    // Act
    //    var result = Eight.IterateNodesUntilDone(instructions, startingNodes, nodes);

    //    //Assert
    //    result.Should().Be(6);
    //}
}