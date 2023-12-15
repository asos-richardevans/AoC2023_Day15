var input = File.ReadAllLines("./input.txt");
var total = 0;
var boxes = new Dictionary<int, List<(string, int)>>();
input[0].Split(',').ToList().ForEach(x =>
{
    int box;
    if(x.Contains("="))
    {
        var values = x.Split('=');
        box = GetHashValue(values[0]);
        var value = int.Parse(values[1]);
        if (boxes.ContainsKey(box))
        {
            if (boxes[box].Any(y=>y.Item1 == values[0]))
            {
                var idx = boxes[box].IndexOf(boxes[box].First(z => z.Item1 == values[0]));
                boxes[box].RemoveAt(idx);
                boxes[box].Insert(idx, (values[0], value));
            }
            else
            {
                boxes[box].Add((values[0], value));
            }
        }
        else
        {
            boxes.Add(box, new List<(string, int)> { (values[0], value) });
        }
    }else if (x.Contains('-'))
    {
        var values = x.Split('-');
        box = GetHashValue(values[0]);
        if (boxes.ContainsKey(box))
        {
            if (boxes[box].Any(a => a.Item1 == values[0]))
            {
                var idx = boxes[box].IndexOf(boxes[box].First(b => b.Item1 == values[0]));
                boxes[box].RemoveAt(idx);
                if (boxes[box].Count == 0) { boxes.Remove(box); }
            }
        }
    }
});

foreach (var box in boxes)
{
    var boxNumber = box.Key + 1;
    for (int i = 1; i <= boxes[box.Key].Count; i++)
    {
        total += boxNumber * i * boxes[box.Key][i - 1].Item2;
    }
}

Console.WriteLine(total);

int GetHashValue(string stringInput)
{
    var hash = 0;
    foreach (var c in stringInput)
    {
        hash = ((((int)c + hash) * 17) % 256);
    }
    return hash;
}