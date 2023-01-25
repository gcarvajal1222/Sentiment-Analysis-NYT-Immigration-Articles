#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NYT.Sentiment.API/NYT.Sentiment.API.csproj", "NYT.Sentiment.API/"]
RUN dotnet restore "NYT.Sentiment.API/NYT.Sentiment.API.csproj"
COPY . .
WORKDIR "/src/NYT.Sentiment.API"
RUN dotnet build "NYT.Sentiment.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NYT.Sentiment.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NYT.Sentiment.API.dll"]