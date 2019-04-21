library(igraph)

getwd()
setwd('c:/Workspace/school/MAD2/Datasets/')

cm = read.csv('cm.csv')
g1 = graph.data.frame(cm, directed=F)
plot(g_cm)

dist = degree_distribution(g1, mode='all')
plot(dist, log='xy')
