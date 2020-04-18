FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS doc

WORKDIR /app

COPY ./BetterExtensions.sln ./
COPY ./src/BetterExtensions/BetterExtensions.csproj ./src/BetterExtensions/
COPY ./tests/BetterExtensions.Tests/BetterExtensions.Tests.csproj ./tests/BetterExtensions.Tests/

RUN dotnet restore ./src/BetterExtensions/BetterExtensions.csproj
RUN dotnet restore ./tests/BetterExtensions.Tests/BetterExtensions.Tests.csproj

COPY ./src/BetterExtensions ./src/BetterExtensions
COPY ./tests/BetterExtensions.Tests ./tests/BetterExtensions.Tests

RUN dotnet build -c Release --no-restore "./src/BetterExtensions/BetterExtensions.csproj"
RUN dotnet build -c Release --no-restore "./tests/BetterExtensions.Tests/BetterExtensions.Tests.csproj"

RUN dotnet test "./tests/BetterExtensions.Tests/BetterExtensions.Tests.csproj" -c Release --no-build --no-restore

RUN dotnet pack "./src/BetterExtensions/BetterExtensions.csproj" -c Release --no-restore --no-build -o /app/out