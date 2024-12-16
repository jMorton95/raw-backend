ARG VERSION=9.0-alpine

FROM mcr.microsoft.com/dotnet/aspnet:$VERSION AS base
WORKDIR /app
EXPOSE 80
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:$VERSION AS build
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
