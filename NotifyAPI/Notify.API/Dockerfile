#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Notify.API/Notify.API.csproj", "Notify.API/"]
COPY ["NotifyApi.Common/NotifyApi.Common.csproj", "NotifyApi.Common/"]
COPY ["NotifyApi.Contract/NotifyApi.Contract.csproj", "NotifyApi.Contract/"]
COPY ["NotifyApi.Domain/NotifyApi.Domain.csproj", "NotifyApi.Domain/"]
COPY ["NotifyApi.DAL/NotifyApi.DAL.csproj", "NotifyApi.DAL/"]
COPY ["NotifyApi.Events/NotifyApi.Events.csproj", "NotifyApi.Events/"]
RUN dotnet restore "Notify.API/Notify.API.csproj"
COPY . .
WORKDIR "/src/Notify.API"
RUN dotnet build "Notify.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Notify.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notify.API.dll"]