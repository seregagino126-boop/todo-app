# Этап 1: сборка
# Node Js позволяет запускать JS вне браузера
FROM node:20-alpine AS build

WORKDIR /app

#Copy package.json и package-lock.json
#Отдельно от остального кода - для кэширования npm install
COPY package*.json ./

#Устанавливаем зависимости
RUN npm ci

COPY . .

#Собираем приложение в продакшн-режиме
#Результат - папка /app/dist
RUN npm run build

#Этап 2: запуск

#Берем образ Nginx(легкий веб-сервер)
FROM nginx:alpine AS final

#Копируем собранные файлы из первого этапа
#/app/dist из этапа build -> /usr/share/ngins/html
#Это стандартная папка откуда nginx раздает статику
COPY --from=build /app/dist /usr/share/nginx/html
#Копируем свой конфиг Nginx
COPY nginx.conf /etc/nginx/conf.d/default.conf

#Документируем порт 80
EXPOSE 80

#Команда запуска Nginx
CMD ["nginx", "-g", "daemon off;"]
