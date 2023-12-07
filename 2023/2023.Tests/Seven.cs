namespace _2023.Tests;

public class SevenTests
{

    [TestCase("AAAAA", true)]
    [TestCase("AAAAB", false)]
    [TestCase("JJJJJ", true)]
    public void IsFiveOfAKind(string hand, bool expectedResult)
    {
        //Arrange
        //Act
        var result = Seven.IsFiveOfAKind(hand);

        result.Should().Be(expectedResult);
    }

    [TestCase("AAAAA", true)]
    [TestCase("AAAAB", false)]
    [TestCase("AAAAJ", true)]
    [TestCase("JJJJJ", true)]
    public void IsFiveOfAKind_with_joker(string hand, bool expectedResult)
    {
        //Arrange
        //Act
        var result = Seven.IsFiveOfAKind(hand, true);

        result.Should().Be(expectedResult);
    }

    [TestCase("AAAAA", false)]
    [TestCase("AAAAB", true)]
    [TestCase("AAABB", false)]
    [TestCase("JAAAB", true)]
    public void IsFourOfAKind(string hand, bool expectedResult)
    {
        //Arrange
        //Act
        var result = Seven.IsFourOfAKind(hand, true);

        result.Should().Be(expectedResult);
    }

    [TestCase("AAAAA", false)]
    [TestCase("AAAAB", false)]
    [TestCase("AAABB", true)]
    [TestCase("AAJBB", true)]
    public void IsFullHouse(string hand, bool expectedResult)
    {
        //Arrange
        //Act
        var result = Seven.IsFullHouse(hand, true);

        result.Should().Be(expectedResult);
    }

    [TestCase("AAAAA", false)]
    [TestCase("AAAAB", false)]
    [TestCase("AAABB", false)]
    [TestCase("AAABC", true)]
    [TestCase("AAJBB", false)]
    public void IsThreeOfAKind(string hand, bool expectedResult)
    {
        //Arrange
        //Act
        var result = Seven.IsThreeOfAKind(hand);

        result.Should().Be(expectedResult);
    }

    [TestCase("AAAAA", false)]
    [TestCase("AAAAB", false)]
    [TestCase("AAABB", false)]
    [TestCase("AAABC", false)]
    [TestCase("AAABC", false)]
    [TestCase("AABBC", true)]
    public void IsTwoPair(string hand, bool expectedResult)
    {
        //Arrange
        //Act
        var result = Seven.IsTwoPair(hand);

        result.Should().Be(expectedResult);
    }

    [TestCase("AAAAA", false)]
    [TestCase("AAAAB", false)]
    [TestCase("AAABB", false)]
    [TestCase("AAABC", false)]
    [TestCase("AAABC", false)]
    [TestCase("AABBC", false)]
    [TestCase("AABCD", true)]
    public void IsOnePair(string hand, bool expectedResult)
    {
        //Arrange
        //Act
        var result = Seven.IsOnePair(hand);

        result.Should().Be(expectedResult);
    }

    [TestCase("AAAAA", false)]
    [TestCase("AAAAB", false)]
    [TestCase("AAABB", false)]
    [TestCase("AAABC", false)]
    [TestCase("AAABC", false)]
    [TestCase("ABCDE", true)]
    [TestCase("AABCD", false)]
    public void IsHighCard(string hand, bool expectedResult)
    {
        //Arrange
        //Act
        var result = Seven.IsHighCard(hand);

        result.Should().Be(expectedResult);
    }

    [Test]
    public void SortSameType()
    {
        // Arrange
        var hands = new[] { ("2AAAA", 1), ("33332", 1) }.ToList();
        var hands2 = new[] { ("77888", 1), ("77788", 1) }.ToList();

        // Act
        hands.Sort(Seven.SortSameType);
        hands2.Sort(Seven.SortSameType);

        // Assert
        hands.Should().BeEquivalentTo(new[] { ("2AAAA", 1), ("33332", 1) }, options => options.WithStrictOrdering());
        hands2.Should().BeEquivalentTo(new[] { ("77788", 1), ("77888", 1) }, options => options.WithStrictOrdering());
    }

    [Test]
    public void AllSelectingWorks()
    {
        // Arrange
        var hands = new List<(string, int)>() {
            ("32T3K", 765),
            ("T55J5", 684),
            ("KK677", 28),
            ("KTJJT", 220),
            ("QQQJA", 483)
        };

        // Act
        var fiveOfAKind = hands.Where(h => Seven.IsFiveOfAKind(h.Item1)).ToList();
        var fourOfAKind = hands.Where(h => Seven.IsFourOfAKind(h.Item1)).ToList();
        var fullHouse = hands.Where(h => Seven.IsFullHouse(h.Item1)).ToList();
        var threeOfAKind = hands.Where(h => Seven.IsThreeOfAKind(h.Item1)).ToList();
        var twoPair = hands.Where(h => Seven.IsTwoPair(h.Item1)).ToList();
        var onePair = hands.Where(h => Seven.IsOnePair(h.Item1)).ToList();
        var highCard = hands.Where(h => Seven.IsHighCard(h.Item1)).ToList();


        var result =
            fiveOfAKind
            .Concat(fourOfAKind)
            .Concat(fullHouse)
            .Concat(threeOfAKind)
            .Concat(twoPair)
            .Concat(onePair)
            .Concat(highCard)
            .ToList().Count;

        result.Should().Be(hands.Count);
    }



