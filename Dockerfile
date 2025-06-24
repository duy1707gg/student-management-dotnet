# 1️⃣ Runtime base image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://0.0.0.0:80
# 2️⃣ Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish "student-management-dotnet.csproj" -c Release -o /src/publish

# 3️⃣ Final stage
FROM base AS final
WORKDIR /app
COPY --from=build /src/publish .
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "student-management-dotnet.dll"]
