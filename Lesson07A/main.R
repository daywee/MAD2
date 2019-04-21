install.packages('igraph')
library(igraph)

# 1. a)
s = cbind(1:9, 2:10)
startGraph = graph_from_edgelist(s, directed=F)
plot(startGraph)

barabasi = barabasi.game(500, m=3, directed=F, start.graph=startGraph)
plot(barabasi)
E(barabasi)

# 1. b)
erdos = erdos.renyi.game(500, 1479, type='gnm', directed=F, loops=F)
plot(erdos)
E(erdos)

# 1. c)
airportCsv = read.csv('USairport500.csv', sep=';')
airports = graph_from_data_frame(airportCsv, directed=F)
plot(airports)

# 2. a)
# number of components
components(erdos)$no
components(barabasi)$no
components(airports)$no

# 2. b)
# size of the largest component
components(erdos)$csize[1]
components(barabasi)$csize[1]
components(airports)$csize[1]

# 2. c)
# mean distance
mean_distance(erdos)
mean_distance(barabasi)
mean_distance(airports)

# 2. d)
# mean degree
mean(degree(erdos))
mean(degree(barabasi))
mean(degree(airports))

# 3.
# erdos - highest degree node removal
erdosAttack = erdos
erdosSize = vcount(erdos)
noRemovedVertices = numeric(erdosSize)
noVertices = numeric(erdosSize)
noComponents = numeric(erdosSize)
largestComponentSize = numeric(erdosSize)
meanDistance = numeric(erdosSize)
meanDegree = numeric(erdosSize)

for (i in 1:300) {
  maxDegreeIndex = which.max(degree(erdosAttack))
  erdosAttack = delete_vertices(erdosAttack, V(erdosAttack)[maxDegreeIndex])
  
  noRemovedVertices[i] = i
  noVertices[i] = erdosSize - i
  noComponents[i] = components(erdosAttack)$no
  largestComponentSize[i] = max(components(erdosAttack)$csize)
  meanDistance[i] = mean_distance(erdosAttack)
  meanDegree[i] = mean(degree(erdosAttack))
}
erdosResults.highestRemoval = data.frame(noRemovedVertices, noVertices, noComponents, largestComponentSize, meanDistance, meanDegree)
# plot(erdosAttack, vertex.label=NA, vertex.size=5)
plot(x=erdosResults.highestRemoval$noRemovedVertices, y=erdosResults.highestRemoval$largestComponentSize)


# erdos - random node removal
erdosAttack = erdos
erdosSize = vcount(erdos)
noRemovedVertices = numeric(erdosSize)
noVertices = numeric(erdosSize)
noComponents = numeric(erdosSize)
largestComponentSize = numeric(erdosSize)
meanDistance = numeric(erdosSize)
meanDegree = numeric(erdosSize)

for (i in 1:300) {
  randomIndex = sample(1:(erdosSize - i + 1), 1)[1]
  erdosAttack = delete_vertices(erdosAttack, V(erdosAttack)[randomIndex])
  
  noRemovedVertices[i] = i
  noVertices[i] = erdosSize - i
  noComponents[i] = components(erdosAttack)$no
  largestComponentSize[i] = max(components(erdosAttack)$csize)
  meanDistance[i] = mean_distance(erdosAttack)
  meanDegree[i] = mean(degree(erdosAttack))
}
erdosResults.randomRemoval = data.frame(noRemovedVertices, noVertices, noComponents, largestComponentSize, meanDistance, meanDegree)
plot(x=erdosResults.randomRemoval$noRemovedVertices, y=erdosResults.randomRemoval$largestComponentSize)


# barabasi - highest degree node removal
barabasiAttack = barabasi
barabasiSize = vcount(barabasi)
noRemovedVertices = numeric(barabasiSize)
noVertices = numeric(barabasiSize)
noComponents = numeric(barabasiSize)
largestComponentSize = numeric(barabasiSize)
meanDistance = numeric(barabasiSize)
meanDegree = numeric(barabasiSize)

