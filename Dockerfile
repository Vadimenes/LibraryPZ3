FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG CONFIGURATION=Release
WORKDIR /src # тута рабочая директория внутри контейнера

COPY ["LibraryPZ3.csproj", "./"] # тута для восстановления зависимостей
RUN dotnet restore "LibraryPZ3.csproj"

COPY . . # тута копирование остального кода
RUN dotnet build "LibraryPZ3.csproj" -c ${CONFIGURATION} -o /app/build

FROM build AS publish # тута публикация
RUN dotnet publish "LibraryPZ3.csproj" -c ${CONFIGURATION} -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080 # тута открывает порт 8080

COPY --from=publish /app/publish .  # тута копирование опубликованных файлов

# Установка curl для healthcheck
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

ENTRYPOINT ["dotnet", "LibraryPZ3.dll"] # тута команда запуска приложения