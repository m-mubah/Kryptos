#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM node:latest AS node_base

RUN echo "NODE Version:" && node --version
RUN echo "NPM Version:" && npm --version

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
COPY --from=node_base . .

WORKDIR /app
# EXPOSE 80
# EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY --from=node_base . .

WORKDIR /src
COPY ["Kryptos.Web/Server/Kryptos.Web.Server.csproj", "Kryptos.Web/Server/"]
COPY ["Kryptos.Web/Client/Kryptos.Web.Client.csproj", "Kryptos.Web/Client/"]
COPY ["Kryptos.Web/Shared/Kryptos.Web.Shared.csproj", "Kryptos.Web/Shared/"]
RUN dotnet restore "Kryptos.Web/Server/Kryptos.Web.Server.csproj"
COPY . .
WORKDIR "/src/Kryptos.Web/Server"
RUN dotnet build "Kryptos.Web.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kryptos.Web.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# ENTRYPOINT ["dotnet", "Kryptos.Web.Server.dll"]

# ENV ASPNETCORE_URLS http://*:$PORT
# CMD ["dotnet", "Kryptos.Web.Server.dll"]

CMD ASPNETCORE_URLS=http://*:$PORT dotnet Kryptos.Web.Server.dll