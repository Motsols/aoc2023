# build
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build

RUN apk update
RUN apk add clang build-base zlib-dev

WORKDIR /src
COPY . .
RUN dotnet publish -o /app day04.csproj

# run
FROM amd64/alpine

WORKDIR /app
COPY --from=build /app .
COPY input.txt .

ENTRYPOINT ["./day04"]