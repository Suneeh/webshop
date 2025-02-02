FROM node:alpine AS build
WORKDIR /usr/local/app
COPY ./ /usr/local/app/
RUN npm install
RUN npm install -g @angular/cli
RUN ng build --configuration=production

FROM nginx:latest
COPY --from=build /usr/local/app/dist/webshop/browser /usr/share/nginx/html
COPY ./nginx.conf  /etc/nginx/conf.d/default.conf
EXPOSE 80