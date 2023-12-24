#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NodinSoftProjectAPI/NodinSoftProjectAPI.csproj", "NodinSoftProjectAPI/"]
COPY ["NodinSoftProject.Application/NodinSoftProject.Application.csproj", "NodinSoftProject.Application/"]
COPY ["NodinSoftProject.Domain/NodinSoftProject.Domain.csproj", "NodinSoftProject.Domain/"]
COPY ["NodinSoftProject.Infrastructure.EFcore/NodinSoftProject.Infrastructure.EFcore.csproj", "NodinSoftProject.Infrastructure.EFcore/"]
RUN dotnet restore "./NodinSoftProjectAPI/./NodinSoftProjectAPI.csproj"
COPY . .
WORKDIR "/src/NodinSoftProjectAPI"
RUN dotnet build "./NodinSoftProjectAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NodinSoftProjectAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NodinSoftProjectAPI.dll"]