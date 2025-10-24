# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["BrewUp.sln", "./"]
COPY ["src/BrewUp.Rest/BrewUp.Rest.csproj", "src/BrewUp.Rest/"]
COPY ["src/BrewUp.Purchase.Facade/BrewUp.Purchase.Facade.csproj", "src/BrewUp.Purchase.Facade/"]
COPY ["src/BrewUp.Purchase.Infrastructure/BrewUp.Purchase.Infrastructure.csproj", "src/BrewUp.Purchase.Infrastructure/"]
COPY ["src/BrewUp.PurchaseMediator/BrewUp.PurchaseMediator.csproj", "src/BrewUp.PurchaseMediator/"]
COPY ["src/BrewUp.Purchase.Domain/BrewUp.Purchase.Domain.csproj", "src/BrewUp.Purchase.Domain/"]
COPY ["src/BrewUp.Purchase.ReadModel/BrewUp.Purchase.ReadModel.csproj", "src/BrewUp.Purchase.ReadModel/"]
COPY ["src/BrewUp.Purchase.SharedKernel/BrewUp.Purchase.SharedKernel.csproj", "src/BrewUp.Purchase.SharedKernel/"]
COPY ["src/BrewUp.Messages/BrewUp.Messages.csproj", "src/BrewUp.Messages/"]

# Restore dependencies
RUN dotnet restore "src/BrewUp.Rest/BrewUp.Rest.csproj"

# Copy remaining source files
COPY . .

# Build the application
WORKDIR "/src/src/BrewUp.Rest"
RUN dotnet build "BrewUp.Rest.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "BrewUp.Rest.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "BrewUp.Rest.dll"]
