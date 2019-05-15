#!/bin/bash
export ASPNETCORE_ENVIRONMENT=local
cd Ordsome.Services.RequestService/Ordsome.Services.RequestService.WebApi
dotnet run --no-restore