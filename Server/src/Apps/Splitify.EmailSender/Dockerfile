#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV SmtpClient__ApiPath=
ENV SmtpClient__ApiKey=
ENV SmtpClient__SenderEmail=
ENV SmtpClient__SenderName=

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Apps/Splitify.EmailSender/Splitify.EmailSender.csproj", "src/Apps/Splitify.EmailSender/"]
COPY ["src/EventBus/Splitify.EventBus.Contracts/Splitify.EventBus.Contracts.csproj", "src/EventBus/Splitify.EventBus.Contracts/"]
RUN dotnet restore "src/Apps/Splitify.EmailSender/Splitify.EmailSender.csproj"
COPY . .
WORKDIR "/src/src/Apps/Splitify.EmailSender"
RUN dotnet build "Splitify.EmailSender.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Splitify.EmailSender.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Splitify.EmailSender.dll"]