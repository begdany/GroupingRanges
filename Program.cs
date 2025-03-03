string inputData = "1-3; 5-8; 2-4";

Table table = new Table();
table.AddGroup(0);

string[] dataSets = inputData.Split("; ");
foreach (var dataSet in dataSets)
{
    string[] bounds = dataSet.Split('-');

    int start = int.Parse(bounds[0]);
    int end = int.Parse(bounds[1]);

    table.GetGroup(0).AddRange(new Range(start, end));
}

table.AddGroup(1);
Range currentRange = table.GetGroup(0).GetRange(1);
table.GetGroup(1).AddRange(currentRange);
int end222 = table.GetGroup(1).GetRange(0).End;

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

class Group
{
    public List<Range> Ranges { get; set; }

    public Group()
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

class Table
{
    private Dictionary<int, Group> Groups { get; set; }

    public Table()
    {
        Groups = new Dictionary<int, Group>();
    }

    public void AddGroup(int groupID)
    {
        Groups[groupID] = new Group();
    }

    public Group GetGroup(int groupID)
    {
        return Groups[groupID];
    }
}