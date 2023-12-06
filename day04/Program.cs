using System.Text.RegularExpressions;

var input = File.ReadAllLines("input.txt").ToList();
Console.WriteLine(Environment.GetEnvironmentVariable("part") is "part1" ? Part1(input) : await Part2(input));

int Part1(List<string> cards)
{
    return cards.Select(card =>
    {
        return ParseCard(card);
    }).Sum(); // 26346!
}

async Task<int> Part2(List<string> cards)
{
    var scratchCards = cards;
    var scratchCardsCount = cards.Count();
    var newCards = cards;

    do
    {
        List<string> bonusCards = new();
        foreach (var newCard in newCards)
        {
            bonusCards.AddRange(await ParseMovingCards(newCard, cards));
        }

        scratchCardsCount += bonusCards.Count();

        newCards = bonusCards;
        scratchCards.AddRange(bonusCards);
    }
    while (newCards.Any());

    return scratchCards.Count(); // 8467762
}

int ParseCard(string card)
{
    string[] activeCard = card.Split(":");
    var cardNumber = new Regex(@"\d+").Matches(activeCard[0]).Select(n => n.Value).First();
    var cardNumbers = activeCard[1].Split(" | ");
    var winningNumbers = new List<string>();
    var handNumbers = new List<string>();
    var foundNumbers = new List<string>();

    winningNumbers.AddRange(new Regex(@"\d+").Matches(cardNumbers[0]).Select(n => n.Value).ToList());
    handNumbers.AddRange(new Regex(@"\d+").Matches(cardNumbers[1]).Select(n => n.Value).ToList());

    int hits = 0;
    foreach (var winningNumber in winningNumbers)
    {
        if (handNumbers.Contains(winningNumber))
        {
            foundNumbers.Add(winningNumber);
            hits += 1;
        }
    }

    Console.WriteLine($"Card No: {cardNumber}, WinningNumbers: {string.Join(",", winningNumbers)}, HandNumber: {string.Join(",", handNumbers)}, FoundNumbers: {string.Join(",", foundNumbers)}, Hits: {hits}");

    if (hits == 0)
        return 0;

    var sum = hits > 0 ? 1 : 0;
    for (int i = 1; i < hits; i++)
        sum = sum * 2;
    return sum;
}

async Task<List<string>> ParseMovingCards(string card, List<string> cards)
{
    var addedCards = new List<string>();
    string[] activeCard = card.Split(":");
    var cardNumber = int.Parse(new Regex(@"\d+").Matches(activeCard[0]).Select(n => n.Value).First());
    var cardNumbers = activeCard[1].Split(" | ");
    var winningNumbers = new List<string>();
    var handNumbers = new List<string>();
    var foundNumbers = new List<string>();

    winningNumbers.AddRange(new Regex(@"\d+").Matches(cardNumbers[0]).Select(n => n.Value).ToList());
    handNumbers.AddRange(new Regex(@"\d+").Matches(cardNumbers[1]).Select(n => n.Value).ToList());

    int hits = 0;
    foreach (var winningNumber in winningNumbers)
    {
        if (handNumbers.Contains(winningNumber))
        {
            foundNumbers.Add(winningNumber);
            hits += 1;
        }
    }

    for (int i = 0; i < hits; i++)
    {
        var newCardNumber = cardNumber + i;
        if (newCardNumber < cards.Count())
            addedCards.Add(cards[cardNumber + i]);
    }

    return addedCards;
}