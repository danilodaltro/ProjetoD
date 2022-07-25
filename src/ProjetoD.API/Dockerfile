#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/ProjetoD.API/ProjetoD.API.csproj", "src/ProjetoD.API/"]
COPY ["src/ProjetoD.Application/ProjetoD.Application.csproj", "src/ProjetoD.Application/"]
COPY ["src/ProjetoD.Infra.Data/ProjetoD.Infra.Data.csproj", "src/ProjetoD.Infra.Data/"]
COPY ["src/ProjetoD.Domain/ProjetoD.Domain.csproj", "src/ProjetoD.Domain/"]
RUN dotnet restore "src/ProjetoD.API/ProjetoD.API.csproj"
COPY . .
WORKDIR "/src/src/ProjetoD.API"
RUN dotnet build "ProjetoD.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProjetoD.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProjetoD.API.dll"]
