#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/MOSI.IdentityServer4.Identity/MOSI.IdentityServer4.Identity.csproj", "src/MOSI.IdentityServer4.Identity/"]
RUN dotnet restore "src/MOSI.IdentityServer4.Identity/MOSI.IdentityServer4.Identity.csproj"
COPY . .
WORKDIR "/src/src/MOSI.IdentityServer4.Identity"
RUN dotnet build "MOSI.IdentityServer4.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MOSI.IdentityServer4.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MOSI.IdentityServer4.Identity.dll"]