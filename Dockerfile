FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Calendar/Calendar.csproj", "Calendar/"]
COPY ["Business/Business.csproj", "Business/"]
COPY ["Data/Data.csproj", "Data/"]
RUN dotnet restore "Calendar/Calendar.csproj"
RUN apt-get update && apt-get install vim -y
COPY . .
WORKDIR "/src/Calendar"
RUN dotnet build "Calendar.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Calendar.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Calendar.dll"]