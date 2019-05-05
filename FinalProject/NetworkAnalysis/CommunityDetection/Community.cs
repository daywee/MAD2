using System.Collections.Generic;
using System.Linq;

namespace FinalProject.NetworkAnalysis.CommunityDetection
{
    public class Community
    {
        public int Id { get; set; }
        public List<Node> Nodes { get; set; }

        public Community(int id, IEnumerable<Node> nodes)
        {
            Id = id;
            Nodes = nodes.ToList();
        }
    }
}
