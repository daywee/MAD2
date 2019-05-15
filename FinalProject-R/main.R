library(igraph)
library(dplyr)

getwd()
setwd('c:/Workspace/MAD2/Datasets/Project')

heroes = read.csv('hero-network.csv')
head(heroes)

h = graph_from_data_frame(heroes, directed=F)

# number of vertices
gorder(h)
# number of edges
gsize(h)

# remove loops in graph
hs = h
E(hs)$weight = 1
hs = simplify(hs)

gorder(hs)
gsize(hs)

is.weighted(hs)
is.simple(hs)
is.connected(hs)
components(hs)

# remove small components
V(hs)$comp = components(hs, mode='strong')$membership
hs = induced.subgraph(hs, V(hs)$comp==1)
is.connected(hs)

#articulation.points(hs)

# basic plot 
plot(hs, layout=layout_with_fr(hs), vertex.label=NA, vertex.size=4)

# stats
order_by_desc = function(values, col_name) {
  x = data.frame(values, names(values))
  colnames(x) = c(col_name, 'name')
  x = arrange(x, desc(x[[col_name]]))
  return(x)
}
order_by_desc2 = function(values, col_name) {
  x = data.frame(values, names(V(hs)))
  colnames(x) = c(col_name, 'name')
  x = arrange(x, desc(x[[col_name]]))
  return(x)
}
par(mfrow=c(1,2))
hist(graph.strength(hs), xlim=c(0, 5000), ylim=c(0, 2500), breaks=1000, xlab='Vážený stupeň', ylab='Četnost', main=NA)
hist(degree(hs), xlim=c(0, 500), ylim=c(0, 500), breaks=1000, xlab='Stupeň', ylab='Četnost', main=NA)
stren = order_by_desc(graph.strength(hs), 'strength')
deg = order_by_desc(degree(hs), 'degree')

hist(degree(hs), breaks=1000)

mean_distance(hs, directed=F)
mean(deg$degree)
centralization.degree(hs)
centralization.betweenness(hs, directed=F)
betw = order_by_desc2(centralization.betweenness(hs, directed=F)$res, 'betweenness_centrality')
centralization.closeness(hs)
clos = order_by_desc2(centralization.closeness(hs)$res, 'closeness_centrality')
centralization.evcent(hs, directed=F)
eige = order_by_desc2(centralization.evcent(hs, directed=F)$vector, 'igenvalue_centrality')
diam = diameter(hs, directed=F)
diam
mean_distance(hs, directed=F)
ecc = order_by_desc(eccentricity(hs), 'eccentricity')
transitivity(hs, type='undirected') # clustering coefficient


# barabasi albert model
ba = sample_pa(6408, m=26)
gsize(ba)
hist(degree(ba), xlim=c(0,200), breaks=1000)
transitivity(ba, type='undirected')

# simulated attack
hsAttack = hs
hsSize = vcount(hsAttack)
noRemovedVertices = numeric(hsSize)
noVertices = numeric(hsSize)
noComponents = numeric(hsSize)
largestComponentSize = numeric(hsSize)
meanDistance = numeric(hsSize)
meanDegree = numeric(hsSize)

for (i in 1:5000) {
  print(i)
  maxDegreeIndex = which.max(degree(hsAttack))
  hsAttack = delete_vertices(hsAttack, V(hsAttack)[maxDegreeIndex])
  
  noRemovedVertices[i] = i
  noVertices[i] = hsSize - i
  noComponents[i] = components(hsAttack)$no
  largestComponentSize[i] = max(components(hsAttack)$csize)
  #meanDistance[i] = mean_distance(hsAttack, directed=F)
  meanDegree[i] = mean(degree(hsAttack))
}
hsAttack.result = data.frame(noRemovedVertices, noVertices, noComponents, largestComponentSize, meanDistance, meanDegree)


hsRandomAttack = hs
hsSize = vcount(hsRandomAttack)
noRemovedVertices = numeric(hsSize)
noVertices = numeric(hsSize)
noComponents = numeric(hsSize)
largestComponentSize = numeric(hsSize)
meanDistance = numeric(hsSize)
meanDegree = numeric(hsSize)

for (i in 1:5000) {
  print(i)
  randomIndex = sample(1:(hsSize - i + 1), 1)[1]
  hsRandomAttack = delete_vertices(hsRandomAttack, V(hsRandomAttack)[randomIndex])
  
  noRemovedVertices[i] = i
  noVertices[i] = hsSize - i
  noComponents[i] = components(hsRandomAttack)$no
  largestComponentSize[i] = max(components(hsRandomAttack)$csize)
  #meanDistance[i] = mean_distance(hsRandomAttack, directed=F)
  meanDegree[i] = mean(degree(hsRandomAttack))
}
hsRandomAttack.result = data.frame(noRemovedVertices, noVertices, noComponents, largestComponentSize, meanDistance, meanDegree)

#hsAttack.result$noRemovedVertices = hsAttack.result$noRemovedVertices / max(hsAttack.result$noRemovedVertices)
#hsAttack.result$largestComponentSize = hsAttack.result$largestComponentSize / max(hsAttack.result$largestComponentSize)
#hsAttack.result$meanDegree = hsAttack.result$meanDegree / max(hsAttack.result$meanDegree)

#hsRandomAttack.result$noRemovedVertices = hsRandomAttack.result$noRemovedVertices / max(hsRandomAttack.result$noRemovedVertices)
#hsRandomAttack.result$largestComponentSize = hsRandomAttack.result$largestComponentSize / max(hsRandomAttack.result$largestComponentSize)
#hsRandomAttack.result$meanDegree = hsRandomAttack.result$meanDegree / max(hsRandomAttack.result$meanDegree)

par(mfrow=c(3,1))
plot(
  x=hsAttack.result$noRemovedVertices,
  y=hsAttack.result$largestComponentSize,
  col='red',
  xlab='Removed vertices',
  ylab='Largest component size'
)
points(x=hsRandomAttack.result$noRemovedVertices, y=hsRandomAttack.result$largestComponentSize, col='blue')
legend('bottomleft', legend=c('Highest degree removal', 'Random removal'), fill=c('red', 'blue'), cex=0.9, bty='n')

plot(
  x=hsAttack.result$noRemovedVertices,
  y=hsAttack.result$noComponents,
  col='red',
  xlab='Removed vertices',
  ylab='Number of components'
)
points(x=hsRandomAttack.result$noRemovedVertices, y=hsRandomAttack.result$noComponents, col='blue')

plot(
  x=hsAttack.result$noRemovedVertices,
  y=hsAttack.result$meanDegree,
  col='red',
  xlab='Removed vertices',
  ylab='Mean degree'
)
points(x=hsRandomAttack.result$noRemovedVertices, y=hsRandomAttack.result$meanDegree, col='blue')

# communities


cl = cluster_louvain(hs)
mem = data.frame(cl$membership, names(V(hs)))

sizes(cl)
cl[4]
crossing(cl, hs)
lay = layout_with_lgl(hs)
plot(cl, hs, layout=layout_with_fr(hs), vertex.label=NA, vertex.size=4)









