#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Appointment/Appointment.API/Appointment.API.csproj", "Services/Appointment/Appointment.API/"]
RUN dotnet restore "Services/Appointment/Appointment.API/Appointment.API.csproj"
COPY . .
WORKDIR "/src/Services/Appointment/Appointment.API"
RUN dotnet build "Appointment.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Appointment.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Appointment.API.dll"]
