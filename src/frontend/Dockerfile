FROM node:16

COPY ./src/frontend /frontend
WORKDIR /frontend

RUN npm install
# RUN npm run-script build

ENTRYPOINT [ "npm", "start" ]