for (i in 1:300) {
  maxDegreeIndex = which.max(degree(barabasiAttack))
  barabasiAttack = delete_vertices(barabasiAttack, V(barabasiAttack)[maxDegreeIndex])
  
  noRemovedVertices[i] = i
  noVertices[i] = barabasiSize - i
  noComponents[i] = components(barabasiAttack)$no
  largestComponentSize[i] = max(components(barabasiAttack)$csize)
  meanDistance[i] = mean_distance(barabasiAttack)
  meanDegree[i] = mean(degree(barabasiAttack))
}
barabasiResults.highestRemoval = data.frame(noRemovedVertices, noVertices, noComponents, largestComponentSize, meanDistance, meanDegree)
# plot(barabasiAttack, vertex.label=NA, vertex.size=5)
plot(x=barabasiResults.highestRemoval$noRemovedVertices, y=barabasiResults.highestRemoval$largestComponentSize)


# barabasi - random node removal
barabasiAttack = barabasi
barabasiSize = vcount(barabasi)
noRemovedVertices = numeric(barabasiSize)
noVertices = numeric(barabasiSize)
noComponents = numeric(barabasiSize)
largestComponentSize = numeric(barabasiSize)
meanDistance = numeric(barabasiSize)
meanDegree = numeric(barabasiSize)

for (i in 1:300) {
  randomIndex = sample(1:(barabasiSize - i + 1), 1)[1]
  barabasiAttack = delete_vertices(barabasiAttack, V(barabasiAttack)[randomIndex])
  
  noRemovedVertices[i] = i
  noVertices[i] = barabasiSize - i
  noComponents[i] = components(barabasiAttack)$no
  largestComponentSize[i] = max(components(barabasiAttack)$csize)
  meanDistance[i] = mean_distance(barabasiAttack)
  meanDegree[i] = mean(degree(barabasiAttack))
}
barabasiResults.randomRemoval = data.frame(noRemovedVertices, noVertices, noComponents, largestComponentSize, meanDistance, meanDegree)
plot(x=barabasiResults.randomRemoval$noRemovedVertices, y=barabasiResults.randomRemoval$largestComponentSize)


# airports - highest degree node removal
airportsAttack = airports
airportsSize = vcount(airports)
noRemovedVertices = numeric(airportsSize)
noVertices = numeric(airportsSize)
noComponents = numeric(airportsSize)
largestComponentSize = numeric(airportsSize)
meanDistance = numeric(airportsSize)
meanDegree = numeric(airportsSize)

for (i in 1:300) {
  maxDegreeIndex = which.max(degree(airportsAttack))
  airportsAttack = delete_vertices(airportsAttack, V(airportsAttack)[maxDegreeIndex])
  
  noRemovedVertices[i] = i
  noVertices[i] = airportsSize - i
  noComponents[i] = components(airportsAttack)$no
  largestComponentSize[i] = max(components(airportsAttack)$csize)
  meanDistance[i] = mean_distance(airportsAttack)
  meanDegree[i] = mean(degree(airportsAttack))
}
airportsResults.highestRemoval = data.frame(noRemovedVertices, noVertices, noComponents, largestComponentSize, meanDistance, meanDegree)
# plot(airportsAttack, vertex.label=NA, vertex.size=5)
plot(x=airportsResults.highestRemoval$noRemovedVertices, y=airportsResults.highestRemoval$largestComponentSize)


# airports - random node removal
airportsAttack = airports
airportsSize = vcount(airports)
noRemovedVertices = numeric(airportsSize)
noVertices = numeric(airportsSize)
noComponents = numeric(airportsSize)
largestComponentSize = numeric(airportsSize)
meanDistance = numeric(airportsSize)
meanDegree = numeric(airportsSize)

