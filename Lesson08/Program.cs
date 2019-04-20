﻿using Lesson08.Graph;
using Lesson08.Graph.Clustering;

namespace Lesson08
{
    class Program
    {
        static void Main(string[] args)
        {
            var gl = new GraphLoader();
            var graph = gl.LoadFromCsvFile("../../../Datasets/KarateClub/KarateClub.csv");
            graph.NormalizeIds();

            var clusterer = new KCoreClusterer();
            clusterer.Cluster(graph, 1);
            clusterer.Cluster(graph, 2);
            clusterer.Cluster(graph, 3);
            clusterer.Cluster(graph, 4);
        }
    }
}
