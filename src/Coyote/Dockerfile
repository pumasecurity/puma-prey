#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["src/Coyote/Coyote.csproj", "src/Coyote/"]
COPY ["src/Rabbit/Rabbit.csproj", "src/Rabbit/"]
RUN dotnet restore "src/Coyote/Coyote.csproj"
COPY . .
WORKDIR "/src/src/Coyote"
RUN dotnet build "Coyote.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Coyote.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Coyote.dll"]