for (i in 1:300) {
  randomIndex = sample(1:(airportsSize - i + 1), 1)[1]
  airportsAttack = delete_vertices(airportsAttack, V(airportsAttack)[randomIndex])
  
  noRemovedVertices[i] = i
  noVertices[i] = airportsSize - i
  noComponents[i] = components(airportsAttack)$no
  largestComponentSize[i] = max(components(airportsAttack)$csize)
  meanDistance[i] = mean_distance(airportsAttack)
  meanDegree[i] = mean(degree(airportsAttack))
}
airportsResults.randomRemoval = data.frame(noRemovedVertices, noVertices, noComponents, largestComponentSize, meanDistance, meanDegree)
plot(x=airportsResults.randomRemoval$noRemovedVertices, y=airportsResults.randomRemoval$largestComponentSize)



# add together
par(mfrow=c(3,3))

# plot erdos
plot(
  x=erdosResults.highestRemoval$noRemovedVertices,
  y=erdosResults.highestRemoval$largestComponentSize,
  col='red',
  xlab='Removed vertices',
  ylab='Largest component size'
)
points(x=erdosResults.randomRemoval$noRemovedVertices, y=erdosResults.randomRemoval$largestComponentSize, col='blue')
legend('bottomleft', legend=c('Highest degree removal', 'Random removal'), fill=c('red', 'blue'), cex=0.9, bty='n')

plot(
  x=erdosResults.highestRemoval$noRemovedVertices,
  y=erdosResults.highestRemoval$meanDistance,
  col='red',
  xlab='Removed vertices',
  ylab='Mean distance',
  main='Erdős–Rényi model'
)
points(x=erdosResults.randomRemoval$noRemovedVertices, y=erdosResults.randomRemoval$meanDistance, col='blue')

plot(
  x=erdosResults.highestRemoval$noRemovedVertices,
  y=erdosResults.highestRemoval$meanDegree,
  col='red',
  xlab='Removed vertices',
  ylab='Mean degree'
)
points(x=erdosResults.randomRemoval$noRemovedVertices, y=erdosResults.randomRemoval$meanDegree, col='blue')

# plot barabasi
plot(
  x=barabasiResults.highestRemoval$noRemovedVertices,
  y=barabasiResults.highestRemoval$largestComponentSize,
  col='red',
  xlab='Removed vertices',
  ylab='Largest component size'
)
points(x=barabasiResults.randomRemoval$noRemovedVertices, y=barabasiResults.randomRemoval$largestComponentSize, col='blue')

plot(
  x=barabasiResults.highestRemoval$noRemovedVertices,
  y=barabasiResults.highestRemoval$meanDistance,
  col='red',
  xlab='Removed vertices',
  ylab='Mean distance',
  main='Barabási–Albert model'
)
points(x=barabasiResults.randomRemoval$noRemovedVertices, y=barabasiResults.randomRemoval$meanDistance, col='blue')

plot(
  x=barabasiResults.highestRemoval$noRemovedVertices,
  y=barabasiResults.highestRemoval$meanDegree,
  col='red',
  xlab='Removed vertices',
  ylab='Mean degree'
)
points(x=barabasiResults.randomRemoval$noRemovedVertices, y=barabasiResults.randomRemoval$meanDegree, col='blue')

# plot USA airports
plot(
  x=airportsResults.highestRemoval$noRemovedVertices,
  y=airportsResults.highestRemoval$largestComponentSize,
  col='red',
  xlab='Removed vertices',
  ylab='Largest component size'
)
points(x=airportsResults.randomRemoval$noRemovedVertices, y=airportsResults.randomRemoval$largestComponentSize, col='blue')

plot(
  x=airportsResults.highestRemoval$noRemovedVertices,
  y=airportsResults.highestRemoval$meanDistance,
  col='red',
  xlab='Removed vertices',
  ylab='Mean distance',
  main='USA Airports 500'
)
points(x=airportsResults.randomRemoval$noRemovedVertices, y=airportsResults.randomRemoval$meanDistance, col='blue')

plot(
  x=airportsResults.highestRemoval$noRemovedVertices,
  y=airportsResults.highestRemoval$meanDegree,
  col='red',
  xlab='Removed vertices',
  ylab='Mean degree'
)
points(x=airportsResults.randomRemoval$noRemovedVertices, y=airportsResults.randomRemoval$meanDegree, col='blue')



