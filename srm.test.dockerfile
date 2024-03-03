FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /srm_app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
#COPY ./**.csproj ./
#RUN dotnet restore
COPY . .
RUN dotnet restore
RUN dotnet build "./SunRaysMarket.sln" -c Release -o /app/build

FROM build AS publish 
RUN dotnet publish "./SunRaysMarket.sln" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Server.Web.dll"]