using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FinalProject.NetworkAnalysis.CommunityDetection
{
    public static class IterativeLocalExpansion
    {
        public static List<Community> FindAllCommunities(Network network, Node startNode)
        {
            var random = new Random();
            var communities = new List<IterativeLocalExpansionCommunity>();
            var foundCommunity = FindLocalCommunityInternal(network, startNode);
            communities.Add(foundCommunity);

            var S = foundCommunity.S.ToList();
            var coveredNodes = foundCommunity.D.ToList();

            while (S.Any())
            {
                var randomNode = S[random.Next(S.Count)];
                foundCommunity = FindLocalCommunityInternal(network, randomNode);
                communities.Add(foundCommunity);
                coveredNodes.AddRange(foundCommunity.D);
                S.AddRange(foundCommunity.S);
                S = S.Except(coveredNodes).ToList();
            }

            return communities.Select((e, i) => new Community(i, e.D)).ToList();
        }

        public static Community FindLocalCommunity(Network network, Node startNode)
        {
            var community = FindLocalCommunityInternal(network, startNode);
            return new Community(0, community.D);
        }

        private static IterativeLocalExpansionCommunity FindLocalCommunityInternal(Network network, Node startNode)
        {
            var community = new IterativeLocalExpansionCommunity(startNode);

            while (AddNodeToCommunity(community)) ;

            return community;
        }

        private static bool AddNodeToCommunity(IterativeLocalExpansionCommunity community)
        {
            Node bestNode = null;
            double bestR = community.ComputeR();
            foreach (var ni in community.S)
            {
                double r = community.ComputeRWithAddedNode(ni);
                if (r > bestR)
                {
                    bestR = r;
                    bestNode = ni;
                }
            }

            if (bestNode == null)
                return false;

            community.AddNodeToD(bestNode);
            Debug.WriteLine($"Added '{bestNode.Id}'");
            return true;
        }
    }
}
