FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["backend/Neelcoco.API/Neelcoco.API.csproj", "backend/Neelcoco.API/"]
RUN dotnet restore "backend/Neelcoco.API/Neelcoco.API.csproj"
COPY . .
WORKDIR "/src/backend/Neelcoco.API"
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "Neelcoco.API.dll"]
