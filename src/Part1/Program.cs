var input = File.ReadAllLines("./input.txt");
var total = 0;
input[0].Split(',').ToList().ForEach(x => { total += GetHashValue(x);});
Console.WriteLine(total);

int GetHashValue(string stringValue)
{
    var hash = 0;
    foreach (var c in stringValue)
    {
        hash = ((((int)c+hash)*17)%256);
    }
    return hash;
}