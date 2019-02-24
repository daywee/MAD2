if (!require("igraph")) 
  install.packages("igraph")
library(igraph) # verze 1.0.1

# load to data frames
ba <- read.csv("../../Exports/ba.csv", header = T)
er <- read.csv("../../Exports/er.csv", header = T)
baRnsSample <- read.csv("../../Exports/baRnsSample.csv", header = T)
baRdnSample <- read.csv("../../Exports/baRdnSample.csv", header = T)
erRnsSample <- read.csv("../../Exports/erRnsSample.csv", header = T)
erRdnSample <- read.csv("../../Exports/erRdnSample.csv", header = T)

# remove vertices with zero degree
er <- er[!er$Degree==0,]

# apply logarithmic scale
ba$Degree = log10(ba$Degree)
baRnsSample$Degree = log10(baRnsSample$Degree) 
baRdnSample$Degree = log10(baRdnSample$Degree)
er$Degree = log10(er$Degree)
erRnsSample$Degree = log10(erRnsSample$Degree)
erRdnSample$Degree = log10(erRdnSample$Degree)

# normalize
ba$Degree = ba$Degree / max(ba[,"Degree"])
baRnsSample$Degree = baRnsSample$Degree / max(baRnsSample[,"Degree"])
baRdnSample$Degree = baRdnSample$Degree / max(baRdnSample[,"Degree"])
er$Degree = er$Degree / max(er[,"Degree"])
erRnsSample$Degree = erRnsSample$Degree / max(erRnsSample[,"Degree"])
erRdnSample$Degree = erRdnSample$Degree / max(erRdnSample[,"Degree"])

plot(ecdf(er[,"Degree"]), cex=0, col="green", verticals=T)
lines(ecdf(erRnsSample[,"Degree"]), cex=0, col="red", verticals=T)
lines(ecdf(erRdnSample[,"Degree"]), cex=0, col="blue", verticals=T)

plot(ecdf(ba[,"Degree"]), cex=0, col="green", verticals=T)
lines(ecdf(baRnsSample[,"Degree"]), cex=0, col="red", verticals=T)
lines(ecdf(baRdnSample[,"Degree"]), cex=0, col="blue", verticals=T)