    [Test]
    public void SortByTypeAndValue()
    {
        // Arrange
        var hands = new List<(string, int)>() {
            ("32T3K", 765),
            ("T55J5", 684),
            ("KK677", 28),
            ("KTJJT", 220),
            ("QQQJA", 483)
        };

        // Act
        var fiveOfAKind = hands.Where(h => Seven.IsFiveOfAKind(h.Item1)).ToList();
        var fourOfAKind = hands.Where(h => Seven.IsFourOfAKind(h.Item1)).ToList();
        var fullHouse = hands.Where(h => Seven.IsFullHouse(h.Item1)).ToList();
        var threeOfAKind = hands.Where(h => Seven.IsThreeOfAKind(h.Item1)).ToList();
        var twoPair = hands.Where(h => Seven.IsTwoPair(h.Item1)).ToList();
        var onePair = hands.Where(h => Seven.IsOnePair(h.Item1)).ToList();
        var highCard = hands.Where(h => Seven.IsHighCard(h.Item1)).ToList();

        fiveOfAKind.Sort(Seven.SortSameType);
        fourOfAKind.Sort(Seven.SortSameType);
        fullHouse.Sort(Seven.SortSameType);
        threeOfAKind.Sort(Seven.SortSameType);
        twoPair.Sort(Seven.SortSameType);
        onePair.Sort(Seven.SortSameType);
        highCard.Sort(Seven.SortSameType);


        var result =
            highCard
            .Concat(onePair)
            .Concat(twoPair)
            .Concat(threeOfAKind)
            .Concat(fourOfAKind)
            .Concat(fullHouse)
            .Concat(fiveOfAKind)
            .ToList();

        // Assert
        result.Should().BeEquivalentTo(new List<(string, int)>() {
            ("32T3K", 765),
            ("KTJJT", 220),
            ("KK677", 28),
            ("T55J5", 684),
            ("QQQJA", 483)
        }, options => options.WithStrictOrdering());
    }

    [Test]
    public void Calculate()
    {
        // Arrange
        var hands = new List<(string, int)>() {
            ("32T3K", 765),
            ("KTJJT", 220),
            ("KK677", 28),
            ("T55J5", 684),
            ("QQQJA", 483)
        };

        // Act
        var result = Seven.Calculate(hands);

        // Assert
        result.Should().Be(6440);
    }

    [Test]
    public void Parse()
    {
        // Arrange
        var lines = new[]
        {
            "32T3K 765",
            "T55J5 684",
            "KK677 28",
            "KTJJT 220",
            "QQQJA 483",
        };

        // Act
        var result = Seven.Parse(lines);

        // Assert
        result.Should().BeEquivalentTo(new List<(string, int)>() {
            ("32T3K", 765),
            ("KTJJT", 220),
            ("KK677", 28),
            ("T55J5", 684),
            ("QQQJA", 483)
        });
    }




    [Test]
    public void SortByTypeAndValue_WithJoker()
    {
        // Arrange
        var hands = new List<(string, int)>() {
            ("32T3K", 765),
            ("T55J5", 684),
            ("KK677", 28),
            ("KTJJT", 220),
            ("QQQJA", 483)
        };
        var joker = true;

        // Act
        var fiveOfAKind = hands.Where(h => Seven.IsFiveOfAKind(h.Item1, joker)).ToList();
        var fourOfAKind = hands.Where(h => Seven.IsFourOfAKind(h.Item1, joker) && !fiveOfAKind.Contains(h)).ToList();
        var fullHouse = hands.Where(h => Seven.IsFullHouse(h.Item1, joker) && !fourOfAKind.Contains(h)).ToList();
        var threeOfAKind = hands.Where(h => Seven.IsThreeOfAKind(h.Item1, joker) && !fullHouse.Contains(h)).ToList();
        var twoPair = hands.Where(h => Seven.IsTwoPair(h.Item1, joker) && !threeOfAKind.Contains(h)).ToList();
        var onePair = hands.Where(h => Seven.IsOnePair(h.Item1, joker) && !twoPair.Contains(h)).ToList();
        var highCard = hands.Where(h => Seven.IsHighCard(h.Item1, joker) && !onePair.Contains(h)).ToList();

        fiveOfAKind.Sort(Seven.SortSameTypeWithJoker);
        fourOfAKind.Sort(Seven.SortSameTypeWithJoker);
        fullHouse.Sort(Seven.SortSameTypeWithJoker);
        threeOfAKind.Sort(Seven.SortSameTypeWithJoker);
        twoPair.Sort(Seven.SortSameTypeWithJoker);
        onePair.Sort(Seven.SortSameTypeWithJoker);
        highCard.Sort(Seven.SortSameTypeWithJoker);


        var result =
            highCard
            .Concat(onePair)
            .Concat(twoPair)
            .Concat(threeOfAKind)
            .Concat(fourOfAKind)
            .Concat(fullHouse)
            .Concat(fiveOfAKind)
            .ToList();

        // Assert
        result.Should().BeEquivalentTo(new List<(string, int)>() {
            ("32T3K", 765),
            ("KK677", 28),
            ("T55J5", 684),
            ("QQQJA", 483),
            ("KTJJT", 220),
        }, options => options.WithStrictOrdering());
    }

    [Test]
    public void Calculate_B()
    {
        // Arrange
        var hands = new List<(string, int)>() {
            ("32T3K", 765),
            ("KK677", 28),
            ("T55J5", 684),
            ("QQQJA", 483),
            ("KTJJT", 220),
        };

        // Act
        var result = Seven.Calculate(hands);

        // Assert
        result.Should().Be(5905);
    }
}