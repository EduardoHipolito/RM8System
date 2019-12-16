FROM microsoft/aspnetcore AS base
WORKDIR /app
EXPOSE 5000

FROM microsoft/aspnetcore-build AS build
WORKDIR /src

COPY *.sln ./
COPY Gateway/Gateway.csproj Gateway/
RUN dotnet restore 
COPY . .
WORKDIR /src/Gateway
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Gateway.dll"]
