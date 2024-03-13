FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5002
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/FastFeet.Api/FastFeet.Api.csproj", "src/FastFeet.Api/"]
COPY ["src/FastFeet.Application/FastFeet.Application.csproj", "src/FastFeet.Application/"]
COPY ["src/FastFeet.Domain/FastFeet.Domain.csproj", "src/FastFeet.Domain/"]
COPY ["src/FastFeet.Infrastructure.ExternalService/FastFeet.Infrastructure.ExternalService.csproj", "src/FastFeet.Infrastructure.ExternalService/"]
COPY ["src/FastFeet.CrossCutting/FastFeet.CrossCutting.csproj", "src/FastFeet.CrossCutting/"]
COPY ["src/FastFeet.Infrastructure.Database/FastFeet.Infrastructure.Database.csproj", "src/FastFeet.Infrastructure.Database/"]
RUN dotnet restore "src/FastFeet.Api/FastFeet.Api.csproj"
COPY . .
WORKDIR "/src/src/FastFeet.Api"
RUN dotnet build "FastFeet.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FastFeet.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FastFeet.Api.dll"]
