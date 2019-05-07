# Ordsome.Services.RequestService

Tried out Mediator pattern with MediatR + CQRS. Implemented Clean Architecture (small flaws).

# Using API-gateway.

Currently working on a gateway made through Ocelot.

To access this gateway you need to use https://localhost:7000.

Since Ocelot doesn't work with Swagger ATM you need to access the different Swagger documentation for each API.

- To access the Swagger documentation for the RequestServiceApi use: http://localhost:7001/requestapi/docs
- To access the Swagger documentation for the UserServiceApi use: http://localhost:7002/userapi/docs

The endpoint listed in swagger can be run through the API gateway on port 7000. 

# Authentication.

To make use of authentication you have to do the following. 
The UserService.WebApi has three endpoints.

![PictureofUserAPI](https://i.imgur.com/pDbBWai.png)

Register will make you an account in the database. Afterwards you will have to make a login to get a token in return.

To make a call to an authorized endpoint the HTTP request could be like the following

![PictureOfCall](https://i.imgur.com/QbrPiSI.png)

#### To do: 
Follow the Trello board for this service
https://trello.com/b/dGL6qEss/requestservice
