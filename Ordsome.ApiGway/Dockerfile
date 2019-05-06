FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Ordsome.ApiGway/Ordsome.ApiGway.csproj", "Ordsome.ApiGway/"]
RUN dotnet restore "Ordsome.ApiGway/Ordsome.ApiGway.csproj"
COPY . .
WORKDIR "/src/Ordsome.ApiGway"
RUN dotnet build "Ordsome.ApiGway.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Ordsome.ApiGway.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Ordsome.ApiGway.dll"]