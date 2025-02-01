# Webshop

This repository is my take on a webshop. I have built and modified many webshops in the past for big customers. Now I want to build my own and take you guys with me.

Follow my journey on [dev.to](https://dev.to/suneeh).

Check it out! It is deployed [here](https://shop.suneeh.de/).

# Contributing

## Clone the repo

Get the code of this git repository to your machine.

`git clone https://github.com/Suneeh/webshop.git`

## Install dependancies

Use `npm install` to install all packages and dependancies.

## Development server

Run `docker run --name webshopDb -e POSTGRES_PASSWORD=MYPASSWORD -p 5432:5432 -d postgres` to create a Postgres DB for your Backend to connect to.

Copy the `./auth_config.example.json` [file](https://github.com/Suneeh/webshop/blob/main/frontend/auth_config.example.json) and remove the `example` fill it with your own values. After that run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The application will automatically reload if you change any of the source files.

## Deployment

Run `npm run deploy` after you renamed the `deployment.exmaple.sh` to `deployment.sh`. Also be sure to adjust this to your needs as shown in the [example](https://github.com/Suneeh/webshop/blob/main/deploy.example.sh).

# TODOs

- Auth
  - Build a login mask instead of redirecting to Auth0
- Docker
  - Build a Docker Compose that starts:
    - Database
    - Backend
    - Frontend
- General
  - Redirect / to /dashboard
  - Add guards that read the accesstoken for roles (and email verification?)
- Shell
  - Build Footer with hotlinks and more information
  - Create a company logo
  - Add the company logo to the sidebar and switch the Dashboard option for it
- Category Page
  - Build a Page that shows Child Categories, Description, and Products
- Dashboard
  - Build Landing Page
- ApiClient
  - Build a Frontend Api Client that calls the backend correctly
- Misc
  - Add linting
