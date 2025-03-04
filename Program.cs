using System.Text.RegularExpressions;

string inputData = "1-3; 5-8; 2-4";

Table table = new Table();

TableManager.ReadData(table, inputData);
TableManager.DistributeRanges(table);
TableManager.PrintTable(table);

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
    public static void ReadData (Table table, string data)
    {
        string[] dataSets = data.Split("; ");
        foreach (var dataSet in dataSets)
        {
            string[] bounds = dataSet.Split('-');
            int start = int.Parse(bounds[0]);
            int end = int.Parse(bounds[1]);
            table.GetGroup(0).AddRange(new Range(start, end));
        }
    }

    public static void ShiftFirstRange (Table table)
    {
        table.AddGroup(1);
        table.GetGroup(1).AddRange(table.GetGroup(0).GetRange(0));
    }

    public static void DistributeRanges (Table table)
    {
        ShiftFirstRange(table);
        foreach (Range range1 in table.GetGroup(0).Ranges.Skip(1))
        {
            bool interrupt = false;
            foreach (Group group in table.Groups.Values.Skip(1))
            {
                interrupt = false;
                foreach (Range range2 in group.Ranges)
                {
                    if (!(range1.Start > range2.End || range1.End < range2.Start))
                    {
                        interrupt = true;
                        break;
                    }
                }
                if (!interrupt)
                {
                    group.AddRange(range1);
                    break;
                }
            }
            if (interrupt)
            {
                int nextGroupID = table.Groups.Keys.Max() + 1;
                table.AddGroup(nextGroupID);
                table.GetGroup(nextGroupID).AddRange(range1);
            }
        }
    }

    public static void PrintTable (Table table)
    {
        int groupID = 0;
        foreach (Group group in table.Groups.Values.Skip(1))
        {
            Console.Write($"{++groupID} группа: ");
            foreach (Range range in group.Ranges)
            {
                Console.Write($"{range.Start}-{range.End}; ");
            }
            Console.Write("\n");
        }
    }
}