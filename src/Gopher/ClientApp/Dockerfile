# base image
FROM node:latest

RUN mkdir /usr/share/app
WORKDIR /usr/share/app
# set working directory
WORKDIR /app

# install and cache app dependencies
COPY package.json package.json
#RUN npm install
RUN npm install -g @angular/cli@11.2.12
COPY package-lock.json package-lock.json
RUN npm i
# add app
COPY . .
RUN ng build --configuration development


# add `/app/node_modules/.bin` to $PATH
ENV PATH /app/node_modules/.bin:$PATH

#EXPOSE 44418
EXPOSE 4200 49153
# start app
#CMD ng serve --host 0.0.0.0 --poll 500
CMD ["ng", "serve", "--port", "4200", "--host=0.0.0.0", "--watch", "--poll=2000", "--disable-host-check" ]
#CMD ["ng", "serve", "--port", "4200", "--host", "0.0.0.0", "--disable-host-check", "--poll", "2000"]
