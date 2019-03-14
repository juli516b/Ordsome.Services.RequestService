FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["RequestService.WebApi/RequestService.WebApi.csproj", "RequestService.WebApi/"]
RUN dotnet restore "RequestService.WebApi/RequestService.WebApi.csproj"
COPY . .
WORKDIR "/src/RequestService.WebApi"
RUN dotnet build "RequestService.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "RequestService.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RequestService.WebApi.dll"]