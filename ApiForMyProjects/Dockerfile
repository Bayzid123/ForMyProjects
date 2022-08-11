#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["PartnerManagement/PartnerManagement.csproj", "PartnerManagement/"]
COPY ["Domain.Core/Domain.Core.csproj", "Domain.Core/"]
COPY ["Infra.Bus/Infra.Bus.csproj", "Infra.Bus/"]
RUN dotnet restore "PartnerManagement/PartnerManagement.csproj"
COPY . .
WORKDIR "/src/PartnerManagement"
RUN dotnet build "PartnerManagement.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PartnerManagement.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PartnerManagement.dll"]