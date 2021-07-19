FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

# Install Node.js
RUN curl -fsSL https://deb.nodesource.com/setup_14.x | bash - \
    && apt-get install -y \
        nodejs \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /src
COPY ["WebApp1/WebApp1.csproj", "WebApp1/"]
RUN dotnet restore "WebApp1/WebApp1.csproj"
COPY . .
WORKDIR "/src/WebApp1"
RUN dotnet build "WebApp1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApp1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApp1.dll"]
