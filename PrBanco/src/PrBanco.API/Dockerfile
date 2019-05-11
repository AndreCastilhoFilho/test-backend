FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY src/PrBanco.API/PrBanco.API.csproj src/PrBanco.API/
COPY src/PrBanco.Domain/PrBanco.Domain.csproj src/PrBanco.Domain/
COPY src/PrBanco.Data/PrBanco.Data.csproj src/PrBanco.Data/
RUN dotnet restore src/PrBanco.API/PrBanco.API.csproj
COPY . .
WORKDIR /src/src/PrBanco.API
RUN dotnet build PrBanco.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish PrBanco.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PrBanco.API.dll"]
