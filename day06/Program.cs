using System.Text.RegularExpressions;

var input = File.ReadAllLines("input.txt").ToList();
Console.WriteLine(Environment.GetEnvironmentVariable("part") is "part1" ? Part1(input) : Part2(input));

int Part1(List<string> input)
{
    var times = new Regex(@"\d+").Matches(input[0]).Select(n => int.Parse(n.Value)).ToList();
    var records = new Regex(@"\d+").Matches(input[1]).Select(n => int.Parse(n.Value)).ToList();
    var pairs = new List<(int time, int record)>();
    for (int i = 0; i < times.Count(); i++)
    {
        pairs.Add((times[i], records[i]));
    }

    var results = pairs.Select(x => GetTimesBeatingRecord(x.time, x.record)).ToList();
    var sum = results[0];
    for (int i = 1; i < results.Count(); i++)
    {
        sum = sum * results[i];
    }

    return sum; // 138915
}

int Part2(List<string> input)
{
    var time = long.Parse(input[0].Replace("Time:", "").Replace(" ", ""));
    var record = long.Parse(input[1].Replace("Distance:", "").Replace(" ", ""));

    return GetTimesBeatingRecord(time, record); // 27340847
}

int GetTimesBeatingRecord(long time, long record)
{
    int timesRecordBeaten = 0;

    for (long i = 0; i < time; i++)
    {
        var speed = i;
        var raceTime = time - i;
        var distance = speed * raceTime;
        if (distance > record)
            timesRecordBeaten++;
    }

    return timesRecordBeaten;
}