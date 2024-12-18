ARG VERSION=9.0-alpine

FROM mcr.microsoft.com/dotnet/aspnet:$VERSION AS base
WORKDIR /app
EXPOSE 80
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:$VERSION AS build
WORKDIR /src
COPY ["RawPlatform/RawPlatform.csproj", "RawPlatform/"]
RUN dotnet restore "RawPlatform/RawPlatform.csproj"
COPY . .
WORKDIR "/src/RawPlatform"
RUN dotnet build "RawPlatform.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RawPlatform.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RawPlatform.dll"]
