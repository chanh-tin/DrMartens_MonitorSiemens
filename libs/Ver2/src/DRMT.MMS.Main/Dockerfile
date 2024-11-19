#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/DRMT.MMS.Main/DRMT.MMS.Main.csproj", "src/DRMT.MMS.Main/"]
COPY ["src/DRMT.MMS.MainDatabase/DRMT.MMS.MainDatabase.csproj", "src/DRMT.MMS.MainDatabase/"]
RUN dotnet restore "./src/DRMT.MMS.Main/DRMT.MMS.Main.csproj"
COPY . .
WORKDIR "/src/src/DRMT.MMS.Main"
RUN dotnet build "./DRMT.MMS.Main.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DRMT.MMS.Main.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DRMT.MMS.Main.dll"]