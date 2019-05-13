#!/bin/bash
export ASPNETCORE_ENVIRONMENT=local
DOTNET_RUN=./scripts/dotnet-run.sh
PREFIX=Ordsome
SERVICE=$PREFIX.Services
REPOSITORIES=($SERVICE.ApiGway $SERVICE.Customers $SERVICE.Identity $SERVICE.Operations $SERVICE.Orders $SERVICE.Products $SERVICE.Signalr)

for REPOSITORY in ${REPOSITORIES[*]}
do
	 echo ========================================================
	 echo Starting a service: $REPOSITORY
	 echo ========================================================
     cd $REPOSITORY
     $DOTNET_RUN &
     cd ..
done