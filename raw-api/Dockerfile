FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["raw-api/raw-api.csproj", "raw-api/"]
RUN dotnet restore "raw-api/raw-api.csproj"
COPY . .
WORKDIR "/src/raw-api"
RUN dotnet build "raw-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "raw-api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "raw-api.dll"]
