FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Falcevor.MongoBenchmark/Falcevor.MongoBenchmark.csproj", "Falcevor.MongoBenchmark/"]
RUN dotnet restore "Falcevor.MongoBenchmark/Falcevor.MongoBenchmark.csproj"
COPY . .
WORKDIR "/src/Falcevor.MongoBenchmark"
RUN dotnet build "Falcevor.MongoBenchmark.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Falcevor.MongoBenchmark.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Falcevor.MongoBenchmark.dll"]
