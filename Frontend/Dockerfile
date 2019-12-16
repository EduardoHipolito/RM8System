FROM node:11.15.0-alpine AS builder
WORKDIR /app
EXPOSE 80

COPY package.json ./
RUN npm install
COPY . . 
CMD npm run start 