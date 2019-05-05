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

        public int BIn { get; set; }
        public int BOut { get; set; }

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

            BIn = community.BIn;
            BOut = community.BOut;
        }

        public void AddNodeToD(Node node)
        {
            if (D.Contains(node))
                throw new Exception($"D already contains '{node.Id}'");

            D.Add(node);
            S.Remove(node);
            S.AddRange(node.Neighbors.Except(S).Except(D));
            B.Add(node);

            BIn += D.Intersect(node.Neighbors).Count();
            BIn += B.Intersect(node.Neighbors).Count();
            BOut += node.Neighbors.Except(node.Neighbors.Intersect(D)).Count();
            BOut -= node.Neighbors.Intersect(D).Count();

            // only nodes in B can be affected
            var nodesInB = node.Neighbors.Intersect(B).ToList();

            int noEdgesRemoved = 0;
            foreach (var b in nodesInB)
            {
                if (!b.Neighbors.Intersect(S).Any())
                {
                    B.Remove(b);
                    C.Add(b);
                    noEdgesRemoved += b.Neighbors.Intersect(D).Count();
                }
            }

            BIn -= noEdgesRemoved;
        }

        public double ComputeRWithAddedNode(Node node)
        {
            var tempCommunity = new IterativeLocalExpansionCommunity(this);
            tempCommunity.AddNodeToD(node);
            return tempCommunity.ComputeR();
        }

        public double ComputeR()
        {
            //int bInEdge = B.Select(e => e.Neighbors.Intersect(D).Count()).Sum();
            //int bOutEdge = B.Select(e => e.Neighbors.Intersect(S).Count()).Sum();

            //double R = (double)bInEdge / (bOutEdge + bInEdge);

            double R = (double)BIn / (BOut + BIn);

            return R;
        }
    }
}
