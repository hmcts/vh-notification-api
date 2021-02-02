#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["NotificationApi/NotificationApi.csproj", "NotificationApi/"]
COPY ["NotificationApi.Common/NotificationApi.Common.csproj", "NotificationApi.Common/"]
COPY ["NotificationApi.Contract/NotificationApi.Contract.csproj", "NotificationApi.Contract/"]
COPY ["NotificationApi.Domain/NotificationApi.Domain.csproj", "NotificationApi.Domain/"]
COPY ["NotificationApi.DAL/NotificationApi.DAL.csproj", "NotificationApi.DAL/"]
COPY ["nuget.config", "nuget.config"]
RUN dotnet restore --configfile nuget.config "NotificationApi/NotificationApi.csproj"
COPY . .
WORKDIR "/src/NotificationApi"
RUN dotnet build "NotificationApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NotificationApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotificationApi.dll"]
