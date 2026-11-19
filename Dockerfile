# Используем официальный образ .NET SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем csproj и восстанавливаем зависимости
COPY ["wt-lab12-12auth-validation-cherevan.csproj", "./"]
RUN dotnet restore "wt-lab12-12auth-validation-cherevan.csproj"

# Копируем все остальные файлы и собираем проект
COPY . .
RUN dotnet publish "wt-lab12-12auth-validation-cherevan.csproj" -c Release -o /app/publish

# Используем официальный образ ASP.NET для запуска
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Указываем Render, что приложение должно слушать порт 10000
ENV ASPNETCORE_URLS=http://0.0.0.0:10000
EXPOSE 10000

# Запускаем приложение
ENTRYPOINT ["dotnet", "wt-lab12-12auth-validation-cherevan.dll"]