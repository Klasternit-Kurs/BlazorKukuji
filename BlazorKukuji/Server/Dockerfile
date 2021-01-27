#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["BlazorKukuji/Server/BlazorKukuji.Server.csproj", "BlazorKukuji/Server/"]
COPY ["BlazorKukuji/Shared/BlazorKukuji.Shared.csproj", "BlazorKukuji/Shared/"]
COPY ["BlazorKukuji/Client/BlazorKukuji.Client.csproj", "BlazorKukuji/Client/"]
RUN dotnet restore "BlazorKukuji/Server/BlazorKukuji.Server.csproj"
COPY . .
WORKDIR "/src/BlazorKukuji/Server"
RUN dotnet build "BlazorKukuji.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlazorKukuji.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["cert/*", "/root/.aspnet/https/"]
ENTRYPOINT ["dotnet", "BlazorKukuji.Server.dll"]