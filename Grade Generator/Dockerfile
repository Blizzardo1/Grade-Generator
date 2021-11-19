FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Grade Generator/Grade Generator.csproj", "Grade Generator/"]
RUN dotnet restore "Grade Generator/Grade Generator.csproj"
COPY . .
WORKDIR "/src/Grade Generator"
RUN dotnet build "Grade Generator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Grade Generator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Grade Generator.dll"]
