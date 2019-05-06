# Ordsome.Services.RequestService

Tried out Mediator pattern with MediatR + CQRS. Implemented Clean Architecture (small flaws).

# Using API-gateway.

Currently working on a gateway made through Ocelot.

To access this gateway you need to use https://localhost:7000.

Since Ocelot doesn't work with Swagger ATM you need to access the different Swagger documentation for each API.

- To access the Swagger documentation for the RequestServiceApi use: http://localhost:7001/requestapi/docs
- To access the Swagger documentation for the UserServiceApi use: http://localhost:7002/userapi/docs

The endpoint listed in swagger can be run through the API gateway on port 7000. 

# Setup EUREKA Service Discovery.

Make a new folder - CMD into it and do the following command:
```git clone https://github.com/spring-cloud-samples/eureka.git
cd eureka```

To make EUREKA run you need to setup your JAVA_PATH enviroment variabal to JDK version 8.
https://www.oracle.com/technetwork/java/javase/downloads/jdk8-downloads-2133151.html

You also need to install Maven and set the PATH variabel. 
https://maven.apache.org/download.cgi

To check if maven is installed do the following in a CMD.
`mvn -v`

Afterwards, open the CMD that is cd into the eureka folder. Run the following command:
`mvnw spring-boot:run`

#### To do: 
Follow the Trello board for this service
https://trello.com/b/dGL6qEss/requestservice
