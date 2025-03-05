// Входные данные с отрезками
string inputData = "1-3; 5-8; 2-4; 7-9";

// Создаем 
Table table = new Table();

TableManager.ReadData(table, inputData);
TableManager.DistributeRanges(table);
TableManager.PrintTable(table);

// Класс отрезок, содержит данные начала и конца отрезка
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

// Класс группа, содержит отрезки
class Group
{
    public List<Range> Ranges { get; set; }

    public Group()
    {
        Ranges = new List<Range>();
    }

    // Метод добавления нового отрезка
    public void AddRange(Range range)
    {
        Ranges.Add(range);
    }

    // Метод получения данных отрезка по его номеру в группе
    public Range GetRange(int rangeID)
    {
        return Ranges[rangeID];
    }
}

// Класс стол, содержит группы отрезков
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

// Класс менеджер стола, содержит основной алгоритм программы
class TableManager
{
    // Метод чтения данных. Считываем данные из строки (отрезки) и помещаем их в 0 группу на столе
    public static void ReadData(Table table, string data)
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

    // Метод переноса первого отрезка в первую группу
    public static void ShiftFirstRange(Table table)
    {
        table.AddGroup(1);
        table.GetGroup(1).AddRange(table.GetGroup(0).GetRange(0));
    }

    // Метод распределения отрезков по группам
    public static void DistributeRanges(Table table)
    {
        ShiftFirstRange(table);
        bool interrupt = false; // Флаг прерывания, обозначающий, что выбранный отрезок пересекается с каким то отрезком группы
        // Пробегаем по каждому отрезку группы 0, пропускаем первый отрезок
        foreach (Range range1 in table.GetGroup(0).Ranges.Skip(1))
        {
            // Пробегаем по каждой группе, пропускаем группу 0
            foreach (Group group in table.Groups.Values.Skip(1))
            {
                interrupt = false; // Сбрасываем флаг при переходе на новую группу
                // Пробегаем по каждому отрезку выбранной группы 
                foreach (Range range2 in group.Ranges)
                {
                    // Если находим пересечение хотя бы с одним отрезком группы
                    if (!(range1.Start > range2.End || range1.End < range2.Start))
                    {
                        interrupt = true; // Поднимае флаг
                        break; // Прерываем цикл
                    }
                }
                // Если пересечения с отрезками группы не было
                if (!interrupt)
                {
                    group.AddRange(range1); // Записываем выбранный отрезок в текущую группу
                    break; // Прерываем цикл
                }
            }
            // Если в последней группе таблицы было пересечение отрезков
            // Создаем новую группу и записываем в нее новый отрезок
            if (interrupt)
            {
                int nextGroupID = table.Groups.Keys.Max() + 1;
                table.AddGroup(nextGroupID);
                table.GetGroup(nextGroupID).AddRange(range1);
                interrupt = false; // Сбрасываем флаг
            }
        }
    }

    // Метод вывода распределенных отрезков по группам
    public static void PrintTable(Table table)
    {
        int groupID = 0;
        foreach (Group group in table.Groups.Values.Skip(1))
        {
            Console.Write($"{++groupID} группа: ");
            foreach (Range range in group.Ranges)
            {
                Console.Write($"{range.Start}-{range.End}; ");
            }
            Console.WriteLine();
        } 
    }
}