using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lesson01
{
    public class DataLoader
    {
        public List<Row> LoadData(string path)
        {
            var temp = File.ReadLines(path)
                .Select(e => e.Replace(',', ';'))
                .Select(e => e.Replace('.', ','))
                .Select(line => line.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
                .Where(e => e.Length > 0);
            return temp.Select((line, i) => new Row
            {
                Id = i+1,
                SepalLength = double.Parse(line[0]),
                SepalWidth = double.Parse(line[1]),
                PetalLength = double.Parse(line[2]),
                PetalWidth = double.Parse(line[3]),
                IrisType = line[4] == "Iris-setosa" ? IrisType.Setosa :
                        line[4] == "Iris-versicolor" ? IrisType.Versicolor : IrisType.Virginica
            })
                .ToList();
        }
    }
}
