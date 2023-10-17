#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ARG CONNECTION_STRING
ARG JWT_SECRET
ARG JWT_AUD
ARG JWT_ISS

ARG MESSAGING_HOST
ARG MESSAGING_ACCESS_KEY
ARG MESSAGING_SECRET_KEY
ENV Messaging__Host=$MESSAGING_HOST
ENV Messaging__AccessKey=$MESSAGING_ACCESS_KEY
ENV Messaging__SecretKey=$MESSAGING_SECRET_KEY

ENV Jwt__Issuer=$JWT_ISS
ENV Jwt__Audience=$JWT_AUD
ENV Jwt__Secret=$JWT_SECRET
ENV ConnectionStrings__ApplicationDbContext=$CONNECTION_STRING

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Statistics/Splitify.Statistics.Api/Splitify.Statistics.Api.csproj", "src/Services/Statistics/Splitify.Statistics.Api/"]
RUN dotnet restore "src/Services/Statistics/Splitify.Statistics.Api/Splitify.Statistics.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Statistics/Splitify.Statistics.Api"
RUN dotnet build "Splitify.Statistics.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Splitify.Statistics.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Splitify.Statistics.Api.dll"]