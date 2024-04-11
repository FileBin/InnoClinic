FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM alpine:2.6 AS prepare
WORKDIR /src
COPY . .
WORKDIR /proj
RUN find /src/ -type f -name '*.csproj' -exec echo {} \; | \
while read line; \
do mkdir -p "/proj/$(dirname $line)"; \
cp "$line" "/proj/$line"; \
done

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY --from=prepare /proj/src .
RUN dotnet restore "./Web/OfficesAPI.Web.csproj"
COPY --from=prepare /src .
RUN dotnet build "./Web/OfficesAPI.Web.csproj" --no-restore -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./Web/OfficesAPI.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true
ENV ASPNETCORE_URLS=http://0.0.0.0:80
ENTRYPOINT ["dotnet", "OfficesAPI.Web.dll"]