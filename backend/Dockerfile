FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /source
COPY *.sln ./
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c release -o ./app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build source/app ./
ENTRYPOINT ["dotnet", "backend.dll"]