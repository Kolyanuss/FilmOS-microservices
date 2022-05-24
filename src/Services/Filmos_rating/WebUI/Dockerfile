#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Services/Filmos_rating/WebUI/WebUI.csproj", "src/Services/Filmos_rating/WebUI/"]
COPY ["src/EventBus.Messages/EventBus.Messages.csproj", "src/EventBus.Messages/"]
COPY ["src/Services/Filmos_rating/Persistence/Persistence.csproj", "src/Services/Filmos_rating/Persistence/"]
COPY ["src/Services/Filmos_rating/Application/Application.csproj", "src/Services/Filmos_rating/Application/"]
COPY ["src/Services/Filmos_rating/Domain/Domain.csproj", "src/Services/Filmos_rating/Domain/"]
COPY ["src/Services/Filmos_rating/Infrastructure/Infrastructure.csproj", "src/Services/Filmos_rating/Infrastructure/"]
RUN dotnet restore "src/Services/Filmos_rating/WebUI/WebUI.csproj"
COPY . .
WORKDIR "/src/src/Services/Filmos_rating/WebUI"
RUN dotnet build "WebUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebUI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Filmos_Rating_CleanArchitecture.WebUI.dll"]
