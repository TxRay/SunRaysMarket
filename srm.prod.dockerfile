FROM mrc.microsoft.com/dotnet/aspnet:8.0 AS base
USER srm
WORKDIR /app
EXPOSE 80

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