FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

EXPOSE 80
EXPOSE 443
EXPOSE 8001

COPY . ./
RUN dotnet restore

COPY src/. ./
RUN dotnet publish -c Release -o out src/WorldTime.gRPC/WorldTime.gRPC.csproj

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "WorldTime.gRPC.dll"]
