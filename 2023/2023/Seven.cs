using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("2023.Tests")]

public class Seven : BaseLevel, ILevel
{

    public string SolveA()
    {
        var hands = Parse(GetInput());

        var highCard = hands.Where(h => IsHighCard(h.Item1)).ToList();
        var onePair = hands.Where(h => IsOnePair(h.Item1)).ToList();
        var twoPair = hands.Where(h => IsTwoPair(h.Item1)).ToList();
        var threeOfAKind = hands.Where(h => IsThreeOfAKind(h.Item1)).ToList();
        var fullHouse = hands.Where(h => IsFullHouse(h.Item1)).ToList();
        var fourOfAKind = hands.Where(h => IsFourOfAKind(h.Item1)).ToList();
        var fiveOfAKind = hands.Where(h => IsFiveOfAKind(h.Item1)).ToList();

        highCard.Sort(SortSameType);
        onePair.Sort(SortSameType);
        twoPair.Sort(SortSameType);
        threeOfAKind.Sort(SortSameType);
        fullHouse.Sort(SortSameType);
        fourOfAKind.Sort(SortSameType);
        fiveOfAKind.Sort(SortSameType);


        var ordered =
            highCard
            .Concat(onePair)
            .Concat(twoPair)
            .Concat(threeOfAKind)
            .Concat(fullHouse)
            .Concat(fourOfAKind)
            .Concat(fiveOfAKind)
            .ToList();

        var result = Calculate(ordered);

        return result.ToString();
    }


    public string SolveB()
    {
        var hands = Parse(GetInput());

        var fiveOfAKind = hands.Where(h => IsFiveOfAKind(h.Item1, true)).ToList();
        var fourOfAKind = hands.Where(h => IsFourOfAKind(h.Item1, true) && !fiveOfAKind.Contains(h)).ToList();
        var fullHouse = hands.Where(h => IsFullHouse(h.Item1, true) && !fourOfAKind.Contains(h)).ToList();
        var threeOfAKind = hands.Where(h => IsThreeOfAKind(h.Item1, true) && !fullHouse.Contains(h)).ToList();
        var twoPair = hands.Where(h => IsTwoPair(h.Item1, true) && !threeOfAKind.Contains(h)).ToList();
        var onePair = hands.Where(h => IsOnePair(h.Item1, true) && !twoPair.Contains(h)).ToList();
        var highCard = hands.Where(h => IsHighCard(h.Item1, true) && !onePair.Contains(h)).ToList();

        highCard.Sort(SortSameTypeWithJoker);
        onePair.Sort(SortSameTypeWithJoker);
        twoPair.Sort(SortSameTypeWithJoker);
        threeOfAKind.Sort(SortSameTypeWithJoker);
        fullHouse.Sort(SortSameTypeWithJoker);
        fourOfAKind.Sort(SortSameTypeWithJoker);
        fiveOfAKind.Sort(SortSameTypeWithJoker);


        var ordered =
            highCard
            .Concat(onePair)
            .Concat(twoPair)
            .Concat(threeOfAKind)
            .Concat(fullHouse)
            .Concat(fourOfAKind)
            .Concat(fiveOfAKind)
            .Distinct()
            .ToList();

        var result = Calculate(ordered);

        return result.ToString();
    }


    internal static bool IsFiveOfAKind(string hand) => IsFiveOfAKind(hand, false);
    internal static bool IsFiveOfAKind(string hand, bool joker)
    {
        return GetCharToCountMap(hand, joker).Any(c => c.Count == 1);
    }
    internal static bool IsFourOfAKind(string hand) => IsFourOfAKind(hand, false);
    internal static bool IsFourOfAKind(string hand, bool joker)
    {
        return GetCharToCountMap(hand, joker).Any(m => m.Any(h => h.Value == 4));
    }

    internal static bool IsFullHouse(string hand) => IsFullHouse(hand, false);
    internal static bool IsFullHouse(string hand, bool joker)
    {
        var map = GetCharToCountMap(hand, joker).Select(m => m.OrderBy(h => h.Value).ToArray()).ToArray();

        return map.Any(m => m.Count() == 2 && m.First().Value == 2 && m[1].Value == 3);
    }

    internal static bool IsThreeOfAKind(string hand) => IsThreeOfAKind(hand, false);

    internal static bool IsThreeOfAKind(string hand, bool joker)
    {
        var map = GetCharToCountMap(hand, joker).Select(m => m.OrderByDescending(h => h.Value)).ToList();

        return map.Any(m => m.Count() > 2 && m.First().Value == 3);
    }

    internal static bool IsTwoPair(string hand) => IsTwoPair(hand, false);

    internal static bool IsTwoPair(string hand, bool joker)
    {
        var map = GetCharToCountMap(hand, joker).Select(m => m.OrderByDescending(h => h.Value).ToArray()).ToArray();

        return map.Any(m => m.Count() == 3 && m[0].Value == 2 && m[1].Value == 2);
    }

    internal static bool IsOnePair(string hand) => IsOnePair(hand, false);

    internal static bool IsOnePair(string hand, bool joker)
    {
        var map = GetCharToCountMap(hand, joker).Select(m => m.OrderByDescending(h => h.Value).ToArray()).ToArray();

        return map.Any(m => m.Count() == 4 && m[0].Value == 2);
    }

    internal static bool IsHighCard(string hand) => IsHighCard(hand, false);
    internal static bool IsHighCard(string hand, bool joker)
    {
        return GetCharToCountMap(hand, joker).Any(m => m.Count == 5);
    }
    internal static bool HasJoker(string hand) => hand.Contains('J') && hand != "JJJJJ";


    private static List<Dictionary<char, int>> GetCharToCountMap(string word, bool joker)
    {
        var map = word
            .ToCharArray()
            .Aggregate(new Dictionary<char, int>(), (total, aggregate) =>
            {
                if (total.ContainsKey(aggregate))
                {
                    total[aggregate]++;
                }
                else
                {
                    total.Add(aggregate, 1);
                }

                return total;
            });

        if (!joker || !HasJoker(word))
        {
            return [map];
        }

        return map.Where(m => m.Key != 'J').Select(m =>
        {
            var d = new Dictionary<char, int>(map);
            d[m.Key] += d['J'];
            d.Remove('J');

            return d;
        }).ToList();

    }

    internal static int SortSameType((string, int) hand, (string, int) hand2) => SortSameType(hand, hand2, false);
    internal static int SortSameTypeWithJoker((string, int) hand, (string, int) hand2) => SortSameType(hand, hand2, true);


    private static int SortSameType((string, int) hand, (string, int) hand2, bool joker)
    {
        var values = joker
            ? ['J', '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'Q', 'K', 'A']
            : new[] { '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A' };

        for (var i = 0; i < hand.Item1.Length; i++)
        {
            if (hand.Item1[i] == hand2.Item1[i])
            {
                continue;
            }

            return
                Array
                .IndexOf(values, hand.Item1[i])
                .CompareTo(Array
                    .IndexOf(values, hand2.Item1[i]));
        }

        return 0;
    }

    internal static int Calculate(List<(string, int)> result) => result.Select((hand, index) => hand.Item2 * (index + 1)).Aggregate(0, (total, next) => next + total);

    internal static List<(string, int)> Parse(string[] lines) => lines.Select(line => (line.Split(" ")[0], Convert.ToInt32(line.Split(" ")[1]))).ToList();
}



