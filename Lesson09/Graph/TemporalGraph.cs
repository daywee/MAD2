using System.Collections.Generic;
using System.Linq;

namespace Lesson09.Graph
{
    public class TemporalGraph
    {
        public List<int> VertexIds { get; }
        public int MinTime { get; }
        public int MaxTime { get; }
        public List<(int time, int v1, int v2)> EdgeList { get; }

        public TemporalGraph(List<(int time, int v1, int v2)> edgeList)
        {
            VertexIds = edgeList
                .Select(e => e.v1)
                .Concat(edgeList.Select(e => e.v2))
                .Distinct()
                .OrderBy(e => e)
                .ToList();

            MinTime = edgeList.Min(e => e.time);
            MaxTime = edgeList.Max(e => e.time);

            EdgeList = edgeList;
        }
    }
}
