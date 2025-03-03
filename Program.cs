string inputData = "1-3; 5-8; 2-4";

GroupManager groupManager = new GroupManager();
groupManager.AddGroup(0);

string[] dataSets = inputData.Split("; ");
foreach (var dataSet in dataSets)
{
    string[] bounds = dataSet.Split('-');

    int start = int.Parse(bounds[0]);
    int end = int.Parse(bounds[1]);

    groupManager.GetGroup(0).AddRange(new Range(start, end));
}

groupManager.AddGroup(1);
Range currentRange = groupManager.GetGroup(0).GetRange(1);
groupManager.GetGroup(1).AddRange(currentRange);

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

class GroupOfRanges
{
    public List<Range> Ranges { get; set; }

    public GroupOfRanges()
    {
        Ranges = new List<Range>();
    }

    public void AddRange(Range range)
    {
        Ranges.Add(range);
    }

    public Range GetRange(int rangeID)
    {
        return Ranges[rangeID];
    }
}

class GroupManager
{
    private Dictionary<int, GroupOfRanges> Groups { get; set; }

    public GroupManager()
    {
        Groups = new Dictionary<int, GroupOfRanges>();
    }

    public void AddGroup(int groupID)
    {
        Groups[groupID] = new GroupOfRanges();
    }

    public GroupOfRanges GetGroup(int groupID)
    {
        return Groups[groupID];
    }
}