FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

WORKDIR /app

COPY ./BetterExtensions.sln ./
COPY ./src/BetterExtensions/BetterExtensions.csproj ./src/BetterExtensions/
COPY ./tests/BetterExtensions.Tests/BetterExtensions.Tests.csproj ./tests/BetterExtensions.Tests/

RUN dotnet restore
COPY . .

ARG CI_BUILDID
ARG CI_PRERELEASE

ENV CI_BUILDID ${CI_BUILDID}
ENV CI_PRERELEASE ${CI_PRERELEASE}

RUN dotnet build -c Release --no-restore
RUN dotnet test -c Release --no-build --no-restore
RUN dotnet pack -c Release --no-restore --no-build -o /app/out