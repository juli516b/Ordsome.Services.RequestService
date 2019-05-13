#!/bin/bash
export ASPNETCORE_ENVIRONMENT=local
cd src/Ordsome.Services.UserService.WebApi
dotnet run --no-restore