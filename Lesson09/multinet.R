getwd()
setwd('c:/Users/nemecdav/Downloads/')

install.packages('multinet')
library(multinet)

g = read_ml('florentine.mpx')
layers_ml(g)
actors_ml(g)
actors_ml(g, layers=character(0))

degree_ml(g, actors=c('Pazzi'))
degree_ml(g, actors=c('Medici'))

neighbors_ml(g, actor='Pazzi')
neighborhood_ml(g, actors=c('Pazzi'))

neighbors_ml(g, actor='Barbadori')
neighborhood_ml(g, actors=c('Barbadori'))
