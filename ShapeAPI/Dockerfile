#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#////////////////////////////////////////////////////////

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS dev
WORKDIR /app
EXPOSE 80

COPY . /app

RUN dotnet dev-certs https
RUN dotnet tool install --global dotnet-ef
RUN dotnet restore

ENTRYPOINT ["dotnet",  "watch", "run", "--urls", "http://0.0.0.0:80"]

#////////////////////////////////////////////////////////

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS prod
WORKDIR /src
COPY ["ShapeAPI/ShapeAPI.csproj", "ShapeAPI/"]
RUN dotnet restore "ShapeAPI/ShapeAPI.csproj"
COPY . .
WORKDIR "/src/ShapeAPI"
RUN dotnet build "ShapeAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShapeAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

WORKDIR /app
COPY /app/publish .
ENTRYPOINT ["dotnet", "ShapeAPI.dll"]