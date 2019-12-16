FROM microsoft/aspnetcore AS base
WORKDIR /app
EXPOSE 5002

FROM microsoft/aspnetcore-build AS build
WORKDIR /src

COPY *.sln ./
COPY Core.Web/Core.Web.csproj Core.Web/
RUN dotnet restore
COPY . .
WORKDIR /src/Core.Web
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Core.Web.dll"]
