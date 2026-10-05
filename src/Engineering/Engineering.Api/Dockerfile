#FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
#COPY ["src/Engineering/Engineering.Domain/Engineering.Domain.csproj", "/Engineering/Engineering.Domain/"]
#COPY ["src/Engineering/Engineering.Application/Engineering.Application.csproj", "/Engineering/Engineering.Application/"]
#COPY ["src/Engineering/Engineering.Infra/Engineering.Infra.csproj", "/Engineering/Engineering.Infra/"]
#COPY ["src/Engineering/Engineering.Persistence/Engineering.Persistence.csproj", "/Engineering/Engineering.Persistence/"]
#COPY ["src/Engineering/Engineering.Api/Engineering.Api.csproj", "/Engineering/Engineering.Api/"]
COPY src/ .
WORKDIR /src/Engineering/Engineering.Api/
RUN dotnet restore "Engineering.Api.csproj"
#COPY . .
WORKDIR "/src/Engineering/Engineering.Api"
RUN dotnet build "Engineering.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Engineering.Api.csproj"  -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Engineering.Api.dll"]


