#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-alpine AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build

WORKDIR /src

COPY ["Observer.Worker/Observer.Worker.csproj", "Observer.Worker/"]

RUN dotnet restore "Observer.Worker/Observer.Worker.csproj"

COPY . .

WORKDIR "/src/Observer.Worker"

RUN dotnet build "Observer.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Observer.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN apk add --no-cache bash

CMD [ "dotnet", "Observer.Worker.dll" ]
