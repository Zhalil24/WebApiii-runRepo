
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
USER root
RUN mkdir -p /src/publish
WORKDIR /src

# Copy everything
COPY . /src
RUN dotnet restore
RUN dotnet publish ./WebApiii.csproj -c Release -o /src/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /src/app
COPY --from=build-env /src/publish .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "/src/app/WebApiii.dll"]