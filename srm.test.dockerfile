FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /
USER root
RUN apt update
RUN apt install -y nodejs npm 
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
ADD ./build-files.tar.gz ./
RUN dotnet restore
COPY . .
RUN dotnet build "./SunRaysMarket.sln" -c Release -o /app/build

FROM base AS style
WORKDIR /src/styles
RUN npm install
RUN npm run build:css

FROM build AS publish 
RUN dotnet publish "./SunRaysMarket.sln" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Server.Web.dll"]