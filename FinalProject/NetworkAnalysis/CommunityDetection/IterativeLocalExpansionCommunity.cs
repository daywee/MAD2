using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalProject.NetworkAnalysis.CommunityDetection
{
    class IterativeLocalExpansionCommunity
    {
        public List<Node> D { get; set; } = new List<Node>(); // core + boundary
        public List<Node> B { get; set; } = new List<Node>(); // boundary
        public List<Node> C { get; set; } = new List<Node>(); // core
        public List<Node> S { get; set; } = new List<Node>(); // shell

        public IterativeLocalExpansionCommunity(Node startNode)
        {
            AddNodeToD(startNode);
        }

        private IterativeLocalExpansionCommunity(IterativeLocalExpansionCommunity community)
        {
            D = community.D.ToList();
            B = community.B.ToList();
            C = community.C.ToList();
            S = community.S.ToList();
        }

        public void AddNodeToD(Node node)
        {
            if (D.Contains(node))
                throw new Exception($"D already contains '{node.Id}'");

            D.Add(node);
            S.Remove(node);
            S.AddRange(node.Neighbors.Except(S).Except(D));
            B.Add(node);

            foreach (var b in B.ToList())
            {
                if (!b.Neighbors.Intersect(S).Any())
                {
                    B.Remove(b);
                    C.Add(b);
                }
            }
        }

        public double ComputeRWithAddedNode(Node node)
        {
            var tempCommunity = new IterativeLocalExpansionCommunity(this);
            tempCommunity.AddNodeToD(node);
            return tempCommunity.ComputeR();
        }

        public double ComputeR()
        {
            int bInEdge = B.Select(e => e.Neighbors.Intersect(D).Count()).Sum();
            int bOutEdge = B.Select(e => e.Neighbors.Intersect(S).Count()).Sum();

            double R = (double)bInEdge / (bOutEdge + bInEdge);

            return R;
        }
    }
}
