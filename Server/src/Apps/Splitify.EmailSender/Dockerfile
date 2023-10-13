#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ARG SMTP_API_PATH
ARG SMTP_API_KEY
ARG SMTP_SENDER_EAMIL
ARG SMTP_SENDER_NAME

ARG MESSAGING_HOST
ARG MESSAGING_ACCESS_KEY
ARG MESSAGING_SECRET_KEY
ENV Messaging__Host=$MESSAGING_HOST
ENV Messaging__AccessKey=$MESSAGING_ACCESS_KEY
ENV Messaging__SecretKey=$MESSAGING_SECRET_KEY

ENV SmtpClient__ApiPath=$SMTP_API_PATH
ENV SmtpClient__ApiKey=$SMTP_API_KEY
ENV SmtpClient__SenderEmail=$SMTP_SENDER_EAMIL
ENV SmtpClient__SenderName=$SMTP_SENDER_NAME


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