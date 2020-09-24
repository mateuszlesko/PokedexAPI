FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /PokeApi
COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /PokeApi
EXPOSE 80
COPY --from=build-env /PokeApi/out .
ENTRYPOINT [ "dotnet","PokeApi.dll" ]