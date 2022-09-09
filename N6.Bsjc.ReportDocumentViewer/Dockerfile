FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
RUN sed -i -e 's/deb.debian.org/mirrors.huaweicloud.com/g' -e 's/security.debian.org/mirrors.huaweicloud.com/g' /etc/apt/sources.list
RUN apt-get update
RUN apt-get install -y libgdiplus libc6-dev
RUN apt-get install -y libicu-dev libharfbuzz0b libfontconfig1 libfreetype6
WORKDIR /app
EXPOSE 80

FROM node:16-alpine as base-node
RUN sed -i 's/dl-cdn.alpinelinux.org/mirrors.aliyun.com/g' /etc/apk/repositories
#RUN apk add --no-cache autoconf automake file g++ libtool make nasm libpng-dev
#RUN npm install --location=global cnpm --registry=https://registry.npmmirror.com
#RUN npm install -g gulp --registry=https://registry.npmmirror.com

FROM base-node as develop-node
ENV NODE_ENV="Release"
WORKDIR /src
COPY ["N6.Bsjc.ReportDocumentViewer/package.json", "N6.Bsjc.ReportDocumentViewer/"]
WORKDIR "/src/N6.Bsjc.ReportDocumentViewer"
RUN npm install


FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
COPY --from=develop-node . .
RUN dotnet tool install -g Microsoft.Web.LibraryManager.Cli
ENV PATH="$PATH:/root/.dotnet/tools"
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["N6.Bsjc.ReportDocumentViewer/N6.Bsjc.ReportDocumentViewer.csproj", "N6.Bsjc.ReportDocumentViewer/"]
COPY ["N6.Bsjc.Reporting.Domain/N6.Bsjc.Reporting.Domain.csproj", "N6.Bsjc.Reporting.Domain/"]
COPY ["N6.Bsjc.Reporting.HttpApi.Client.Contracts/N6.Bsjc.Reporting.HttpApi.Client.Contracts.csproj", "N6.Bsjc.Reporting.HttpApi.Client.Contracts/"]
COPY ["N6.Core.Abp.Client.Contracts/N6.Core.Abp.Client.Contracts.csproj", "N6.Core.Abp.Client.Contracts/"]
COPY ["N6.Bsjc.Reporting.HttpClient/N6.Bsjc.Reporting.HttpApi.Client.csproj", "N6.Bsjc.Reporting.HttpClient/"]
COPY ["N6.Core.Abp.Client.HttpApi/N6.Core.Abp.Client.HttpApi.csproj", "N6.Core.Abp.Client.HttpApi/"]
RUN dotnet restore "N6.Bsjc.ReportDocumentViewer/N6.Bsjc.ReportDocumentViewer.csproj"
COPY . .
WORKDIR "/src/N6.Bsjc.ReportDocumentViewer"
RUN libman restore

FROM build AS publish
RUN dotnet publish N6.Bsjc.ReportDocumentViewer.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "N6.Bsjc.ReportDocumentViewer.dll"]