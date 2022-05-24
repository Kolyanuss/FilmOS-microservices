#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Services/Filmos_favorites/EFCoreCodeFirstSampleWEBAPI/EFCoreCodeFirstSampleWEBAPI.csproj", "src/Services/Filmos_favorites/EFCoreCodeFirstSampleWEBAPI/"]
COPY ["src/Services/Filmos_favorites/EFCoreCodeFirstSampleWEBAPI.BLL/EFCoreCodeFirstSampleWEBAPI.BLL.csproj", "src/Services/Filmos_favorites/EFCoreCodeFirstSampleWEBAPI.BLL/"]
COPY ["src/EventBus.Messages/EventBus.Messages.csproj", "src/EventBus.Messages/"]
COPY ["src/Services/Filmos_favorites/EFCoreCodeFirstSampleWEBAPI.DAL/EFCoreCodeFirstSampleWEBAPI.DAL.csproj", "src/Services/Filmos_favorites/EFCoreCodeFirstSampleWEBAPI.DAL/"]
RUN dotnet restore "src/Services/Filmos_favorites/EFCoreCodeFirstSampleWEBAPI/EFCoreCodeFirstSampleWEBAPI.csproj"
COPY . .
WORKDIR "/src/src/Services/Filmos_favorites/EFCoreCodeFirstSampleWEBAPI"
RUN dotnet build "EFCoreCodeFirstSampleWEBAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EFCoreCodeFirstSampleWEBAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EFCoreCodeFirstSampleWEBAPI.dll"]
