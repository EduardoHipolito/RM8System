FROM microsoft/aspnetcore AS base
WORKDIR /app
EXPOSE 5001

FROM microsoft/aspnetcore-build AS build
WORKDIR /src

COPY *.sln ./
COPY Stock.Web/Stock.Web.csproj Stock.Web/
RUN dotnet restore
COPY . .
WORKDIR /src/Stock.Web
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Stock.Web.dll"]
