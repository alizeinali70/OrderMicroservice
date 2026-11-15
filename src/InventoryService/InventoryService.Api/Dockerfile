FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy project files with correct InventoryService project names
COPY ["src/InventoryService/InventoryService.Api/InventoryService.Api.csproj", "src/InventoryService/InventoryService.Api/"]
COPY ["src/InventoryService/InventoryService.Application/InventoryService.Application.csproj", "src/InventoryService/InventoryService.Application/"]
COPY ["src/InventoryService/InventoryService.Domain/InventoryService.Domain.csproj", "src/InventoryService/InventoryService.Domain/"]
COPY ["src/InventoryService/InventoryService.Infrastructure/InventoryService.Infrastructure.csproj", "src/InventoryService/InventoryService.Infrastructure/"]

# Restore dependencies for the API project
RUN dotnet restore "src/InventoryService/InventoryService.Api/InventoryService.Api.csproj"

# Copy all remaining source code and build
COPY . .
WORKDIR "/src/src/InventoryService/InventoryService.Api"
RUN dotnet build "InventoryService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InventoryService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InventoryService.Api.dll"]