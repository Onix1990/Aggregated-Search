# Aggregated Search
## Тестовое задание для Sirena Travel

## Run
```
docker build -t aggregated_search -f AggregatedSearch.Api/Dockerfile .
docker run --name aggregated_search -d -p 8080:80 aggregated_search
http://localhost:8080/swagger
```

## Examples
```
http://localhost:8080/Routes?Origin=Москва&Destination=Сочи&OriginDateTime=2023-07-15T19:00:00
http://localhost:8080/Routes/fc475de5-2285-4e7c-a4a6-563dcd364fd0
http://localhost:8080/Ping
```

## Documentation
http://localhost:8080/swagger

## Requirements
* .NET 6