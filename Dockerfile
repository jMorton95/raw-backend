ARG VERSION=9.0-alpine

FROM mcr.microsoft.com/dotnet/aspnet:$VERSION AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:$VERSION AS build
WORKDIR /src

RUN apk add --no-cache nodejs npm

COPY ["package.json", "package-lock.json*", "./RawPlatform/"]
WORKDIR "/src/RawPlatform"
RUN npm install

COPY ["RawPlatform/RawPlatform.csproj", "RawPlatform/"]
RUN dotnet restore "RawPlatform/RawPlatform.csproj"

COPY . .
WORKDIR "/src/RawPlatform"

RUN dotnet build "RawPlatform/RawPlatform.csproj" -c Release -o /app/build
RUN npx tailwindcss -i RawPlatform/wwwroot/app.css -o RawPlatform/wwwroot/styles.css --minify

FROM build AS publish
RUN dotnet publish "RawPlatform/RawPlatform.csproj" -c Release -o /app/publish -p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RawPlatform.dll"]
