FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/AuthorizationAPI.Admin.Api/AuthorizationAPI.Admin.Api.csproj", "src/AuthorizationAPI.Admin.Api/"]
COPY ["src/AuthorizationAPI.Admin.EntityFramework.Shared/AuthorizationAPI.Admin.EntityFramework.Shared.csproj", "src/AuthorizationAPI.Admin.EntityFramework.Shared/"]
COPY ["src/AuthorizationAPI.Admin.EntityFramework.SqlServer/AuthorizationAPI.Admin.EntityFramework.SqlServer.csproj", "src/AuthorizationAPI.Admin.EntityFramework.SqlServer/"]
COPY ["src/AuthorizationAPI.Shared/AuthorizationAPI.Shared.csproj", "src/AuthorizationAPI.Shared/"]
COPY ["src/AuthorizationAPI.Admin.EntityFramework.PostgreSQL/AuthorizationAPI.Admin.EntityFramework.PostgreSQL.csproj", "src/AuthorizationAPI.Admin.EntityFramework.PostgreSQL/"]
COPY ["src/AuthorizationAPI.Admin.EntityFramework.MySql/AuthorizationAPI.Admin.EntityFramework.MySql.csproj", "src/AuthorizationAPI.Admin.EntityFramework.MySql/"]
RUN dotnet restore "src/AuthorizationAPI.Admin.Api/AuthorizationAPI.Admin.Api.csproj"
COPY . .
WORKDIR "/src/src/AuthorizationAPI.Admin.Api"
RUN dotnet build "AuthorizationAPI.Admin.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthorizationAPI.Admin.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true
ENTRYPOINT ["dotnet", "AuthorizationAPI.Admin.Api.dll"]