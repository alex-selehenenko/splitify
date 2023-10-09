#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ARG CONNECTION_STRING
ARG JWT_SECRET
ARG JWT_AUD
ARG JWT_ISS

ENV Jwt__Issuer=$JWT_ISS
ENV Jwt__Audience=$JWT_AUD
ENV Jwt__Secret=$JWT_SECRET
ENV ConnectionStrings__ApplicationDbContext=$CONNECTION_STRING

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Identity/Splitify.Identity.Api/Splitify.Identity.Api.csproj", "src/Services/Identity/Splitify.Identity.Api/"]
COPY ["src/BuildingBlocks/Splitify.BuildingBlocks.EventBus/Splitify.BuildingBlocks.EventBus.csproj", "src/BuildingBlocks/Splitify.BuildingBlocks.EventBus/"]
COPY ["src/EventBus/Splitify.EventBus.MassTransit/Splitify.EventBus.MassTransit.csproj", "src/EventBus/Splitify.EventBus.MassTransit/"]
COPY ["src/Shared/Splitify.Shared.AspDotNet/Splitify.Shared.AspDotNet.csproj", "src/Shared/Splitify.Shared.AspDotNet/"]
COPY ["src/BuildingBlocks/Splitify.BuildingBlocks.Domain/Splitify.BuildingBlocks.Domain.csproj", "src/BuildingBlocks/Splitify.BuildingBlocks.Domain/"]
COPY ["src/Shared/Splitify.Shared.Services/Splitify.Shared.Services.csproj", "src/Shared/Splitify.Shared.Services/"]
COPY ["src/Services/Identity/Splitify.Identity.Application/Splitify.Identity.Application.csproj", "src/Services/Identity/Splitify.Identity.Application/"]
COPY ["src/EventBus/Splitify.EventBus.Contracts/Splitify.EventBus.Contracts.csproj", "src/EventBus/Splitify.EventBus.Contracts/"]
COPY ["src/Services/Identity/Splitify.Identity.Domain/Splitify.Identity.Domain.csproj", "src/Services/Identity/Splitify.Identity.Domain/"]
COPY ["src/Services/Identity/Splitify.Identity.Infrastructure/Splitify.Identity.Infrastructure.csproj", "src/Services/Identity/Splitify.Identity.Infrastructure/"]
RUN dotnet restore "src/Services/Identity/Splitify.Identity.Api/Splitify.Identity.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Identity/Splitify.Identity.Api"
RUN dotnet build "Splitify.Identity.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Splitify.Identity.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Splitify.Identity.Api.dll"]