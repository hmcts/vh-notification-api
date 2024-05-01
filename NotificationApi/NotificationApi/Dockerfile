# Create Base Image.
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Publish .NET App
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS publish
WORKDIR /src
COPY NotificationApi .
RUN dotnet restore --configfile nuget.config "NotificationApi/NotificationApi.csproj"
WORKDIR "/src/NotificationApi"
RUN dotnet publish "NotificationApi.csproj" -c Release -o /app/publish

# Create Final App.
FROM base AS final
RUN apt-get update && apt-get install -y curl
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotificationApi.dll"]
