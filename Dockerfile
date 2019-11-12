FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["Calendar/Calendar.csproj", "Calendar/"]
COPY ["Business_Layer/Business_Layer.csproj", "Business_Layer/"]
COPY ["Data_Layer/Data_Layer.csproj", "Data_Layer/"]
RUN dotnet restore "Calendar/Calendar.csproj"
COPY . .
WORKDIR "/src/Calendar"
RUN dotnet build "Calendar.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Calendar.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Calendar.dll"]