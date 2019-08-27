FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/Transavia.API/Transavia.API.csproj", "src/Transavia.API/"]
RUN dotnet restore "src/Transavia.API/Transavia.API.csproj"
COPY . .
WORKDIR "/src/src/Transavia.API"
RUN dotnet build "Transavia.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Transavia.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Transavia.API.dll"]