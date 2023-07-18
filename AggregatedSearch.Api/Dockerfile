FROM mcr.microsoft.com/dotnet/sdk:6.0.301 AS build
WORKDIR /app

COPY . .
RUN dotnet restore
RUN dotnet publish --no-restore -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0.1 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "AggregatedSearch.Api.dll"]
