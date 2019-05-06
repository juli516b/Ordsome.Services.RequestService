FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["UserService.WebApi/UserService.WebApi.csproj", "UserService.WebApi/"]
RUN dotnet restore "UserService.WebApi/UserService.WebApi.csproj"
COPY . .
WORKDIR "/src/UserService.WebApi"
RUN dotnet build "UserService.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "UserService.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "UserService.WebApi.dll"]