# Create Base Image.
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Add version arguments
ARG BUILD_VERSION
ARG ASSEMBLY_VERSION
ARG FILE_VERSION

# Set environment variables for version (optional)
ENV BUILD_VERSION=$BUILD_VERSION
ENV ASSEMBLY_VERSION=$ASSEMBLY_VERSION
ENV FILE_VERSION=$FILE_VERSION

# Publish .NET App
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS publish
WORKDIR /src
COPY NotificationApi .
RUN dotnet restore --configfile nuget.config "NotificationApi/NotificationApi.csproj"
WORKDIR "/src/NotificationApi"

# Pass version arguments during the build/publish process
RUN dotnet publish "NotificationApi.csproj" -c Release -o /app/publish \
    /p:Version=$BUILD_VERSION \
    /p:AssemblyVersion=$ASSEMBLY_VERSION \
    /p:FileVersion=$FILE_VERSION

# Create Final App.
FROM base AS final
RUN apt-get update && apt-get install -y curl \
    && apt-get clean
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotificationApi.dll"]
