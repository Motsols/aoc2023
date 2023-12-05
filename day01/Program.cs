using System.Text.RegularExpressions;

var input = File.ReadAllLines("input.txt").ToList();
Console.WriteLine(Environment.GetEnvironmentVariable("part") is "part1" ? Part1(input) : Part2(input));

int Part1(List<string> lines)
{
    return lines.Select(line =>
    {
        var values = new Regex(@"\d").Matches(line).Select(x => x.Value);
        return int.Parse($"{values.First()}{values.Last()}");
    }).Sum(); // 54630 :) 
}

int Part2(List<string> lines)
{
    return lines.Select(FindEdgeNumber).Sum(); // 54770 :)
}

int FindEdgeNumber(string line)
{
    var searchingFirst = true;
    var startWindow = 1;
    var startNumber = "0";
    do
    {
        (bool found, string number) = Search(line, 0, startWindow);
        if (found)
        {
            startNumber = number;
            searchingFirst = false;
        }
        startWindow++;
    }
    while (searchingFirst);

    var searchingLast = true;
    var endWindow = 1;
    var endNumber = "0";
    do
    {
        (bool found, string number) = Search(line, line.Length - endWindow, endWindow);
        if (found)
        {
            endNumber = number;
            searchingLast = false;
        }
        endWindow++;
    }
    while (searchingLast);

    return int.Parse(startNumber + endNumber);
}

(bool, string) Search(string line, int start, int windowSize)
{
    string window = line.Substring(start, windowSize);
    string number = "0";

    if (window.Contains("one") || window.Contains("1"))
        number = "1";
    if (window.Contains("two") || window.Contains("2"))
        number = "2";
    if (window.Contains("three") || window.Contains("3"))
        number = "3";
    if (window.Contains("four") || window.Contains("4"))
        number = "4";
    if (window.Contains("five") || window.Contains('5'))
        number = "5";
    if (window.Contains("six") || window.Contains("6"))
        number = "6";
    if (window.Contains("seven") || window.Contains("7"))
        number = "7";
    if (window.Contains("eight") || window.Contains("8"))
        number = "8";
    if (window.Contains("nine") || window.Contains("9"))
        number = "9";

    return (!number.Equals("0"), number);
}