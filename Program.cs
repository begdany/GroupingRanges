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
Range range1 = table.GetGroup(0).GetRange(0);
Range range2 = table.GetGroup(0).GetRange(0);
table.GetGroup(1).AddRange(range1);
int x01 = 0;
int x11 = 0;
int x02 = 0;
int x12 = 0;
int numRanges = table.GetGroup(0).Ranges.Count;
for (int n = 0; n < numRanges; n++)
{
    range2 = table.GetGroup(0).GetRange(n);
    x02 = range2.Start;
    x12 = range2.End;
    int numGroupsOnTable = table.Groups.Count - 1;
    int j = 1;
    while (j <= numGroupsOnTable)
    {
        int numRangeInGroup = table.GetGroup(j).Ranges.Count;
        for (int i = 0; i < numRangeInGroup; i++)
        {
            range1 = table.GetGroup(j).GetRange(i);
            x01 = range1.Start;
            x11 = range1.End;
            if (!(x01 > x12 || x11 < x02))
            {
                j++;
                table.AddGroup(j);
                break;
            }
        }
    }
    table.GetGroup(j).AddRange(range2);
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
    public Dictionary<int, Group> Groups { get; set; }

    public Table()
    {
        Groups = new Dictionary<int, Group>();
        AddGroup(0);
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

class TableManager
{

}