getwd()
setwd("C:/Workspace/school/MAD2/Exports")

karateClub = read.csv('KarateClubSirDistribution.csv')
erdos = read.csv('ErdosSirDistribution.csv')
barabasi = read.csv('BarabasiSirDistribution.csv')
airports = read.csv('AirportsSirDistribution.csv')

plotDistribution = function(table, datasetName) {
  n = table$Susceptible[1] + table$Infected[1] + table$Recovered[1]
  table$Susceptible = table$Susceptible / n
  table$Infected = table$Infected / n
  table$Recovered = table$Recovered / n
  
  plot(
    x=table$Iteration, 
    y=table$Susceptible, 
    col='green',
    type='l',
    ylim=c(0,1),
    xlab='Time',
    ylab='Distribution',
    main=paste(datasetName, '(Infection probability: 0.3, Recovery rate: 5)')
  )
  lines(x=table$Iteration, y=table$Infected, col='red')
  lines(x=table$Iteration, y=table$Recovered, col='blue')
}

plotDistribution(karateClub, 'Karate club')
plotDistribution(erdos, 'Erdos')
plotDistribution(barabasi, 'Barabasi')
plotDistribution(airports, 'Airports')
