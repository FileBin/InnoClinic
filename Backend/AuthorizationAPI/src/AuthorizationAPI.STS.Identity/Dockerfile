FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/AuthorizationAPI.STS.Identity/AuthorizationAPI.STS.Identity.csproj", "src/AuthorizationAPI.STS.Identity/"]
COPY ["src/AuthorizationAPI.Admin.EntityFramework.Shared/AuthorizationAPI.Admin.EntityFramework.Shared.csproj", "src/AuthorizationAPI.Admin.EntityFramework.Shared/"]
COPY ["src/AuthorizationAPI.Shared/AuthorizationAPI.Shared.csproj", "src/AuthorizationAPI.Shared/"]
RUN dotnet restore "src/AuthorizationAPI.STS.Identity/AuthorizationAPI.STS.Identity.csproj"
COPY . .
WORKDIR "/src/src/AuthorizationAPI.STS.Identity"
RUN dotnet build "AuthorizationAPI.STS.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthorizationAPI.STS.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true
ENTRYPOINT ["dotnet", "AuthorizationAPI.STS.Identity.dll"]