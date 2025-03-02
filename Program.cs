List<Range> ranges = new List<Range>();

string inputData = "1-3; 5-8; 2-4";

string[] dataSets = inputData.Split("; ");
foreach (var dataSet in dataSets)
{
    string[] bounds = dataSet.Split('-');

    int start = int.Parse(bounds[0]);
    int end = int.Parse(bounds[1]);

    ranges.Add(new Range(start, end));
}

foreach (var range in ranges)
{
    Console.WriteLine($"Start: {range.Start}, End: {range.End}");
}

class Range
{
    public int Start { get; set; }
    public int End { get; set; }

    public Range(int start, int end)
    {
        Start = start;
        End = end;
    }
}