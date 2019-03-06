using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Lesson04
{
    public class DataLoader
    {
        public List<Row> LoadData(string path)
        {
            var lines = File.ReadLines(path)
                .Select(e => e.Replace(',', ';'))
                .Select(e => e.Replace('.', ','))
                .Select(line => line.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
                .Where(e => e.Length > 0);

            var numberFormat = new NumberFormatInfo { NumberDecimalSeparator = "," };
            return lines.Select((line, i) => new Row
            {
                Id = i + 1,
                SepalLength = double.Parse(line[0], numberFormat),
                SepalWidth = double.Parse(line[1], numberFormat),
                PetalLength = double.Parse(line[2], numberFormat),
                PetalWidth = double.Parse(line[3], numberFormat),
                IrisType = line[4] == "Iris-setosa" ? IrisType.Setosa :
                        line[4] == "Iris-versicolor" ? IrisType.Versicolor : IrisType.Virginica
            })
                .ToList();
        }
    }
}
