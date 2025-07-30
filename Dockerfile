# 🏗️ Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore
COPY *.csproj ./
RUN dotnet restore

# Copy the rest and build
COPY . ./
RUN dotnet publish -c Release -o out

# 🏃 Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Use HTTPS port by default
EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "dotrestapiwithmongo.dll"]
