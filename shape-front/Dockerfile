FROM node:16-alpine AS dev

RUN apk add --no-cache libc6-compat

WORKDIR /app

COPY package.json yarn.lock ./

RUN yarn install --frozen-lockfile

COPY . /app

EXPOSE 3000

CMD yarn dev