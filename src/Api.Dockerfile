FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine3.15-amd64 AS base
WORKDIR /app
EXPOSE 5001/tcp

RUN apk add terminus-font && \
    apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT false
ENV ASPNETCORE_ENVIRONMENT=Production
#ENV ConnectionStrings:StarWarsConnection="server=starwars-db;database=starwars;user=sa;password=dev@1234;convert zero datetime=True;"s

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine3.15-amd64 AS build-env
COPY ["./StarWars.sln", "./"]
COPY ["./StarWars.Shared.Kernel/StarWars.Shared.Kernel.csproj", "./StarWars.Shared.Kernel/" ]
COPY ["./StarWars.Infrastructure/StarWars.Infrastructure.csproj", "./StarWars.Infrastructure/" ]
COPY ["./StarWars.Domain/StarWars.Domain.csproj", "./StarWars.Domain/" ]
COPY ["./StarWars.Application/StarWars.Application.csproj", "./StarWars.Application/" ]
COPY ["./StarWars.Api/StarWars.Api.csproj", "./StarWars.Api/" ]
#RUN dotnet restore "./StarWars.Api/StarWars.Api.csproj"
COPY ./ .

#RUN dotnet build "./StarWars.Api/StarWars.Api.csproj" --packages ./.nuget/packages -c Production -o /app/build

#RUN dotnet test

FROM build-env AS publish
RUN dotnet publish "./StarWars.Api/StarWars.Api.csproj" -c Production -o /app/publish


FROM base AS final
WORKDIR /app/build
RUN chmod +x ./

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "StarWars.Api.dll", "--server.urls", "http://*:5001"]