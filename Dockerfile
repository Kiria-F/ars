FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ARS.csproj", "./"]
RUN dotnet restore "ARS.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "ARS.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ARS.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ARS.dll"]
