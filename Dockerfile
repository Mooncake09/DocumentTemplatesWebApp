FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
RUN apt-get update && apt-get install -y \
curl
CMD /bin/bash
RUN curl -sL https://deb.nodesource.com/setup_16.x | bash -
RUN apt-get install -y nodejs

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
RUN apt-get update && apt-get install -y \
curl
CMD /bin/bash
RUN curl -sL https://deb.nodesource.com/setup_16.x | bash -
RUN apt-get install -y nodejs
WORKDIR /src
COPY ["DocumentTemplatesWebApp/DocumentTemplatesWebApp.csproj", "DocumentTemplatesWebApp/"]
RUN dotnet restore "DocumentTemplatesWebApp/DocumentTemplatesWebApp.csproj"
COPY . .
WORKDIR "/src/DocumentTemplatesWebApp"
RUN dotnet build "DocumentTemplatesWebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DocumentTemplatesWebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DocumentTemplatesWebApp.dll"]