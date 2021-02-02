#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Notifications.API/Notifications.API.csproj", "Notifications.API/"]
COPY ["NotificationsApi.Common/NotificationsApi.Common.csproj", "NotificationsApi.Common/"]
COPY ["NotificationsApi.Contract/NotificationsApi.Contract.csproj", "NotificationsApi.Contract/"]
COPY ["NotificationsApi.Domain/NotificationsApi.Domain.csproj", "NotificationsApi.Domain/"]
COPY ["NotificationsApi.DAL/NotificationsApi.DAL.csproj", "NotificationsApi.DAL/"]
COPY ["NotificationsApi.Services/NotificationsApi.Services.csproj", "NotificationsApi.Services/"]
COPY ["nuget.config", "nuget.config"]
RUN dotnet restore --configfile nuget.config "NotificationsApi.API/NotificationsApi.API.csproj"
COPY . .
WORKDIR "/src/NotificationsApi.API"
RUN dotnet build "NotificationsApi.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NotificationsApi.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotificationsApi.API.dll"]
