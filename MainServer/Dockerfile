# Use the official ASP.NET Core runtime as a base image for the runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 7037

# Use the SDK image to build the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project files and restore dependencies
COPY ["MainServer/MainServer.csproj", "MainServer/"]
COPY ["AppBuilder/AppBuilder.csproj", "AppBuilder/"]
COPY ["CallComponent/CallComponent.csproj", "CallComponent/"]
COPY ["Core/Core.csproj", "Core/"]
COPY ["Database/Database.csproj", "Database/"]
COPY ["SemanticAnalysisComponent/SemanticAnalysisComponent.csproj", "SemanticAnalysisComponent/"]
COPY ["TopicComponent/TopicComponent.csproj", "TopicComponent/"]
# Restore the project dependencies
RUN dotnet restore "MainServer/MainServer.csproj"

# Copy the rest of the application code
COPY . .

# Build the project
WORKDIR "/src/MainServer"
RUN dotnet build "MainServer.csproj" -c Release -o /app/build

# Publish the project
FROM build AS publish
RUN dotnet publish "MainServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the base image to run the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MainServer.dll"]