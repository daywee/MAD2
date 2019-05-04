using System.Collections.Generic;
using System.Linq;

namespace FinalProject.NetworkAnalysis.CommunityDetection
{
    public class Community
    {
        public List<Node> Nodes { get; set; }

        public Community(IEnumerable<Node> nodes)
        {
            Nodes = nodes.ToList();
        }
    }
}
