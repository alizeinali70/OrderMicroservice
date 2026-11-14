FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy project files while preserving the full directory structure
COPY ["src/OrderService/OrderService.Api/OrderService.Api.csproj", "src/OrderService/OrderService.Api/"]
COPY ["src/OrderService/OrderService.Application/OrderService.Application.csproj", "src/OrderService/OrderService.Application/"]
COPY ["src/OrderService/OrderService.Domain/OrderService.Domain.csproj", "src/OrderService/OrderService.Domain/"]
COPY ["src/OrderService/OrderService.Infrastructure/OrderService.Infrastructure.csproj", "src/OrderService/OrderService.Infrastructure/"]

# Restore dependencies for the API project
RUN dotnet restore "src/OrderService/OrderService.Api/OrderService.Api.csproj"

# Copy all remaining source code
COPY src/ src/
WORKDIR "/src/src/OrderService/OrderService.Api"
RUN dotnet build "OrderService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrderService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderService.Api.dll"]