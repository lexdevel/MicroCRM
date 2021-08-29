FROM node:16 as build-env

COPY ./src/frontend /frontend
WORKDIR /frontend

RUN npm install
RUN npm run-script build

FROM nginx:1.21.1

COPY --from=build-env /frontend/dist /usr/share/nginx/html
