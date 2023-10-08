#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Redirect/Splitify.Redirect.Api/Splitify.Redirect.Api.csproj", "src/Services/Redirect/Splitify.Redirect.Api/"]
COPY ["src/BuildingBlocks/Splitify.BuildingBlocks.EventBus/Splitify.BuildingBlocks.EventBus.csproj", "src/BuildingBlocks/Splitify.BuildingBlocks.EventBus/"]
COPY ["src/EventBus/Splitify.EventBus.Contracts/Splitify.EventBus.Contracts.csproj", "src/EventBus/Splitify.EventBus.Contracts/"]
COPY ["src/EventBus/Splitify.EventBus.MassTransit/Splitify.EventBus.MassTransit.csproj", "src/EventBus/Splitify.EventBus.MassTransit/"]
COPY ["src/Shared/Splitify.Shared.AspDotNet/Splitify.Shared.AspDotNet.csproj", "src/Shared/Splitify.Shared.AspDotNet/"]
COPY ["src/BuildingBlocks/Splitify.BuildingBlocks.Domain/Splitify.BuildingBlocks.Domain.csproj", "src/BuildingBlocks/Splitify.BuildingBlocks.Domain/"]
COPY ["src/Shared/Splitify.Shared.Services/Splitify.Shared.Services.csproj", "src/Shared/Splitify.Shared.Services/"]
COPY ["src/Services/Redirect/Splitify.Redirect.Application/Splitify.Redirect.Application.csproj", "src/Services/Redirect/Splitify.Redirect.Application/"]
COPY ["src/Services/Redirect/Splitify.Redirect.Domain/Splitify.Redirect.Domain.csproj", "src/Services/Redirect/Splitify.Redirect.Domain/"]
COPY ["src/Services/Redirect/Splitify.Redirect.Infrastructure/Splitify.Redirect.Infrastructure.csproj", "src/Services/Redirect/Splitify.Redirect.Infrastructure/"]
RUN dotnet restore "src/Services/Redirect/Splitify.Redirect.Api/Splitify.Redirect.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Redirect/Splitify.Redirect.Api"
RUN dotnet build "Splitify.Redirect.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Splitify.Redirect.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Splitify.Redirect.Api.dll"]