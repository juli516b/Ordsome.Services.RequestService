#!/bin/bash
export ASPNETCORE_ENVIRONMENT=local
DOTNET_RUN=./scripts/dotnet-run.sh
PREFIX=Ordsome
SERVICE=$PREFIX.Services
REPOSITORIES=($PREFIX.ApiGway $SERVICE.RequestSevice.WebApi $SERVICE.UserService)

for REPOSITORY in ${REPOSITORIES[*]}
do
	 echo ========================================================
	 echo Starting a service: $REPOSITORY
	 echo ========================================================
     cd $REPOSITORY
     $DOTNET_RUN &
     cd ..
done